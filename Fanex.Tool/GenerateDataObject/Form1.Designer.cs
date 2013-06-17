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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCofigBrowser = new System.Windows.Forms.Button();
            this.btnSpBrowser = new System.Windows.Forms.Button();
            this.grbLoadSP = new System.Windows.Forms.GroupBox();
            this.cmboDatabase = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gvSpName = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLoadSpName = new System.Windows.Forms.Button();
            this.txtConnectionStringPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSpNamePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grbLoadSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSpName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(780, 594);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(772, 568);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "From Configure";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCofigBrowser
            // 
            this.btnCofigBrowser.Location = new System.Drawing.Point(681, 64);
            this.btnCofigBrowser.Name = "btnCofigBrowser";
            this.btnCofigBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnCofigBrowser.TabIndex = 12;
            this.btnCofigBrowser.Text = "Browser";
            this.btnCofigBrowser.UseVisualStyleBackColor = true;
            this.btnCofigBrowser.Click += new System.EventHandler(this.btnCofigBrowser_Click);
            // 
            // btnSpBrowser
            // 
            this.btnSpBrowser.Location = new System.Drawing.Point(681, 16);
            this.btnSpBrowser.Name = "btnSpBrowser";
            this.btnSpBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnSpBrowser.TabIndex = 11;
            this.btnSpBrowser.Text = "Browser";
            this.btnSpBrowser.UseVisualStyleBackColor = true;
            this.btnSpBrowser.Click += new System.EventHandler(this.btnSpBrowser_Click);
            // 
            // grbLoadSP
            // 
            this.grbLoadSP.Controls.Add(this.cmboDatabase);
            this.grbLoadSP.Controls.Add(this.label3);
            this.grbLoadSP.Controls.Add(this.gvSpName);
            this.grbLoadSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbLoadSP.Location = new System.Drawing.Point(0, 0);
            this.grbLoadSP.Name = "grbLoadSP";
            this.grbLoadSP.Size = new System.Drawing.Size(766, 419);
            this.grbLoadSP.TabIndex = 10;
            this.grbLoadSP.TabStop = false;
            this.grbLoadSP.Text = "Load Store Procedure";
            // 
            // cmboDatabase
            // 
            this.cmboDatabase.FormattingEnabled = true;
            this.cmboDatabase.Location = new System.Drawing.Point(123, 19);
            this.cmboDatabase.Name = "cmboDatabase";
            this.cmboDatabase.Size = new System.Drawing.Size(164, 21);
            this.cmboDatabase.TabIndex = 7;
            this.cmboDatabase.SelectedIndexChanged += new System.EventHandler(this.cmboDatabase_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Database";
            // 
            // gvSpName
            // 
            this.gvSpName.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSpName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSpName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.gvSpName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gvSpName.Location = new System.Drawing.Point(3, 62);
            this.gvSpName.Name = "gvSpName";
            this.gvSpName.Size = new System.Drawing.Size(760, 354);
            this.gvSpName.TabIndex = 8;
            this.gvSpName.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSpName_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Key";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "SP Name";
            this.Column2.Name = "Column2";
            // 
            // btnLoadSpName
            // 
            this.btnLoadSpName.Location = new System.Drawing.Point(122, 105);
            this.btnLoadSpName.Name = "btnLoadSpName";
            this.btnLoadSpName.Size = new System.Drawing.Size(91, 23);
            this.btnLoadSpName.TabIndex = 6;
            this.btnLoadSpName.Text = "Load";
            this.btnLoadSpName.UseVisualStyleBackColor = true;
            this.btnLoadSpName.Click += new System.EventHandler(this.btnLoadSpName_Click);
            // 
            // txtConnectionStringPath
            // 
            this.txtConnectionStringPath.Location = new System.Drawing.Point(122, 67);
            this.txtConnectionStringPath.Name = "txtConnectionStringPath";
            this.txtConnectionStringPath.Size = new System.Drawing.Size(528, 20);
            this.txtConnectionStringPath.TabIndex = 3;
            this.txtConnectionStringPath.Text = "D:\\GitScm\\apollo\\apollo-agent\\Website\\Web.ConnectionString.config";
            this.txtConnectionStringPath.TextChanged += new System.EventHandler(this.txtConnectionStringPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Connection String ";
            // 
            // txtSpNamePath
            // 
            this.txtSpNamePath.Location = new System.Drawing.Point(122, 20);
            this.txtSpNamePath.Name = "txtSpNamePath";
            this.txtSpNamePath.Size = new System.Drawing.Size(528, 20);
            this.txtSpNamePath.TabIndex = 1;
            this.txtSpNamePath.Text = "D:\\GitScm\\apollo\\apollo-agent\\Website\\SpNames.xml";
            this.txtSpNamePath.TextChanged += new System.EventHandler(this.txtSpNamePath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SP Name ";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnCofigBrowser);
            this.splitContainer1.Panel1.Controls.Add(this.txtSpNamePath);
            this.splitContainer1.Panel1.Controls.Add(this.btnSpBrowser);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadSpName);
            this.splitContainer1.Panel1.Controls.Add(this.txtConnectionStringPath);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbLoadSP);
            this.splitContainer1.Size = new System.Drawing.Size(766, 562);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 594);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.grbLoadSP.ResumeLayout(false);
            this.grbLoadSP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSpName)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmboDatabase;
        private System.Windows.Forms.Button btnLoadSpName;
        private System.Windows.Forms.TextBox txtConnectionStringPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSpNamePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gvSpName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grbLoadSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnSpBrowser;
        private System.Windows.Forms.Button btnCofigBrowser;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

