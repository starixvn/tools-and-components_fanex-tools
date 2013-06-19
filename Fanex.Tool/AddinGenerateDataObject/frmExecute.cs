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

    /// <summary>
    /// 
    /// </summary>
    public partial class frmExecute : Form
    {
        /// <summary>
        /// 
        /// </summary>
        protected const string DbSettingCacheKeyName = "GenerateDTOXmlDbSettingProvider:DBSetting";

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

        private void frmExecute_Load(object sender, EventArgs e)
        {
            string settingKey = (string)this.CacheManager.Get(Constant.NameKey);
            string projectName = (string)this.CacheManager.Get(Constant.ProjectNameKey);

            var settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(DbSettingCacheKeyName);

            var dbSetting = settingDataSource[settingKey];

            lbSpName.Text = dbSetting.SpName;
            lbSpName.Tag = settingKey;

            //default
            txtClassName.Text = dbSetting.SpName;
            txtNamespace.Text = projectName;

            BindParameterToGridView(dbSetting);

            // disable
            gvColumnInfo.Enabled = false;
            btnGenerate.Enabled = false;

            txtMappingPreview.Language = Language.HTML;
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

                string settingKey = lbSpName.Tag.ToString();

                var settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(DbSettingCacheKeyName);

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
                btnGenerate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
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
                    strSource.Append("namespace " + nameSpace + " {\n\n");
                }
                strSource.Append("\tpublic class " + className + "{\n\n");

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

                        if (count == gvColumnInfo.Rows.Count - 1)
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
    }
}
