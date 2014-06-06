using FastColoredTextBoxNS;
namespace AddinGenerateDataObject
{
    partial class frmExecute
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecute));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboProject = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSettingKey = new System.Windows.Forms.TextBox();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gvParameter = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExecute = new System.Windows.Forms.Button();
            this.gvColumnInfo = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupPreview = new System.Windows.Forms.GroupBox();
            this.txtPreview = new FastColoredTextBoxNS.FastColoredTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cmboMode = new System.Windows.Forms.ToolStripComboBox();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewMappingGroup = new System.Windows.Forms.GroupBox();
            this.txtMappingPreview = new FastColoredTextBoxNS.FastColoredTextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.copyAllToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxParameter = new System.Windows.Forms.GroupBox();
            this.groupColumInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmboIndexTable = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvColumnInfo)).BeginInit();
            this.groupPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreview)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.previewMappingGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMappingPreview)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxParameter.SuspendLayout();
            this.groupColumInfo.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboProject);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSettingKey);
            this.groupBox1.Controls.Add(this.txtClassName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNamespace);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(429, 145);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cmboProject
            // 
            this.cmboProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.cmboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmboProject.FormattingEnabled = true;
            this.cmboProject.Location = new System.Drawing.Point(97, 20);
            this.cmboProject.Name = "cmboProject";
            this.cmboProject.Size = new System.Drawing.Size(290, 25);
            this.cmboProject.TabIndex = 8;
            this.cmboProject.SelectedIndexChanged += new System.EventHandler(this.cmboProject_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Project";
            // 
            // txtSettingKey
            // 
            this.txtSettingKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSettingKey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSettingKey.BackColor = System.Drawing.Color.White;
            this.txtSettingKey.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSettingKey.Location = new System.Drawing.Point(97, 49);
            this.txtSettingKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtSettingKey.Name = "txtSettingKey";
            this.txtSettingKey.Size = new System.Drawing.Size(290, 25);
            this.txtSettingKey.TabIndex = 6;
            this.txtSettingKey.TextChanged += new System.EventHandler(this.txtSpName_TextChanged);
            // 
            // txtClassName
            // 
            this.txtClassName.BackColor = System.Drawing.Color.White;
            this.txtClassName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClassName.Location = new System.Drawing.Point(97, 107);
            this.txtClassName.Margin = new System.Windows.Forms.Padding(4);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(290, 25);
            this.txtClassName.TabIndex = 2;
            this.txtClassName.TextChanged += new System.EventHandler(this.txtClassName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Class Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Setting Key";
            // 
            // txtNamespace
            // 
            this.txtNamespace.BackColor = System.Drawing.Color.White;
            this.txtNamespace.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamespace.Location = new System.Drawing.Point(97, 78);
            this.txtNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(290, 25);
            this.txtNamespace.TabIndex = 1;
            this.txtNamespace.TextChanged += new System.EventHandler(this.txtClassName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Namespace";
            // 
            // gvParameter
            // 
            this.gvParameter.AllowUserToAddRows = false;
            this.gvParameter.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Menu;
            this.gvParameter.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvParameter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvParameter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5,
            this.Column2,
            this.Column4});
            this.gvParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvParameter.Location = new System.Drawing.Point(3, 21);
            this.gvParameter.Margin = new System.Windows.Forms.Padding(4);
            this.gvParameter.Name = "gvParameter";
            this.gvParameter.ReadOnly = true;
            this.gvParameter.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvParameter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvParameter.Size = new System.Drawing.Size(425, 301);
            this.gvParameter.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FillWeight = 73.85786F;
            this.Column1.HeaderText = "Parameter";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Type";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.FillWeight = 152.2843F;
            this.Column2.HeaderText = "Db Type";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Output Parameter";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.btnExecute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExecute.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
            this.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecute.Location = new System.Drawing.Point(4, 488);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(137, 33);
            this.btnExecute.TabIndex = 4;
            this.btnExecute.Text = "Get Column Info";
            this.btnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // gvColumnInfo
            // 
            this.gvColumnInfo.AllowUserToAddRows = false;
            this.gvColumnInfo.AllowUserToDeleteRows = false;
            this.gvColumnInfo.AllowUserToResizeColumns = false;
            this.gvColumnInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Menu;
            this.gvColumnInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvColumnInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvColumnInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column3,
            this.dataGridViewTextBoxColumn2,
            this.Column6});
            this.tableLayoutPanel2.SetColumnSpan(this.gvColumnInfo, 2);
            this.gvColumnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvColumnInfo.Location = new System.Drawing.Point(4, 34);
            this.gvColumnInfo.Margin = new System.Windows.Forms.Padding(4);
            this.gvColumnInfo.Name = "gvColumnInfo";
            this.gvColumnInfo.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvColumnInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvColumnInfo.Size = new System.Drawing.Size(417, 236);
            this.gvColumnInfo.TabIndex = 5;
            this.gvColumnInfo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvColumnInfo_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.FillWeight = 72.68041F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 115;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column3.FillWeight = 72.68041F;
            this.Column3.HeaderText = "Mapping";
            this.Column3.Name = "Column3";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 65;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "     Nullable";
            this.Column6.Name = "Column6";
            this.Column6.Width = 90;
            // 
            // groupPreview
            // 
            this.groupPreview.Controls.Add(this.txtPreview);
            this.groupPreview.Controls.Add(this.menuStrip1);
            this.groupPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPreview.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPreview.Location = new System.Drawing.Point(441, 4);
            this.groupPreview.Margin = new System.Windows.Forms.Padding(4);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.SetRowSpan(this.groupPreview, 3);
            this.groupPreview.Size = new System.Drawing.Size(573, 521);
            this.groupPreview.TabIndex = 7;
            this.groupPreview.TabStop = false;
            // 
            // txtPreview
            // 
            this.txtPreview.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.txtPreview.BackBrush = null;
            this.txtPreview.CharHeight = 14;
            this.txtPreview.CharWidth = 8;
            this.txtPreview.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPreview.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.IsReplaceMode = false;
            this.txtPreview.Location = new System.Drawing.Point(4, 49);
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.Paddings = new System.Windows.Forms.Padding(0);
            this.txtPreview.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtPreview.Size = new System.Drawing.Size(565, 468);
            this.txtPreview.TabIndex = 6;
            this.txtPreview.Text = "fastColoredTextBox1";
            this.txtPreview.Zoom = 100;
            this.txtPreview.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtPreview_TextChanging);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmboMode,
            this.copyToolStripMenuItem,
            this.addToProjectToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 22);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(565, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cmboMode
            // 
            this.cmboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboMode.Items.AddRange(new object[] {
            "DTO Class",
            "Criteria Class",
            "Dynamic Object",
            "Hash Table",
            "Add Parameter",
            "Read IDataReader"});
            this.cmboMode.Name = "cmboMode";
            this.cmboMode.Size = new System.Drawing.Size(121, 23);
            this.cmboMode.SelectedIndexChanged += new System.EventHandler(this.comboMode_SelectedtIndexChanged);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(135, 23);
            this.copyToolStripMenuItem.Text = "Copy To Clipboard";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // addToProjectToolStripMenuItem
            // 
            this.addToProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addToProjectToolStripMenuItem.Image")));
            this.addToProjectToolStripMenuItem.Name = "addToProjectToolStripMenuItem";
            this.addToProjectToolStripMenuItem.Size = new System.Drawing.Size(114, 23);
            this.addToProjectToolStripMenuItem.Text = "Add To Project";
            this.addToProjectToolStripMenuItem.Click += new System.EventHandler(this.addToProjectToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
            this.saveToolStripMenuItem.Text = "Save File";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // previewMappingGroup
            // 
            this.previewMappingGroup.Controls.Add(this.txtMappingPreview);
            this.previewMappingGroup.Controls.Add(this.menuStrip2);
            this.previewMappingGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewMappingGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewMappingGroup.Location = new System.Drawing.Point(441, 533);
            this.previewMappingGroup.Margin = new System.Windows.Forms.Padding(4);
            this.previewMappingGroup.Name = "previewMappingGroup";
            this.previewMappingGroup.Padding = new System.Windows.Forms.Padding(4);
            this.previewMappingGroup.Size = new System.Drawing.Size(573, 296);
            this.previewMappingGroup.TabIndex = 8;
            this.previewMappingGroup.TabStop = false;
            this.previewMappingGroup.Text = "Preview Mapping";
            // 
            // txtMappingPreview
            // 
            this.txtMappingPreview.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtMappingPreview.BackBrush = null;
            this.txtMappingPreview.CharHeight = 14;
            this.txtMappingPreview.CharWidth = 8;
            this.txtMappingPreview.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMappingPreview.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtMappingPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMappingPreview.IsReplaceMode = false;
            this.txtMappingPreview.Location = new System.Drawing.Point(4, 46);
            this.txtMappingPreview.Name = "txtMappingPreview";
            this.txtMappingPreview.Paddings = new System.Windows.Forms.Padding(0);
            this.txtMappingPreview.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtMappingPreview.Size = new System.Drawing.Size(565, 246);
            this.txtMappingPreview.TabIndex = 7;
            this.txtMappingPreview.Zoom = 100;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAllToClipboardToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(4, 22);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip2.Size = new System.Drawing.Size(565, 24);
            this.menuStrip2.TabIndex = 8;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // copyAllToClipboardToolStripMenuItem
            // 
            this.copyAllToClipboardToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyAllToClipboardToolStripMenuItem.Image")));
            this.copyAllToClipboardToolStripMenuItem.Name = "copyAllToClipboardToolStripMenuItem";
            this.copyAllToClipboardToolStripMenuItem.Size = new System.Drawing.Size(152, 20);
            this.copyAllToClipboardToolStripMenuItem.Text = "Copy All To Clipboard";
            this.copyAllToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyAllToClipboardToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.02554F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.97446F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxParameter, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupColumInfo, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupPreview, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.previewMappingGroup, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnExecute, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.54762F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.45238F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1018, 854);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // groupBoxParameter
            // 
            this.groupBoxParameter.Controls.Add(this.gvParameter);
            this.groupBoxParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxParameter.Location = new System.Drawing.Point(3, 156);
            this.groupBoxParameter.Name = "groupBoxParameter";
            this.groupBoxParameter.Size = new System.Drawing.Size(431, 325);
            this.groupBoxParameter.TabIndex = 7;
            this.groupBoxParameter.TabStop = false;
            this.groupBoxParameter.Text = "Parameter Infomation";
            // 
            // groupColumInfo
            // 
            this.groupColumInfo.Controls.Add(this.tableLayoutPanel2);
            this.groupColumInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupColumInfo.Location = new System.Drawing.Point(3, 532);
            this.groupColumInfo.Name = "groupColumInfo";
            this.groupColumInfo.Size = new System.Drawing.Size(431, 298);
            this.groupColumInfo.TabIndex = 7;
            this.groupColumInfo.TabStop = false;
            this.groupColumInfo.Text = "Column Information";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 319F));
            this.tableLayoutPanel2.Controls.Add(this.gvColumnInfo, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmboIndexTable, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.27273F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.72727F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(425, 274);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // cmboIndexTable
            // 
            this.cmboIndexTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.cmboIndexTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmboIndexTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboIndexTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmboIndexTable.FormattingEnabled = true;
            this.cmboIndexTable.Location = new System.Drawing.Point(109, 3);
            this.cmboIndexTable.Name = "cmboIndexTable";
            this.cmboIndexTable.Size = new System.Drawing.Size(313, 25);
            this.cmboIndexTable.TabIndex = 6;
            this.cmboIndexTable.Visible = false;
            this.cmboIndexTable.SelectedIndexChanged += new System.EventHandler(this.cmboIndexTable_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 30);
            this.label5.TabIndex = 7;
            this.label5.Text = "Object Index";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 833);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(1018, 21);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(500, 16);
            this.toolStripStatusLabel1.Text = "Copyright by R&&D Team © 2010 Nexcel Solutions Co., Ltd. All Rights Reserved";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmExecute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1018, 854);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmExecute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate";
            this.Load += new System.EventHandler(this.frmExecute_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvColumnInfo)).EndInit();
            this.groupPreview.ResumeLayout(false);
            this.groupPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreview)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.previewMappingGroup.ResumeLayout(false);
            this.previewMappingGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMappingPreview)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxParameter.ResumeLayout(false);
            this.groupColumInfo.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gvParameter;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.DataGridView gvColumnInfo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupPreview;
        private System.Windows.Forms.GroupBox previewMappingGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private FastColoredTextBox txtPreview;
        private FastColoredTextBox txtMappingPreview;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyAllToClipboardToolStripMenuItem;
        private System.Windows.Forms.TextBox txtSettingKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboProject;
        private System.Windows.Forms.GroupBox groupColumInfo;
        private System.Windows.Forms.GroupBox groupBoxParameter;
        private System.Windows.Forms.ToolStripComboBox cmboMode;
        private System.Windows.Forms.ComboBox cmboIndexTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem addToProjectToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
    }
}