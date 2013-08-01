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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using FastColoredTextBoxNS;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    /// <summary>
    /// 
    /// </summary>
    public partial class frmExecute : Form
    {
        #region Style code

        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        #endregion

        #region Cache

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

        #endregion

        #region Member

        private List<ProjectModel> _listProjectModel = new List<ProjectModel>();

        private Dictionary<string, DbSetting> _settingDataSource = new Dictionary<string, DbSetting>();

        private DataSet dataSetColumnInfo = new DataSet();

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        public frmExecute()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmExecute_Load(object sender, EventArgs e)
        {
            // get cache
            string settingKey = (string)this.CacheManager.Get(Constant.NameKey);
            string projectName = (string)this.CacheManager.Get(Constant.ProjectNameKey);

            _settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(Constant.DbSettingCacheKeyName);
            _listProjectModel = (List<ProjectModel>)this.CacheManager.Get(Constant.ProjectKey);

            BindProjectToCombo(_listProjectModel, projectName);

            // setting key
            txtSettingKey.Text = settingKey;
            txtSettingKey.Tag = settingKey;

            // stored procedure name
            var dbSetting = _settingDataSource[settingKey];
            txtClassName.Text = dbSetting.SpName;
            txtNamespace.Text = projectName;

            // form setting
            gvColumnInfo.Enabled = false;
            txtMappingPreview.Language = Language.HTML;
            cmboMode.SelectedIndex = 0;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                gvColumnInfo.Rows.Clear();

                string settingKey = txtSettingKey.Tag.ToString();
                var dbSetting = _settingDataSource[settingKey];

                SqlDatabase sqlDataBase = new SqlDatabase(dbSetting.ConnectionString);
                DbCommand dbCommand = sqlDataBase.GetStoredProcCommand(dbSetting.SpName);
                sqlDataBase.DiscoverParameters(dbCommand);

                // get parameter
                foreach (DataGridViewRow itemRow in gvParameter.Rows)
                {
                    if (itemRow.Cells[0].Value != null)
                    {
                        var parameter = itemRow.Cells[0].Value.ToString().Trim();
                        var value = itemRow.Cells[2].Value != null ? itemRow.Cells[2].Value.ToString().Trim() : null;

                        sqlDataBase.SetParameterValue(dbCommand, parameter, value);
                    }
                }
                
                this.dataSetColumnInfo = sqlDataBase.ExecuteReader(dbCommand);

                cmboIndexTable.Items.Clear();
                for (int i = 0; i < this.dataSetColumnInfo.Tables.Count; i++)
                {
                    cmboIndexTable.Items.Add(i);
                }

                if (dataSetColumnInfo.Tables.Count > 1)
                {
                    label5.Visible = true;
                    cmboIndexTable.Visible = true;
                    cmboIndexTable.SelectedIndex = 0;
                }
                else
                {
                    label5.Visible = false;
                    cmboIndexTable.Visible = false;
                }

                var table = dataSetColumnInfo.Tables[0];
                BindingColumnInformation(table, dbSetting);

                // enable
                gvColumnInfo.Enabled = true;
                cmboMode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtClassName_TextChanged(object sender, EventArgs e)
        {
            if (cmboMode.Items.Count > 5)
            {
                if (cmboMode.SelectedIndex != 5)
                {
                    txtPreview.Text = GenerateDTOClass(txtNamespace.Text, txtClassName.Text, gvColumnInfo);
                    cmboMode.SelectedIndex = 0;
                }
                else
                {
                    txtPreview.Text = GenerateReadIDataReader(txtClassName.Text, gvColumnInfo);
                }
            }
        }

        private void gvColumnInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (cmboMode.Items.Count > 5)
            {
                if (cmboMode.SelectedIndex != 5)
                {
                    txtPreview.Text = GenerateDTOClass(txtNamespace.Text, txtClassName.Text, gvColumnInfo);
                    cmboMode.SelectedIndex = 0;
                }
                else
                {
                    txtPreview.Text = GenerateReadIDataReader(txtClassName.Text, gvColumnInfo);
                }

                txtMappingPreview.Text = GenerateMappingPreview(gvColumnInfo);
            }
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

                        string strClassGenerate = GenerateDTOClass(nameSpace, className, gvColumnInfo);

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
                    txtSettingKey.AutoCompleteCustomSource = BindingDataToAutoComple(project.ResourcePath, project.ConfigPath);
                }
                else
                {
                    MessageBox.Show("Can not find Application config or SP name file !");
                    txtSettingKey.Clear();
                }

                if (!_settingDataSource.ContainsKey(txtSettingKey.Text.Trim()))
                {
                    txtSettingKey.Text = string.Empty;
                    txtNamespace.Text = project.Name;
                    txtClassName.Text = string.Empty;
                    gvParameter.Rows.Clear();
                    gvColumnInfo.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show("Can not find Application config or SP name file !");
                txtSettingKey.Clear();
            }
        }

        private void txtSpName_TextChanged(object sender, EventArgs e)
        {
            var settingKey = txtSettingKey.Text.Trim();

            if (_settingDataSource.ContainsKey(settingKey))
            {
                gvParameter.Rows.Clear();

                var dbSetting = _settingDataSource[settingKey];

                groupBoxParameter.Text = "Parameters of SP : " + dbSetting.SpName;
                groupColumInfo.Text = "Columns of SP : " + dbSetting.SpName;

                txtSettingKey.Text = settingKey;
                txtSettingKey.Tag = settingKey;

                //default
                txtClassName.Text = dbSetting.SpName;
                txtNamespace.Text = ((ProjectModel)cmboProject.SelectedItem).Name;

                BindParameterToGridView(dbSetting);

                // disable
                gvColumnInfo.Rows.Clear();
                gvColumnInfo.Enabled = false;

                txtMappingPreview.Language = Language.HTML;
            }
        }

        private void comboMode_SelectedtIndexChanged(object sender, EventArgs e)
        {
            int index = cmboMode.SelectedIndex;
            string textPreview = string.Empty;

            EnableGenerateDTOControl(false);

            switch (index)
            {
                case 1:
                    textPreview = GenerateCriteriaClass(txtNamespace.Text, txtClassName.Text, gvParameter);
                    break;
                case 2:
                    textPreview = GenerateDynamicParameter(gvParameter);
                    break;
                case 3:
                    textPreview = GenerateHashParameter(gvParameter);
                    break;
                case 4:
                    textPreview = GenerateAddParameter();
                    break;
                case 5:
                    EnableGenerateDTOControl(true);
                    textPreview = GenerateReadIDataReader(txtClassName.Text, gvColumnInfo);
                    break;
                default:
                    EnableGenerateDTOControl(true);
                    textPreview = GenerateDTOClass(txtNamespace.Text, txtClassName.Text, gvColumnInfo);
                    break;
            }

            txtPreview.Text = textPreview;
        }

        private void cmboIndexTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingColumnInformation(dataSetColumnInfo.Tables[cmboIndexTable.SelectedIndex]);
        }

        #endregion

        #region Generate

        private string GenerateHeaderClass(string nameSpace, string className)
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

            return strSource.ToString();
        }

        private string GenerateCriteriaClass(string nameSpace, string className, DataGridView gvParameter)
        {
            try
            {
                StringBuilder strSource = new StringBuilder();
                strSource.Append(this.GenerateHeaderClass(nameSpace, className));

                if (nameSpace != string.Empty)
                {
                    strSource.Append("namespace " + nameSpace + "\n");
                }
                strSource.Append("{\n");
                strSource.Append("\tusing System;\n");
                strSource.Append("\tusing Fanex.NAP.Framework.Data;\n\n");
                strSource.Append("\tpublic class " + className + " : CriteriaBase\n");
                strSource.Append("\t{\n");

                foreach (DataGridViewRow item in gvParameter.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        string type = item.Cells[1].Value.ToString();
                        type = MappingDataType.GetTypeName(type);
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
                strSource.Append("\t\t\t throw new NotImplementedException();\n");
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

        private string GenerateDTOClass(string nameSpace, string className, DataGridView gvColumnInfo)
        {
            try
            {
                StringBuilder strSource = new StringBuilder();
                strSource.Append(this.GenerateHeaderClass(nameSpace, className));

                if (nameSpace != string.Empty)
                {
                    strSource.Append("namespace " + nameSpace + "\n");
                }
                strSource.Append("{\n");
                strSource.Append("\tusing System;\n\n");
                strSource.Append("\tpublic class " + className + "\n");
                strSource.Append("\t{\n");
                int count = 0;

                foreach (DataGridViewRow item in gvColumnInfo.Rows)
                {
                    count++;

                    if (item.Cells[0].Value != null)
                    {
                        string type = item.Cells[2].Value.ToString();
                        type = MappingDataType.GetTypeName(type);
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

        private string GenerateHashParameter(DataGridView gvParameter)
        {
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

            return str.ToString();
        }

        private string GenerateDynamicParameter(DataGridView gvParameter)
        {
            StringBuilder str = new StringBuilder();
            str.Append("var param = new\n");
            str.Append("{\n");
            foreach (DataGridViewRow item in gvParameter.Rows)
            {
                if (item.Cells[0].Value != null
                    && !string.IsNullOrWhiteSpace(item.Cells[0].Value.ToString()))
                {
                    string strKey = item.Cells[0].Value.ToString().TrimStart('@');
                    str.Append("\t" + strKey + " = _" + strKey.ToLower() + ",\n");
                }
            }
            str.Append("};");

            return str.ToString();
        }

        private string GenerateAddParameter()
        {
            var dbSetting = _settingDataSource[txtSettingKey.Text];

            StringBuilder str = new StringBuilder();
            str.Append("using (ObjectDb db = new ObjectDb(\"" + txtSettingKey.Text + "\"))\n");
            str.Append("{\n");

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

            return str.ToString();
        }

        private string GenerateMappingPreview(DataGridView gvColumnInfo)
        {
            try
            {
                string _format = "<Mapping DataField=\"{0}\" ObjectProperty=\"{1}\"/>";
                string _formatMulti = "<Mapping DataField=\"{0}\" ObjectProperty=\"{1}\" Object=\"{2}\"/>";

                StringBuilder strSource = new StringBuilder();
                foreach (DataGridViewRow item in gvColumnInfo.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        string properties = item.Cells[0].Value.ToString();
                        string mapping = item.Cells[1].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(mapping))
                        {
                            if (dataSetColumnInfo.Tables.Count > 1)
                            {
                                strSource.Append(string.Format(_formatMulti, properties, mapping, cmboIndexTable.SelectedIndex) + Environment.NewLine);
                            }
                            else
                            {
                                strSource.Append(string.Format(_format, properties, mapping) + Environment.NewLine);
                            }
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

        private string GenerateReadIDataReader(string className, DataGridView gvColumnInfo)
        {
            StringBuilder strSource = new StringBuilder();

            int count = 0;
            strSource.Append("using Fanex.Common;\n\n");
            strSource.Append("public List<" + className + "> ReadObject(IDataReader reader)\n");
            strSource.Append("{\n");
            strSource.Append("\tvar list =  new List<" + className + ">();\n\n");
            strSource.Append("\tusing (reader)\n");
            strSource.Append("\t{\n");
            strSource.Append("\t\twhile (reader.Read())\n");
            strSource.Append("\t\t{\n");
            strSource.Append("\t\t\tvar model = new " + className + "\n");
            strSource.Append("\t\t\t{\n");
            foreach (DataGridViewRow item in gvColumnInfo.Rows)
            {
                count++;

                if (item.Cells[0].Value != null)
                {
                    string type = item.Cells[2].Value.ToString();
                    type = MappingDataType.GetTypeName(type);
                    string properties = item.Cells[0].Value.ToString();
                    string mapping = item.Cells[1].Value.ToString();

                    properties = string.IsNullOrWhiteSpace(mapping) ? properties : mapping;

                    if (count == gvColumnInfo.Rows.Count)
                    {
                        strSource.Append(string.Format("\t\t\t\t{0} = reader.{1}(\"{0}\")\n", properties, MappingDataType.ReaderTo(type), properties));
                    }
                    else
                    {
                        strSource.Append(string.Format("\t\t\t\t{0} = reader.{1}(\"{0}\"),\n", properties, MappingDataType.ReaderTo(type), properties));
                    }
                }
            }
            strSource.Append("\t\t\t}\n\n");
            strSource.Append("\t\t\tlist.Add(model);\n");
            strSource.Append("\t\t}\n");
            strSource.Append("\t}\n\n");
            strSource.Append("\treturn list;\n");
            strSource.Append("}\n");

            return strSource.ToString();
        }

        #endregion

        #region Get Username

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

        #endregion

        #region Get resource path

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
        /// Returns true if the given file path is a folder.
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>True if a folder</returns>
        protected bool IsFolder(string path)
        {
            try
            {
                return (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Autocomplete

        protected AutoCompleteStringCollection BindingDataToAutoComple(string resourcePath, string connectionPath)
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
                                        if (!_settingDataSource.ContainsKey(key))
                                        {
                                            _settingDataSource.Add(key, databaseSetting);
                                        }
                                    }
                                }

                                autoList.Add(key);
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

        #endregion

        #region Bind Data

        /// <summary>
        /// Load data from list project to combobox
        /// </summary>
        /// <param name="listProject"></param>
        /// <param name="selectedProject"></param>
        private void BindProjectToCombo(List<ProjectModel> listProject, string selectedProject)
        {
            foreach (var item in listProject)
            {
                cmboProject.Items.Add(item);

                try
                {
                    if (Equals(item.Name, selectedProject))
                    {
                        cmboProject.SelectedItem = item;
                    }
                }
                catch (Exception ex) { }
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

        private void BindingColumnInformation(DataTable table, DbSetting dbSetting = null)
        {
            gvColumnInfo.Rows.Clear();

            if (dbSetting == null)
            {
                string settingKey = txtSettingKey.Tag.ToString();
                dbSetting = _settingDataSource[settingKey];
            }

            foreach (DataColumn item in table.Columns)
            {
                string mappingName = string.Empty;
                string columnName = item.ColumnName;

                foreach (DictionaryEntry itemMapColumn in dbSetting.CustomMappingColumns)
                {
                    if (Equals(itemMapColumn.Key.ToString(), columnName + ";" + cmboIndexTable.SelectedIndex.ToString())
                        || Equals(itemMapColumn.Key.ToString(), columnName + ";" + txtClassName.Text))
                    {
                        mappingName = itemMapColumn.Value as string;
                    }
                }

                gvColumnInfo.Rows.Add(columnName, mappingName, item.DataType.Name);
            }

            txtPreview.Text = GenerateDTOClass(txtNamespace.Text, txtClassName.Text, gvColumnInfo);
            txtMappingPreview.Text = GenerateMappingPreview(gvColumnInfo);
        }

        #endregion

        #region IO

        void SaveClassFile(string strSource, string nameClass, string filePath)
        {
            FileStream fileStream = new FileStream(filePath + "\\" + nameClass + ".cs", FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine(strSource.ToString());
            writer.Close();
            fileStream.Close();
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
        #endregion

        #region Ultil

        private void EnableGenerateDTOControl(bool enable)
        {
            btnExecute.Enabled = enable;
            gvColumnInfo.Enabled = enable;
            txtMappingPreview.Enabled = enable;
            previewMappingGroup.Enabled = enable;
            groupColumInfo.Enabled = enable;
            if (enable)
            {
                btnExecute.BackColor = Color.FromArgb(241, 243, 248);
            }
            else
            {
                btnExecute.BackColor = Color.Silver;
                gvColumnInfo.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
            }
        }

        #endregion
    }
}
