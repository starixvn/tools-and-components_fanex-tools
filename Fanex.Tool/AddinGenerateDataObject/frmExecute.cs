// --------------------------------------------------------------------------
// <copyright file="frmExecute.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.6.17">Created</change>
// </history>

namespace AddinGenerateDataObject
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Caching;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using FastColoredTextBoxNS;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System.Xml.Linq;
    using System.Linq;
    using System.Collections;

    /// <summary>
    /// 
    /// </summary>
    public partial class frmExecute : Form
    {
        //styles
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        /// <summary>
        /// cache manager
        /// </summary>
        private static volatile ObjectCache cacheManager;

        /// <summary>
        /// 
        /// </summary>
        protected static object syncRoot = new object();

        /// <summary>
        /// Gets the memory cache instance
        /// </summary>
        protected ObjectCache CacheManager
        {
            /// <summary>
            /// Cache manager singleton
            /// </summary>
            get
            {
                if (cacheManager == null)
                {
                    lock (syncRoot)
                    {
                        if (cacheManager == null)
                        {
                            cacheManager = MemoryCache.Default;
                        }
                    }
                }

                return cacheManager;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public frmExecute()
        {
            InitializeComponent();
        }

        private List<ProjectModel> _listProjectModel = new List<ProjectModel>();

        private void frmExecute_Load(object sender, EventArgs e)
        {
            string settingKey = (string)this.CacheManager.Get(Constant.NameKey);
            string projectName = (string)this.CacheManager.Get(Constant.ProjectNameKey);

            var settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(Constant.DbSettingCacheKeyName);

            var dbSetting = settingDataSource[settingKey];

            _listProjectModel = (List<ProjectModel>)this.CacheManager.Get(Constant.ProjectKey);

            BindDataToProjectCombo(_listProjectModel);

            txtSettingKey.Text = settingKey;
            txtSettingKey.Tag = settingKey;

            //default
            txtClassName.Text = dbSetting.SpName;
            txtNamespace.Text = projectName;

            //BindParameterToGridView(dbSetting);

            // disable
            gvColumnInfo.Enabled = false;
            //btnGenerate.Enabled = false;

            txtMappingPreview.Language = Language.HTML;
        }

        /// <summary>
        /// Load data from list project to combobox
        /// </summary>
        /// <param name="listProject"></param>
        private void BindDataToProjectCombo(List<ProjectModel> listProject)
        {
            foreach (var item in listProject)
            {
                cmboProject.Items.Add(item);
                if (item.IsActive)
                {
                    cmboProject.SelectedItem = item;
                }
            }
        }

        private void BindParameterToGridView(DbSetting dbSetting)
        {
            string spName = dbSetting.SpName;
            SqlDatabase sqlDataBase = new SqlDatabase(dbSetting.ConnectionString);

            DataTable resultField = new DataTable();
            resultField.Columns.Add("Properties");
            resultField.Columns.Add("SpField");
            resultField.Columns.Add("Type");
            DataTable table = new DataTable();
            DbCommand dbCommand = sqlDataBase.GetStoredProcCommand(spName);
            sqlDataBase.DiscoverParameters(dbCommand);
            int countParam = dbCommand.Parameters.Count;
            List<SqlParameter> listSqlParameter = new List<SqlParameter>();
            for (int i = 0; i < countParam; i++)
            {
                SqlParameter sql = (SqlParameter)dbCommand.Parameters[i];
                bool isNotReturnValue = !sql.ParameterName.Equals("@RETURN_VALUE");
                if (isNotReturnValue)
                {
                    gvParameter.Rows.Add(sql.ParameterName, sql.DbType.ToString());
                }
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                gvColumnInfo.Rows.Clear();

                string settingKey = txtSettingKey.Tag.ToString();

                var settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(Constant.DbSettingCacheKeyName);

                var dbSetting = settingDataSource[settingKey];

                SqlDatabase sqlDataBase = new SqlDatabase(dbSetting.ConnectionString);
                DbCommand dbCommand = sqlDataBase.GetStoredProcCommand(dbSetting.SpName);
                sqlDataBase.DiscoverParameters(dbCommand);

                foreach (DataGridViewRow itemRow in gvParameter.Rows)
                {
                    if (itemRow.Cells[0].Value != null)
                    {
                        var parameter = itemRow.Cells[0].Value.ToString().Trim();
                        var value = itemRow.Cells[2].Value != null ? itemRow.Cells[2].Value.ToString().Trim() : null;

                        sqlDataBase.SetParameterValue(dbCommand, parameter, value);
                    }
                }

                var table = sqlDataBase.ExecuteDataSet(dbCommand).Tables[0];

                foreach (DataColumn item in table.Columns)
                {
                    string mappingName = string.Empty;
                    string columnName = item.ColumnName;
                    if (dbSetting.CustomMappingColumns.Contains(columnName))
                    {
                        mappingName = dbSetting.CustomMappingColumns[columnName] as string;
                    }

                    gvColumnInfo.Rows.Add(columnName, mappingName, item.DataType.Name);
                }

                txtPreview.Text = GenerateClass(txtNamespace.Text, txtClassName.Text);
                txtMappingPreview.Text = GenerateMappingPreview();

                // enable
                gvColumnInfo.Enabled = true;
                //btnGenerate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GenerateMappingPreview()
        {
            try
            {
                string _format = "<Mapping DataField=\"{0}\" ObjectProperty=\"{1}\"/>";
                StringBuilder strSource = new StringBuilder();
                foreach (DataGridViewRow item in gvColumnInfo.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        string properties = item.Cells[0].Value.ToString();
                        string mapping = item.Cells[1].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(mapping))
                        {
                            strSource.Append(string.Format(_format, properties, mapping) + Environment.NewLine);
                        }
                    }
                }

                return strSource.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Uppercase the first letter of a word
        /// </summary>
        /// <returns></returns>
        private string UpperCaseFirstCharacter(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]).ToString() + s.Substring(1).ToLower();
        }

        private string GetUserName()
        {
            string userName = Environment.UserName;
            if (userName.IndexOf('.') > 0)
            {
                string[] s = userName.Split('.');
                return UpperCaseFirstCharacter(s[0]) + "." + UpperCaseFirstCharacter(s[1]);
            }

            return UpperCaseFirstCharacter(userName);
        }

        private string GenerateClass(string nameSpace, string className)
        {
            try
            {
                StringBuilder strSource = new StringBuilder();
                strSource.Append("// --------------------------------------------------------------------------\n");
                strSource.Append("// <copyright file=\"" + className + ".cs\" company=\"Nexcel Solutions Vietnam\">\n");
                strSource.Append("// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.\n");
                strSource.Append("// </copyright>\n");
                strSource.Append("// --------------------------------------------------------------------------\n");
                strSource.Append("// <history>\n");
                strSource.Append("//   <change who=\"" + GetUserName() + "\" date=\"" + DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "\">Created</change>\n");
                strSource.Append("// </history>\n\n");

                strSource.Append("using System;\n\n");
                if (nameSpace != string.Empty)
                {
                    strSource.Append("namespace " + nameSpace + "\n");
                }
                strSource.Append("{\n");
                strSource.Append("\tpublic class " + className + "\n");
                strSource.Append("{\n");
                int count = 0;

                foreach (DataGridViewRow item in gvColumnInfo.Rows)
                {
                    count++;

                    if (item.Cells[0].Value != null)
                    {
                        string type = item.Cells[2].Value.ToString();
                        type = MappingDataType.GetTypeAlias(type);
                        string properties = item.Cells[0].Value.ToString();
                        string mapping = item.Cells[1].Value.ToString();

                        properties = string.IsNullOrWhiteSpace(mapping) ? properties : mapping;

                        if (count == gvColumnInfo.Rows.Count)
                        {
                            strSource.Append("\t\t public " + type + " " + properties + " { get; set; }");
                        }
                        else
                        {
                            strSource.Append("\t\t public " + type + " " + properties + " { get; set; }\n\n");
                        }
                    }
                }
                strSource.Append("\n\t}\n");
                if (nameSpace != string.Empty)
                {
                    strSource.Append("}");
                }
                return strSource.ToString();
            }
            catch
            {
                return null;
            }
        }

        void SaveClassFile(string strSource, string nameClass, string filePath)
        {
            FileStream fileStream = new FileStream(filePath + "\\" + nameClass + ".cs", FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine(strSource.ToString());
            writer.Close();
            fileStream.Close();
        }

        private void txtNamespace_TextChanged(object sender, EventArgs e)
        {
            txtPreview.Text = GenerateClass(txtNamespace.Text, txtClassName.Text);
        }

        private void txtClassName_TextChanged(object sender, EventArgs e)
        {
            txtPreview.Text = GenerateClass(txtNamespace.Text, txtClassName.Text);
        }

        private void gvColumnInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            txtPreview.Text = GenerateClass(txtNamespace.Text, txtClassName.Text);
            txtMappingPreview.Text = GenerateMappingPreview();
        }

        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            txtPreview.LeftBracket = '(';
            txtPreview.RightBracket = ')';
            txtPreview.LeftBracket2 = '\x0';
            txtPreview.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);

            //string highlighting
            e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        private void txtPreview_TextChanging(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPreview.Text);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClassName.Text != string.Empty)
                {
                    DialogResult dr = folderBrowserDialog1.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        string className = txtClassName.Text.Trim();
                        string nameSpace = txtNamespace.Text.Trim();

                        string strClassGenerate = GenerateClass(nameSpace, className);

                        SaveClassFile(strClassGenerate, className, folderBrowserDialog1.SelectedPath);

                        MessageBox.Show("OK");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Clas name invalid");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void copyAllToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtMappingPreview.Text);
        }

        private void cmboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectModel project = (ProjectModel)cmboProject.SelectedItem;

            if (project != null)
            {
                if (!string.IsNullOrWhiteSpace(project.ConfigPath)
                    && !string.IsNullOrWhiteSpace(project.ResourcePath))
                {
                    txtSettingKey.AutoCompleteCustomSource = AddDataSourceToCache(project.ResourcePath, project.ConfigPath);
                }
            }
        }

        /// <summary>
        /// Returns true if the given file path is a folder.
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>True if a folder</returns>
        protected bool IsFolder(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
        }


        /// <summary>
        /// Get all db setting configuration from the data source and than add to cache
        /// </summary>
        /// <param name="resourcePath">path to resource file for folder</param>        
        /// <param name="connectionPath">path to connection string</param>   
        protected AutoCompleteStringCollection AddDataSourceToCache(string resourcePath, string connectionPath)
        {
            AutoCompleteStringCollection autoList = new AutoCompleteStringCollection();

            lock (syncRoot)
            {
                string[] listOfResouces;
                if (IsFolder(resourcePath))
                {
                    // just get xml file
                    listOfResouces = Directory.GetFiles(resourcePath);
                    listOfResouces = listOfResouces.ToList<string>()
                         .Where(path
                             => Path.GetExtension(path).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                         .Select(s => s).ToArray<string>();
                }
                else
                {
                    listOfResouces = new string[] { resourcePath };
                }

                int resourceLength = listOfResouces.Length;

                try
                {
                    var settingDataSource = new Dictionary<string, DbSetting>();

                    for (int i = 0; i < resourceLength; i++)
                    {
                        var document = XDocument.Load(GetResourcePath(listOfResouces[i]));
                        var functions = document.Descendants("Function");

                        foreach (var functionItem in functions)
                        {
                            var settingList = functionItem.Descendants("Setting");
                            foreach (var setting in settingList)
                            {
                                var databaseSetting = new DbSetting
                                {
                                    SpName = setting.Attribute("SpName").Value.Trim(),
                                    DbName = setting.Attribute("DbName").Value.Trim(),
                                    ExecuteTimeOut = setting.Attribute("TimeOut") != null ? Convert.ToInt32(setting.Attribute("TimeOut").Value.Trim()) : 120
                                };

                                // Add custom column for SpName
                                var customColumList = setting.Descendants("Mapping");
                                Hashtable customMapping = new Hashtable();
                                foreach (var item in customColumList)
                                {
                                    string dataField = item.Attribute("DataField").Value.Trim();
                                    string objectProperty = item.Attribute("ObjectProperty").Value.Trim();
                                    customMapping.Add(dataField, objectProperty);
                                }

                                databaseSetting.CustomMappingColumns = customMapping;

                                // check connection string whether require to decrypt or not
                                var isEncryptedConn = IsEncryptedConnection(setting);
                                databaseSetting.ConnectionString = ConnectionStringGetter.GetConnectionString(connectionPath, databaseSetting.DbName, isEncryptedConn);

                                var key = setting.Attribute("Key").Value.Trim();
                                if (!string.IsNullOrWhiteSpace(key))
                                {
                                    if (databaseSetting.IsValid())
                                    {
                                        if (!settingDataSource.ContainsKey(key))
                                        {
                                            settingDataSource.Add(key, databaseSetting);
                                        }
                                    }
                                }

                                autoList.Add(key);
                                this.CacheManager.Set(Constant.DbSettingCacheKeyName, settingDataSource, new CacheItemPolicy());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return autoList;
            }
        }

        /// <summary>
        /// get resource path point to external config data source
        /// </summary>
        /// <param name="resourcePath">NameValueCollection config</param>
        /// <returns>return absolute resource path</returns>
        private string GetResourcePath(string resourcePath)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
            {
                throw new NoNullAllowedException();
            }

            // check root path http://msdn.microsoft.com/query/dev10.query?appId=Dev10IDEF1&l=EN-US&k=k%28SYSTEM.IO.PATH.ISPATHROOTED%29;k%28SOLUTIONITEMSPROJECT%29;k%28TargetFrameworkMoniker-%22.NETFRAMEWORK%2cVERSION%3dV4.0%22%29;k%28DevLang-CSHARP%29&rd=true
            if (!Path.IsPathRooted(resourcePath))
            {
                if (resourcePath.StartsWith("~"))
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        resourcePath = System.Web.HttpContext.Current.Server.MapPath(resourcePath);
                    }
                }
                else
                {
                    // if got the problem with window service
                    // see http://haacked.com/archive/2004/06/29/706.aspx
                    resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourcePath);
                }
            }

            return resourcePath;
        }


        /// <summary>
        /// check whether connection was encrypted or not
        /// </summary>
        /// <param name="element">XElement element</param>
        /// <returns>bool value, true if connection requires encrypted</returns>
        protected static bool IsEncryptedConnection(XElement element)
        {
            var attr = element.Attribute("Plain");
            var isPlaintConnection = attr != null
                           && (attr.Value.Equals("1") || attr.Value.Equals("true", StringComparison.OrdinalIgnoreCase));

            return !isPlaintConnection;
        }

        private void txtSpName_TextChanged(object sender, EventArgs e)
        {
            var settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(Constant.DbSettingCacheKeyName);
            var settingKey = txtSettingKey.Text.Trim();

            if (settingDataSource.ContainsKey(settingKey))
            {
                gvParameter.Rows.Clear();

                var dbSetting = settingDataSource[settingKey];

                txtSettingKey.Text = dbSetting.SpName;
                txtSettingKey.Tag = settingKey;

                //default
                txtClassName.Text = dbSetting.SpName;
                txtNamespace.Text = ((ProjectModel)cmboProject.SelectedItem).Name;

                BindParameterToGridView(dbSetting);

                // disable
                gvColumnInfo.Rows.Clear();
                gvColumnInfo.Enabled = false;
                //btnGenerate.Enabled = false;

                txtMappingPreview.Language = Language.HTML;
            }
        }

        private void hashTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUnCheckMenuItem();
            hashTableToolStripMenuItem.Checked = true;

            StringBuilder str = new StringBuilder();
            str.Append("var param = new Hashtable();\n");
            foreach (DataGridViewRow item in gvParameter.Rows)
            {
                if (item.Cells[0].Value != null
                    && !string.IsNullOrWhiteSpace(item.Cells[0].Value.ToString()))
                {
                    string strKey = item.Cells[0].Value.ToString().TrimStart('@').ToLower();
                    str.Append("param.Add(\"" + strKey + "\", _" + strKey + ");\n");
                }
            }

            txtPreview.Text = str.ToString();
        }

        private void dynamicObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUnCheckMenuItem();
            dynamicObjectToolStripMenuItem.Checked = true;

            StringBuilder str = new StringBuilder();
            str.Append("var param = new\n");
            str.Append("{\n");
            foreach (DataGridViewRow item in gvParameter.Rows)
            {
                if (item.Cells[0].Value != null
                    && !string.IsNullOrWhiteSpace(item.Cells[0].Value.ToString()))
                {
                    string strKey = item.Cells[0].Value.ToString().TrimStart('@');
                    str.Append("\t\t" + strKey + " = _" + strKey.ToLower() + ",\n");
                }
            }
            str.Append("};");

            txtPreview.Text = str.ToString();
        }

        private void addParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUnCheckMenuItem();
            addParameterToolStripMenuItem.Checked = true;

            StringBuilder str = new StringBuilder();
            str.Append("using (ObjectDb db = new ObjectDb(\"" + txtSettingKey.Text + "\"))\n");
            str.Append("{\n");
            var settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(Constant.DbSettingCacheKeyName);
            var dbSetting = settingDataSource[txtSettingKey.Text];

            string spName = dbSetting.SpName;
            SqlDatabase sqlDataBase = new SqlDatabase(dbSetting.ConnectionString);

            DataTable resultField = new DataTable();
            resultField.Columns.Add("Properties");
            resultField.Columns.Add("SpField");
            resultField.Columns.Add("Type");
            DataTable table = new DataTable();
            DbCommand dbCommand = sqlDataBase.GetStoredProcCommand(spName);
            sqlDataBase.DiscoverParameters(dbCommand);
            int countParam = dbCommand.Parameters.Count;
            List<SqlParameter> listSqlParameter = new List<SqlParameter>();
            for (int i = 0; i < countParam; i++)
            {
                SqlParameter sql = (SqlParameter)dbCommand.Parameters[i];
                bool isNotReturnValue = !sql.ParameterName.Equals("@RETURN_VALUE");
                if (isNotReturnValue)
                {
                    if (sql.Direction == ParameterDirection.Input)
                    {
                        str.Append("\tdb.AddInParameter(\"" + sql.ParameterName.TrimStart('@') + "\", DbType." + sql.DbType.ToString() + ", _" + sql.ParameterName.TrimStart('@').ToLower() + ");\n");
                    }
                    else
                    {
                        str.Append("\tdb.AddOutParameter(\"" + sql.ParameterName.TrimStart('@') + "\", DbType." + sql.DbType.ToString() + ", _" + sql.ParameterName.TrimStart('@').ToLower() + ");\n");
                    }
                }
            }
            str.Append("}");

            txtPreview.Text = str.ToString();
        }

        private void criteriaClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUnCheckMenuItem();
            criteriaClassToolStripMenuItem.Checked = true;
            txtPreview.Text = GenerateCriteriaClass(txtNamespace.Text, txtClassName.Text);
        }

        private void SetUnCheckMenuItem()
        {
            criteriaClassToolStripMenuItem.Checked = false;
            hashTableToolStripMenuItem.Checked = false;
            dynamicObjectToolStripMenuItem.Checked = false;
            addParameterToolStripMenuItem.Checked = false;
            dTOClassToolStripMenuItem.Checked = false;
        }

        private string GenerateCriteriaClass(string nameSpace, string className)
        {
            try
            {
                StringBuilder strSource = new StringBuilder();
                strSource.Append("// --------------------------------------------------------------------------\n");
                strSource.Append("// <copyright file=\"" + className + ".cs\" company=\"Nexcel Solutions Vietnam\">\n");
                strSource.Append("// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.\n");
                strSource.Append("// </copyright>\n");
                strSource.Append("// --------------------------------------------------------------------------\n");
                strSource.Append("// <history>\n");
                strSource.Append("//   <change who=\"" + GetUserName() + "\" date=\"" + DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "\">Created</change>\n");
                strSource.Append("// </history>\n\n");

                strSource.Append("using System;\n");
                strSource.Append("using Fanex.NAP.Framework.Data;\n\n");
                if (nameSpace != string.Empty)
                {
                    strSource.Append("namespace " + nameSpace + " {\n\n");
                }
                strSource.Append("\tpublic class " + className + " : CriteriaBase {\n\n");

                foreach (DataGridViewRow item in gvParameter.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        string type = item.Cells[1].Value.ToString();
                        type = MappingDataType.GetTypeAlias(type);
                        string properties = item.Cells[0].Value.ToString().TrimStart('@');
                        string mapping = item.Cells[2].Value != null ? item.Cells[2].Value.ToString() : string.Empty;

                        if (!string.IsNullOrWhiteSpace(mapping))
                        {
                            strSource.Append("\t\t[SpParam(SqlParamName=\"" + properties + "\")]\n");
                            strSource.Append("\t\t public " + type + " " + mapping + " { get; set; }\n\n");
                        }
                        else
                        {
                            strSource.Append("\t\t public " + type + " " + properties + " { get; set; }\n\n");
                        }
                    }
                }

                strSource.Append("\t\t public override string GetSettingKey()\n");
                strSource.Append("\t\t {\n");
                strSource.Append("\t\t\t return \"" + txtSettingKey.Text + "\";\n");
                strSource.Append("\t\t }\n\n");
                strSource.Append("\t\t public override bool IsValid()\n");
                strSource.Append("\t\t {\n");
                strSource.Append("\t\t\t return true;\n");
                strSource.Append("\t\t }");
                strSource.Append("\n\t}\n");
                if (nameSpace != string.Empty)
                {
                    strSource.Append("}");
                }
                return strSource.ToString();
            }
            catch
            {
                return null;
            }
        }

        private void dTOClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUnCheckMenuItem();
            dTOClassToolStripMenuItem.Checked = true;
            txtPreview.Text = GenerateClass(txtNamespace.Text, txtClassName.Text);
        }
    }
}
