namespace ArrangeAirQualityData
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_ImportExcel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Lab_Message0 = new System.Windows.Forms.Label();
            this.Lab_Message = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_ExportExcel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lab_Mode = new System.Windows.Forms.Label();
            this.cbx_Mode = new System.Windows.Forms.ComboBox();
            this.chk_ExportSelectedColumn = new System.Windows.Forms.CheckBox();
            this.cb_SelectedColumn = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btn_ImportExcel);
            this.flowLayoutPanel1.Controls.Add(this.progressBar1);
            this.flowLayoutPanel1.Controls.Add(this.Lab_Message0);
            this.flowLayoutPanel1.Controls.Add(this.Lab_Message);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(469, 37);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_ImportExcel
            // 
            this.btn_ImportExcel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_ImportExcel.Location = new System.Drawing.Point(4, 4);
            this.btn_ImportExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ImportExcel.Name = "btn_ImportExcel";
            this.btn_ImportExcel.Size = new System.Drawing.Size(133, 29);
            this.btn_ImportExcel.TabIndex = 0;
            this.btn_ImportExcel.Text = "讀取Excel檔案";
            this.btn_ImportExcel.UseVisualStyleBackColor = true;
            this.btn_ImportExcel.Click += new System.EventHandler(this.btn_ImportExcel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(145, 4);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(133, 29);
            this.progressBar1.TabIndex = 1;
            // 
            // Lab_Message0
            // 
            this.Lab_Message0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Lab_Message0.AutoSize = true;
            this.Lab_Message0.Location = new System.Drawing.Point(285, 11);
            this.Lab_Message0.Name = "Lab_Message0";
            this.Lab_Message0.Size = new System.Drawing.Size(91, 15);
            this.Lab_Message0.TabIndex = 3;
            this.Lab_Message0.Text = "Lab_Message0";
            // 
            // Lab_Message
            // 
            this.Lab_Message.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Lab_Message.AutoSize = true;
            this.Lab_Message.Location = new System.Drawing.Point(382, 11);
            this.Lab_Message.Name = "Lab_Message";
            this.Lab_Message.Size = new System.Drawing.Size(84, 15);
            this.Lab_Message.TabIndex = 2;
            this.Lab_Message.Text = "Lab_Message";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btn_ExportExcel);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 527);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(911, 48);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btn_ExportExcel
            // 
            this.btn_ExportExcel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_ExportExcel.Location = new System.Drawing.Point(4, 4);
            this.btn_ExportExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ExportExcel.Name = "btn_ExportExcel";
            this.btn_ExportExcel.Size = new System.Drawing.Size(133, 40);
            this.btn_ExportExcel.TabIndex = 0;
            this.btn_ExportExcel.Text = "匯出Excel";
            this.btn_ExportExcel.UseVisualStyleBackColor = true;
            this.btn_ExportExcel.Click += new System.EventHandler(this.btn_ExportExcel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 66);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(911, 461);
            this.dataGridView1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 37);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(780, 11);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(127, 15);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "歷年監測資料下載";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.lab_Mode);
            this.flowLayoutPanel3.Controls.Add(this.cbx_Mode);
            this.flowLayoutPanel3.Controls.Add(this.chk_ExportSelectedColumn);
            this.flowLayoutPanel3.Controls.Add(this.cb_SelectedColumn);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 37);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(911, 29);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // lab_Mode
            // 
            this.lab_Mode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lab_Mode.AutoSize = true;
            this.lab_Mode.Location = new System.Drawing.Point(3, 7);
            this.lab_Mode.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lab_Mode.Name = "lab_Mode";
            this.lab_Mode.Size = new System.Drawing.Size(67, 15);
            this.lab_Mode.TabIndex = 3;
            this.lab_Mode.Text = "匯出模式";
            // 
            // cbx_Mode
            // 
            this.cbx_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Mode.FormattingEnabled = true;
            this.cbx_Mode.Location = new System.Drawing.Point(70, 3);
            this.cbx_Mode.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cbx_Mode.Name = "cbx_Mode";
            this.cbx_Mode.Size = new System.Drawing.Size(200, 23);
            this.cbx_Mode.TabIndex = 2;
            // 
            // chk_ExportSelectedColumn
            // 
            this.chk_ExportSelectedColumn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chk_ExportSelectedColumn.AutoSize = true;
            this.chk_ExportSelectedColumn.Location = new System.Drawing.Point(313, 5);
            this.chk_ExportSelectedColumn.Margin = new System.Windows.Forms.Padding(40, 2, 0, 2);
            this.chk_ExportSelectedColumn.Name = "chk_ExportSelectedColumn";
            this.chk_ExportSelectedColumn.Size = new System.Drawing.Size(119, 19);
            this.chk_ExportSelectedColumn.TabIndex = 1;
            this.chk_ExportSelectedColumn.Text = "匯出指定欄位";
            this.chk_ExportSelectedColumn.UseVisualStyleBackColor = true;
            this.chk_ExportSelectedColumn.CheckedChanged += new System.EventHandler(this.chk1_CheckedChanged);
            // 
            // cb_SelectedColumn
            // 
            this.cb_SelectedColumn.FormattingEnabled = true;
            this.cb_SelectedColumn.Location = new System.Drawing.Point(432, 2);
            this.cb_SelectedColumn.Margin = new System.Windows.Forms.Padding(0, 2, 3, 2);
            this.cb_SelectedColumn.Name = "cb_SelectedColumn";
            this.cb_SelectedColumn.Size = new System.Drawing.Size(121, 23);
            this.cb_SelectedColumn.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 575);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(926, 611);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "歷年空汙資料格式轉換工具 v5.0 (2019.12.21)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_ImportExcel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btn_ExportExcel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label Lab_Message;
        private System.Windows.Forms.Label Lab_Message0;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.ComboBox cb_SelectedColumn;
        private System.Windows.Forms.CheckBox chk_ExportSelectedColumn;
        private System.Windows.Forms.Label lab_Mode;
        private System.Windows.Forms.ComboBox cbx_Mode;
    }
}

