namespace AddinGenerateDataObject
{
    partial class main
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbLoadSP = new System.Windows.Forms.GroupBox();
            this.gvSpName = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtKey = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmboDatabase = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbLoadSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSpName)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbLoadSP
            // 
            this.grbLoadSP.Controls.Add(this.gvSpName);
            this.grbLoadSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbLoadSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLoadSP.Location = new System.Drawing.Point(0, 0);
            this.grbLoadSP.Margin = new System.Windows.Forms.Padding(4);
            this.grbLoadSP.Name = "grbLoadSP";
            this.grbLoadSP.Padding = new System.Windows.Forms.Padding(4);
            this.grbLoadSP.Size = new System.Drawing.Size(904, 328);
            this.grbLoadSP.TabIndex = 11;
            this.grbLoadSP.TabStop = false;
            // 
            // gvSpName
            // 
            this.gvSpName.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSpName.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gvSpName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSpName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
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
            this.gvSpName.Size = new System.Drawing.Size(896, 305);
            this.gvSpName.TabIndex = 8;
            this.gvSpName.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSpName_CellDoubleClick);
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
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Silver;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtKey,
            this.toolStripLabel2,
            this.cmboDatabase});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(904, 25);
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
            this.txtKey.TextChanged += new System.EventHandler(this.txtKey_TextChanged);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbLoadSP);
            this.splitContainer1.Size = new System.Drawing.Size(904, 372);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 12;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "main";
            this.Size = new System.Drawing.Size(904, 372);
            this.Load += new System.EventHandler(this.main_Load);
            this.grbLoadSP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSpName)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbLoadSP;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtKey;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cmboDatabase;
        private System.Windows.Forms.DataGridView gvSpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}
