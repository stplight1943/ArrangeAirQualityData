using ConvertData.Common;
using ExcelTool;
using ExcelToolForWin;
using ModuleTool;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsTools;

namespace ArrangeAirQualityData
{
    public partial class Form1 : Form
    {
        private enum RunMode
        {
            合併,
            測項單一欄位並合併
        }

        private ChangeLabelText myChangeLabelText;
        private ChangeProgressBarValue myChangeProgressBarValue;
        private MyStyle.DataGridViewStyle myStyle;
        private ControlTools myControlTools;
        private DataTable dt_ImportData = null;

        //參數設定；Tools.SetControlEnabled 作業所需之參數
        Type[] ControlType = { typeof(Button) };

        public Form1()
        {
            InitializeComponent();
            myChangeLabelText = new ChangeLabelText();
            myChangeProgressBarValue = new ChangeProgressBarValue();
            myStyle = new MyStyle.DataGridViewStyle();
            myControlTools = new ControlTools();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Lab_Message0.Text = string.Empty;
            Lab_Message.Text = string.Empty;
            myChangeLabelText.Label = Lab_Message;
            myChangeProgressBarValue.ProgressBar = progressBar1;

            cb_SelectedColumn.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_SelectedColumn.Enabled = chk_ExportSelectedColumn.Checked;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            myStyle.DGV_default(dataGridView1);

            Dictionary<RunMode, string> dic_RunMode = new Dictionary<RunMode, string>()
            {
                { RunMode.合併, RunMode.合併.ToString() },
                { RunMode.測項單一欄位並合併, RunMode.測項單一欄位並合併.ToString()}
            };
            cbx_Mode.DataSource = new BindingSource(dic_RunMode, null);
            cbx_Mode.ValueMember = "Key";
            cbx_Mode.DisplayMember = "Value";
            cbx_Mode.SelectedIndex = 0;
        }

        private void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            ExcelMethod myExcelMethod = new ExcelMethod();
            string[] paths = myExcelMethod.GetLoadPaths();
            if (paths == null)
            {
                return;
            }

            //鎖住控制項
            myControlTools.SetControlEnabled(this, false, ControlType);

            var uiContext = System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            ExcelTool.ExcelToDataTable myExcel = new ExcelTool.ExcelToDataTable();
            myExcel.Progress.Executing = msg => myChangeLabelText.ChangeTextByInterval(msg, uiContext);
            myExcel.Progress.ProgressRange = (min, max) => myChangeProgressBarValue.SetProgressBarRange(min, max, uiContext); // 設定ProgressBar的Value範圍
            myExcel.Progress.ProgressValue = val => myChangeProgressBarValue.ChangeValueByInterval(val, uiContext); // 變更Value的方法


            ChangeLabelText myChangeLabelText0 = new ChangeLabelText();
            myChangeLabelText0.Label = Lab_Message0;


            myChangeProgressBarValue.ClearJob();
            dataGridView1.DataSource = null;
            dt_ImportData = null;

            Task myTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    for (int i = 0; i <= paths.GetUpperBound(0); i++)
                    {
                        myChangeLabelText0.ChangeText(string.Format("正在讀取第 {0} 個檔案，共 {1} 個。", i + 1, paths.Length), uiContext);
                        myExcel.FilePath = paths[i];
                        if (dt_ImportData == null)
                            dt_ImportData = myExcel.ReadExcelToDatatable(0, true);
                        else
                            dt_ImportData.Merge(myExcel.ReadExcelToDatatable(0, true));
                    }
                    myChangeProgressBarValue.FinishJob(uiContext); // 填滿進度條
                    myChangeLabelText0.ChangeText(string.Empty, uiContext);
                    myChangeLabelText.ChangeText("匯入完成", uiContext);
                }
                catch (Exception ex)
                {
                    cts.Cancel();

                    myChangeProgressBarValue.ClearJob(uiContext);
                    myChangeLabelText.ChangeText("發生錯誤!", uiContext);
                    MessageBox.Show(ModuleTool.ExceptionClass.GetExceptionMessage(ex).ToString(), "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    //控制項解鎖
                    myControlTools.SetControlEnabled(this, true, uiContext, ControlType);
                }
            }, token);

            myTask.ContinueWith((CTask) =>
            {
                // If cts.IsCancellationRequested Then
                // SetControlEnabled(True, uiContext) '設定並回傳控制項狀態
                // Exit Sub
                // End If

                cb_SelectedColumn.Items.AddRange(Constant.ImportColumns);
                //comboBox1.DataSource = null;
                //var 測項list = from r in dt_ImportData.AsEnumerable()
                //             group r by new
                //             {
                //                 測項 = r.Field<string>("測項")
                //             } into g
                //             select new
                //             {
                //                 測項 = g.Key.測項,
                //             };

                //comboBox1.Items.AddRange(測項list.Select(p => p.測項).ToArray<object>());


                //foreach (var x in 測項list)
                //{
                //    if (!comboBox1.Items.Cast<string>().Contains(x.測項))
                //    {
                //        comboBox1.Items.Add(x.測項);
                //    }
                //}

                if (cb_SelectedColumn.Items.Count > 0) cb_SelectedColumn.SelectedIndex = 0;

                dataGridView1.DataSource = dt_ImportData;
                myStyle.SetColumnsWidth(dataGridView1);

            }, token, TaskContinuationOptions.None, uiContext);
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            if (dt_ImportData == null)
                return;

            //鎖住控制項
            myControlTools.SetControlEnabled(this, false, ControlType);

            TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext();

            if ((RunMode)cbx_Mode.SelectedValue == RunMode.合併)
            {
                Mode0(uiContext);
            }
            else if ((RunMode)cbx_Mode.SelectedValue == RunMode.測項單一欄位並合併)
            {
                Mode1(uiContext);
            }
        }


        //輸出的資料表格式
        private Dictionary<string, bool> GetItems()
        {
            //設定一容器，檢查一個時間點的測項是否已讀取
            Dictionary<string, bool> dic_flag = new Dictionary<string, bool>();
            //設定需要匯出的測項
            if (chk_ExportSelectedColumn.Checked)
            {
                dic_flag.Add(cb_SelectedColumn.Text, false);
            }
            else
            {
                foreach (var col in Constant.ImportColumns)
                {
                    dic_flag.Add(col, false);
                }
            }

            return dic_flag;
        }

        //輸出的資料表格式
        private DataTable GetExportTable()
        {
            DataTable dt_export = new DataTable();
            dt_export.Columns.Add("SiteName", typeof(string));
            dt_export.Columns.Add("PublishTime", typeof(DateTime));
            if (chk_ExportSelectedColumn.Checked)
            {
                dt_export.Columns.Add(cb_SelectedColumn.Text, typeof(float));
            }
            else
            {
                foreach (var col in Constant.ImportColumns)
                {
                    dt_export.Columns.Add(col, typeof(float));
                }
            }

            return dt_export;
        }

        private void Mode0(TaskScheduler uiContext)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            //設定進度條
            ProgressBase myProgress = new ProgressBase
            {
                Executing = (msg) => myChangeLabelText.ChangeTextByInterval(msg, uiContext),
                ProgressRange = (min, max) => myChangeProgressBarValue.SetProgressBarRange(min, max, uiContext), //設定ProgressBar的Value範圍
                ProgressValue = (val) => myChangeProgressBarValue.ChangeValueByInterval(val, uiContext) //變更Value的方法
            };
            myProgress.SetProgressRange(1, dt_ImportData.Rows.Count);

            //取得輸出資料格式
            DataTable dt_export = GetExportTable();

            //如超過 EXCEL 單個頁籤的最大 Rows Count 時，將拆分成多頁籤存放
            List<DataTable> list_dt = new List<DataTable>
            {
                dt_export.Clone()
            };


            Task myTask1 = Task.Factory.StartNew(() =>
            {
                try
                {
                    //設定一容器，檢查一個時間點的測項是否已讀取
                    Dictionary<string, bool> dic_flag = GetItems();

                    int AllDataCount = dt_ImportData.Rows.Count * 24; //所有測項 * 24 小時
                    int DataIndex = 0;

                    foreach (DataColumn c in dt_ImportData.Columns)
                    {
                        switch (c.ColumnName)
                        {
                            case "00":
                            case "01":
                            case "02":
                            case "03":
                            case "04":
                            case "05":
                            case "06":
                            case "07":
                            case "08":
                            case "09":
                            case "10":
                            case "11":
                            case "12":
                            case "13":
                            case "14":
                            case "15":
                            case "16":
                            case "17":
                            case "18":
                            case "19":
                            case "20":
                            case "21":
                            case "22":
                            case "23":
                                DataIndex += 1; //計數

                                //xlsx的Sheet的RowCount最大限制為2^20 (1048576)
                                if (list_dt[list_dt.Count - 1].Rows.Count >= 1048500)
                                    list_dt.Add(dt_export.Clone());

                                DataRow r = list_dt[list_dt.Count - 1].NewRow();
                                dic_flag.ToDictionary(p => p.Key, p => false); //設定所有值為 false

                                foreach (DataRow row in dt_ImportData.Rows)
                                {
                                    //當 dic_flag.Values 找不到 true 時，表示該設定新的 DataRow
                                    if (!dic_flag.Values.Contains(true))
                                    {
                                        //初始row使用
                                        r = list_dt[list_dt.Count - 1].NewRow();
                                    }

                                    string 測項 = row["測項"].ToString();
                                    string 測站 = row["測站"].ToString();
                                    string 時間 = $"{row["日期"].ToString()} {c.ColumnName}:00:00";

                                    //略過不需要的測項(只匯出選擇的測項)
                                    if (!dic_flag.Keys.Contains(測項)) continue;
                                    var tmp = r["PublishTime"];
                                    //時間不一致，表示該項目屬於新的 Row
                                    if (!string.IsNullOrEmpty(tmp.ToString()) && (DateTime)tmp != DateTime.Parse(時間)) //(dic_flag[測項] == true)
                                    {
                                        //row最後一筆使用
                                        list_dt[list_dt.Count - 1].Rows.Add(r);

                                        //設定所有值為 false
                                        dic_flag = dic_flag.ToDictionary(p => p.Key, p => false);

                                        //初始row使用
                                        r = list_dt[list_dt.Count - 1].NewRow();
                                    }


                                    r["SiteName"] = 測站;
                                    r["PublishTime"] = 時間;


                                    if (float.TryParse(row[c.ColumnName].ToString(), out float myValue))
                                    {
                                        r[測項] = myValue;
                                    }
                                    else
                                    {
                                        r[測項] = DBNull.Value;
                                    }
                                    //標記此次的 DataRow 已加入該測項的值
                                    dic_flag[測項] = true;


                                    //當該 DataRow 的所有 Cell 已加入資料時
                                    if (!dic_flag.Values.Contains(false))
                                    {
                                        //row最後一筆使用
                                        list_dt[list_dt.Count - 1].Rows.Add(r);

                                        //設定所有值為 false
                                        dic_flag = dic_flag.ToDictionary(p => p.Key, p => false);
                                    }
                                }

                                myProgress.Run(DataIndex);
                                myProgress.Run(string.Format($"正在處理第 {DataIndex} 筆資料，共 {AllDataCount} 筆..."));
                                break;
                        }



                    }


                    ////無作用，UI一樣會卡死
                    //Task.Factory.StartNew(() =>
                    //{
                    //    foreach (var table in list_dt)
                    //        dt_export.Merge(table);

                    //    dataGridView1.DataSource = dt_export;
                    //}, CancellationToken.None, TaskCreationOptions.None, uiContext);

                    myChangeProgressBarValue.FinishJob(uiContext);
                    myChangeLabelText.ChangeText("處理完成", uiContext);
                }
                catch (Exception ex)
                {
                    cts.Cancel();

                    MessageBox.Show(ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //控制項解鎖
                    myControlTools.SetControlEnabled(this, true, uiContext, ControlType);
                }
            }, token, TaskCreationOptions.None, uiContext).ContinueWith((c) =>
            {
                try
                {
                    ExportExcel myExportExcel = new ExportExcel();
                    myExportExcel.MessageLabel = Lab_Message;
                    myExportExcel.ProgressBar = progressBar1;
                    int DataCount = 1 + list_dt[0].Rows.Count; //1為第一列的欄位名稱
                    if (DataCount < 65530)
                        myExportExcel.ExcelFileNameExtension = DataTableToExcel.ExcelFileNameExtensionEnum.xls;
                    else if (DataCount < 1048570)
                        myExportExcel.ExcelFileNameExtension = DataTableToExcel.ExcelFileNameExtensionEnum.xlsx;
                    else
                        throw new Exception("資料列數不得大於 1048570");

                    ChangeLabelText myChangeLabelText0 = new ChangeLabelText();
                    myChangeLabelText0.Label = Lab_Message0;

                    int count_tmp = 0;
                    foreach (var table in list_dt)
                    {
                        int count1 = count_tmp += 1;
                        string SheetName = (count1).ToString();
                        myExportExcel.AddDataMethod(() => myChangeLabelText0.ChangeText(string.Format("Sheet({0}/{1})", count1, list_dt.Count), uiContext));
                        myExportExcel.AddDataMethod(() => myExportExcel.Add(SheetName));
                        myExportExcel.AddDataMethod(() => myExportExcel.Item[myExportExcel.Item.GetLength(0) - 1].AddData(table, true));
                    }
                    myExportExcel.AddDataMethod(() => myChangeLabelText0.ChangeText(string.Empty, uiContext));
                    myExportExcel.AddDataMethod(() => myExportExcel.SetProgressInterval(false, uiContext));
                    myExportExcel.Export(cts, uiContext);

                    myChangeProgressBarValue.FinishJob(uiContext);
                    myChangeLabelText.ChangeText("處理完成", uiContext);
                    //SetControlEnabled(True, uiContext); '設定並回傳控制項狀態
                }
                catch (Exception ex)
                {
                    cts.Cancel();
                    //SetControlEnabled(True, uiContext); //設定並回傳控制項狀態
                    String Message = ex.Message;
                    if (string.IsNullOrEmpty(Message) || !string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        Message = ex.InnerException.Message;
                    }

                    MessageBox.Show(Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    //控制項解鎖
                    myControlTools.SetControlEnabled(this, true, uiContext, ControlType);
                }
            });
        }

        private void Mode1(TaskScheduler uiContext)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            ProgressBase myProgress = new ProgressBase();
            myProgress.Executing = (msg) => myChangeLabelText.ChangeTextByInterval(msg, uiContext);
            myProgress.ProgressRange = (min, max) => myChangeProgressBarValue.SetProgressBarRange(min, max, uiContext); //設定ProgressBar的Value範圍
            myProgress.ProgressValue = (val) => myChangeProgressBarValue.ChangeValueByInterval(val, uiContext); //變更Value的方法
            myProgress.SetProgressRange(1, dt_ImportData.Rows.Count);

            DataTable dt_export = new DataTable();
            dt_export.Columns.Add("SiteName", typeof(string));
            dt_export.Columns.Add("PublishTime", typeof(DateTime));
            dt_export.Columns.Add("MonitoringItem", typeof(string));
            dt_export.Columns.Add("MonitoringValue", typeof(float));
            List<DataTable> list_dt = new List<DataTable>();
            list_dt.Add(dt_export.Clone());

            //ConcurrentBag<int> list = new ConcurrentBag<int>();
            //Parallel.For(0, 10000, item =>
            //{
            //    list.Add(item);
            //});


            Task myTask1 = Task.Factory.StartNew(() =>
            {
                try
                {
                    //設定一容器，檢查一個時間點的測項是否已讀取
                    Dictionary<string, bool> dic_flag = GetItems();

                    int AllDataCount = dt_ImportData.Rows.Count;
                    int DataIndex = 0;
                    
                    foreach (DataRow row in dt_ImportData.Rows)
                    {
                        DataIndex += 1; //計數

                        //略過不需要的測項(只匯出選擇的測項)
                        if (!dic_flag.Keys.Contains(row["測項"].ToString())) continue;

                        foreach (DataColumn c in dt_ImportData.Columns)
                        {
                            switch (c.ColumnName)
                            {
                                case "00":
                                case "01":
                                case "02":
                                case "03":
                                case "04":
                                case "05":
                                case "06":
                                case "07":
                                case "08":
                                case "09":
                                case "10":
                                case "11":
                                case "12":
                                case "13":
                                case "14":
                                case "15":
                                case "16":
                                case "17":
                                case "18":
                                case "19":
                                case "20":
                                case "21":
                                case "22":
                                case "23":

                                    //xlsx的Sheet的RowCount最大限制為2^20 (1048576)
                                    if (list_dt[list_dt.Count - 1].Rows.Count >= 1048500)
                                        list_dt.Add(dt_export.Clone());

                                    //如有測項才加入
                                    if (float.TryParse(row[c.ColumnName].ToString(), out float myValue))
                                    {
                                        DataRow r = list_dt[list_dt.Count - 1].NewRow(); //初始row使用
                                        r["SiteName"] = row["測站"].ToString();
                                        r["PublishTime"] = string.Format("{0} {1}:00:00", row["日期"].ToString(), c.ColumnName);
                                        r["MonitoringItem"] = row["測項"].ToString();
                                        r["MonitoringValue"] = myValue;
                                        list_dt[list_dt.Count - 1].Rows.Add(r);
                                    }

                                    break;
                            }
                        }

                        myProgress.Run(DataIndex);
                        myProgress.Run(string.Format($"正在處理第 {DataIndex} 筆資料，共 {AllDataCount} 筆..."));
                    }


                    ////無作用，UI一樣會卡死
                    //Task.Factory.StartNew(() =>
                    //{
                    //    foreach (var table in list_dt)
                    //        dt_export.Merge(table);

                    //    dataGridView1.DataSource = dt_export;
                    //}, CancellationToken.None, TaskCreationOptions.None, uiContext);

                    myChangeProgressBarValue.FinishJob(uiContext);
                    myChangeLabelText.ChangeText("處理完成", uiContext);
                }
                catch (Exception ex)
                {
                    cts.Cancel();
                    //SetControlEnabled(True, uiContext); //設定並回傳控制項狀態
                    String Message = ex.Message;
                    if (string.IsNullOrEmpty(Message))
                    {
                        Message = ex.InnerException.Message;
                    }

                    MessageBox.Show(Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //控制項解鎖
                    myControlTools.SetControlEnabled(this, true, uiContext, ControlType);
                }
            }, token, TaskCreationOptions.None, uiContext).ContinueWith((c) =>
            {
                try
                {
                    ExportExcel myExportExcel = new ExportExcel();
                    myExportExcel.MessageLabel = Lab_Message;
                    myExportExcel.ProgressBar = progressBar1;
                    int DataCount = 1 + list_dt[0].Rows.Count; //1為第一列的欄位名稱
                    if (DataCount < 65530)
                        myExportExcel.ExcelFileNameExtension = DataTableToExcel.ExcelFileNameExtensionEnum.xls;
                    else if (DataCount < 1048570)
                        myExportExcel.ExcelFileNameExtension = DataTableToExcel.ExcelFileNameExtensionEnum.xlsx;
                    else
                        throw new Exception("資料列數不得大於 1048570");

                    ChangeLabelText myChangeLabelText0 = new ChangeLabelText();
                    myChangeLabelText0.Label = Lab_Message0;

                    int count_tmp = 0;
                    foreach (var table in list_dt)
                    {
                        int count1 = count_tmp += 1;
                        string SheetName = (count1).ToString();
                        myExportExcel.AddDataMethod(() => myChangeLabelText0.ChangeText(string.Format("Sheet({0}/{1})", count1, list_dt.Count), uiContext));
                        myExportExcel.AddDataMethod(() => myExportExcel.Add(SheetName));
                        myExportExcel.AddDataMethod(() => myExportExcel.Item[myExportExcel.Item.GetLength(0) - 1].AddData(table, true));
                    }
                    myExportExcel.AddDataMethod(() => myChangeLabelText0.ChangeText(string.Empty, uiContext));
                    myExportExcel.AddDataMethod(() => myExportExcel.SetProgressInterval(false, uiContext));
                    myExportExcel.Export(cts, uiContext);

                    myChangeProgressBarValue.FinishJob(uiContext);
                    myChangeLabelText.ChangeText("處理完成", uiContext);
                    //SetControlEnabled(True, uiContext); '設定並回傳控制項狀態
                }
                catch (Exception ex)
                {
                    cts.Cancel();
                    //SetControlEnabled(True, uiContext); //設定並回傳控制項狀態
                    String Message = ex.Message;
                    if (string.IsNullOrEmpty(Message) || !string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        Message = ex.InnerException.Message;
                    }

                    MessageBox.Show(Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    //控制項解鎖
                    myControlTools.SetControlEnabled(this, true, uiContext, ControlType);
                }
            });
        }

        //Dictionary<string, DateTime> dic_time = new Dictionary<string, DateTime>();
        //private DateTime GetDateTime(string str, string format)
        //{
        //    if (dic_time.ContainsKey(str))
        //        return dic_time[str];
        //    else
        //    {
        //        DateTime dti = DateTime.ParseExact(str, format, new CultureInfo("zh-TW", true));
        //        dic_time.Add(str, dti);
        //        return dti;
        //    }
        //}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked." + Environment.NewLine +
                    ex.Message);
            }
        }
        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited   
            // to true.  
            linkLabel1.LinkVisited = true;
            //Call the Process.Start method to open the default browser   
            //with a URL:  
            System.Diagnostics.Process.Start("https://taqm.epa.gov.tw/taqm/tw/YearlyDataDownload.aspx");
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            cb_SelectedColumn.Enabled = chk_ExportSelectedColumn.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }


}
