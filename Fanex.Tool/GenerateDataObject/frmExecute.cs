using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fanex.Caching;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.IO;

namespace GenerateDataObject
{
    public partial class frmExecute : Form
    {
        protected const string DbSettingCacheKeyName = "GenerateDTOXmlDbSettingProvider:DBSetting";

        private ICache _cache = CacheService.CreateInstance(CacheType.MemoryCache);

        public frmExecute()
        {
            InitializeComponent();
        }

        private void frmExecute_Load(object sender, EventArgs e)
        {
            string settingKey = _cache.Get<string>("DataMainForm");

            var settingDataSource = _cache.Get<Dictionary<string, DbSetting>>(DbSettingCacheKeyName);

            var dbSetting = settingDataSource[settingKey];

            lbSpName.Text = dbSetting.SpName;
            lbSpName.Tag = settingKey;

            //default
            txtClassName.Text = dbSetting.SpName;
            txtNamespace.Text = dbSetting.SpName;

            BindParameterToGridView(dbSetting);

            // disable
            gvColumnInfo.Enabled = false;
            btnGenerate.Enabled = false;
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

                var settingDataSource = _cache.Get<Dictionary<string, DbSetting>>(DbSettingCacheKeyName);

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
                    }

                    MessageBox.Show("OK");
                    this.Close();
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

        private string GenerateClass(string nameSpace, string className)
        {
            try
            {
                StringBuilder strSource = new StringBuilder();
                strSource.Append("using System;" + Environment.NewLine);
                if (nameSpace != string.Empty)
                {
                    strSource.Append("namespace " + nameSpace + " { " + Environment.NewLine);
                }
                strSource.Append("\tpublic class " + className + "{" + Environment.NewLine);
                foreach (DataGridViewRow item in gvColumnInfo.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        string type = item.Cells[2].Value.ToString();
                        type = MappingDataType.GetTypeAlias(type);
                        string properties = item.Cells[0].Value.ToString();
                        string mapping = item.Cells[1].Value.ToString();

                        properties = string.IsNullOrWhiteSpace(mapping) ? properties : mapping;

                        strSource.Append("\t\t public " + type + " " + properties + "{ get; set;}" + Environment.NewLine + Environment.NewLine);
                    }
                }
                strSource.Append("\t}" + Environment.NewLine);
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
    }
}
