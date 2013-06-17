namespace GenerateDataObject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSpBrowser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCofigBrowser = new System.Windows.Forms.Button();
            this.txtSpNamePath = new System.Windows.Forms.TextBox();
            this.txtConnectionStringPath = new System.Windows.Forms.TextBox();
            this.btnLoadSpName = new System.Windows.Forms.Button();
            this.grbLoadSP = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtKey = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmboDatabase = new System.Windows.Forms.ToolStripComboBox();
            this.gvSpName = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grbLoadSP.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSpName)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1040, 731);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1032, 702);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "From Configure";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbLoadSP);
            this.splitContainer1.Size = new System.Drawing.Size(1021, 689);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1021, 153);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.02086F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.97914F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel1.Controls.Add(this.btnSpBrowser, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCofigBrowser, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSpNamePath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtConnectionStringPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLoadSpName, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.36709F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.63291F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1013, 130);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // btnSpBrowser
            // 
            this.btnSpBrowser.BackColor = System.Drawing.Color.White;
            this.btnSpBrowser.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSpBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpBrowser.Location = new System.Drawing.Point(867, 4);
            this.btnSpBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.btnSpBrowser.Name = "btnSpBrowser";
            this.btnSpBrowser.Size = new System.Drawing.Size(142, 28);
            this.btnSpBrowser.TabIndex = 11;
            this.btnSpBrowser.Text = "Browser";
            this.btnSpBrowser.UseVisualStyleBackColor = false;
            this.btnSpBrowser.Click += new System.EventHandler(this.btnSpBrowser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Connection String ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "SP Name ";
            // 
            // btnCofigBrowser
            // 
            this.btnCofigBrowser.BackColor = System.Drawing.Color.White;
            this.btnCofigBrowser.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCofigBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCofigBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCofigBrowser.Location = new System.Drawing.Point(867, 50);
            this.btnCofigBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.btnCofigBrowser.Name = "btnCofigBrowser";
            this.btnCofigBrowser.Size = new System.Drawing.Size(142, 28);
            this.btnCofigBrowser.TabIndex = 12;
            this.btnCofigBrowser.Text = "Browser";
            this.btnCofigBrowser.UseVisualStyleBackColor = false;
            this.btnCofigBrowser.Click += new System.EventHandler(this.btnCofigBrowser_Click);
            // 
            // txtSpNamePath
            // 
            this.txtSpNamePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSpNamePath.Location = new System.Drawing.Point(125, 4);
            this.txtSpNamePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpNamePath.Name = "txtSpNamePath";
            this.txtSpNamePath.Size = new System.Drawing.Size(734, 22);
            this.txtSpNamePath.TabIndex = 1;
            this.txtSpNamePath.Text = "D:\\GitScm\\apollo\\apollo-agent\\Website\\SpNames.xml";
            this.txtSpNamePath.TextChanged += new System.EventHandler(this.txtSpNamePath_TextChanged);
            // 
            // txtConnectionStringPath
            // 
            this.txtConnectionStringPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectionStringPath.Location = new System.Drawing.Point(125, 50);
            this.txtConnectionStringPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtConnectionStringPath.Name = "txtConnectionStringPath";
            this.txtConnectionStringPath.Size = new System.Drawing.Size(734, 22);
            this.txtConnectionStringPath.TabIndex = 3;
            this.txtConnectionStringPath.Text = "D:\\GitScm\\apollo\\apollo-agent\\Website\\Web.ConnectionString.config";
            this.txtConnectionStringPath.TextChanged += new System.EventHandler(this.txtConnectionStringPath_TextChanged);
            // 
            // btnLoadSpName
            // 
            this.btnLoadSpName.BackColor = System.Drawing.Color.White;
            this.btnLoadSpName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadSpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadSpName.Location = new System.Drawing.Point(125, 97);
            this.btnLoadSpName.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadSpName.Name = "btnLoadSpName";
            this.btnLoadSpName.Size = new System.Drawing.Size(189, 29);
            this.btnLoadSpName.TabIndex = 6;
            this.btnLoadSpName.Text = "Load";
            this.btnLoadSpName.UseVisualStyleBackColor = false;
            this.btnLoadSpName.Click += new System.EventHandler(this.btnLoadSpName_Click);
            // 
            // grbLoadSP
            // 
            this.grbLoadSP.Controls.Add(this.toolStrip1);
            this.grbLoadSP.Controls.Add(this.gvSpName);
            this.grbLoadSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbLoadSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLoadSP.Location = new System.Drawing.Point(0, 0);
            this.grbLoadSP.Margin = new System.Windows.Forms.Padding(4);
            this.grbLoadSP.Name = "grbLoadSP";
            this.grbLoadSP.Padding = new System.Windows.Forms.Padding(4);
            this.grbLoadSP.Size = new System.Drawing.Size(1021, 531);
            this.grbLoadSP.TabIndex = 10;
            this.grbLoadSP.TabStop = false;
            this.grbLoadSP.Text = "Load Store Procedure";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Silver;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtKey,
            this.toolStripLabel2,
            this.cmboDatabase});
            this.toolStrip1.Location = new System.Drawing.Point(4, 19);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1013, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel1.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(300, 25);
            this.txtKey.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel2.Text = "Database";
            // 
            // cmboDatabase
            // 
            this.cmboDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmboDatabase.Name = "cmboDatabase";
            this.cmboDatabase.Size = new System.Drawing.Size(200, 25);
            this.cmboDatabase.SelectedIndexChanged += new System.EventHandler(this.cmboDatabase_SelectedIndexChanged);
            // 
            // gvSpName
            // 
            this.gvSpName.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSpName.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSpName.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvSpName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSpName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSpName.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvSpName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvSpName.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.gvSpName.Location = new System.Drawing.Point(4, 19);
            this.gvSpName.Margin = new System.Windows.Forms.Padding(4);
            this.gvSpName.MultiSelect = false;
            this.gvSpName.Name = "gvSpName";
            this.gvSpName.ReadOnly = true;
            this.gvSpName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gvSpName.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gvSpName.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSpName.Size = new System.Drawing.Size(1013, 508);
            this.gvSpName.TabIndex = 8;
            this.gvSpName.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSpName_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Key";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "SP Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1040, 731);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Generate DTO";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grbLoadSP.ResumeLayout(false);
            this.grbLoadSP.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSpName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnLoadSpName;
        private System.Windows.Forms.TextBox txtConnectionStringPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSpNamePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gvSpName;
        private System.Windows.Forms.GroupBox grbLoadSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnSpBrowser;
        private System.Windows.Forms.Button btnCofigBrowser;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtKey;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cmboDatabase;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

