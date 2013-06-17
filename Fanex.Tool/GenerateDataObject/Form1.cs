// --------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.4.4">Created</change>
// </history>

namespace GenerateDataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;
    using System.IO;
    using System.Collections;
    using System.Runtime.Caching;
    using Fanex.Caching;
    using System.Configuration;

    public partial class Form1 : Form
    {
        /// <summary>
        /// syncRoot to check thread safe when implementing singleton pattern
        /// </summary>
        protected static object syncRoot = new object();

        protected const string DbSettingCacheKeyName = "GenerateDTOXmlDbSettingProvider:DBSetting";

        //private static volatile ObjectCache cacheManager;

        private ICache _cache = CacheService.CreateInstance(CacheType.MemoryCache);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadSpName_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSpNamePath.Text)
                 || string.IsNullOrWhiteSpace(txtConnectionStringPath.Text))
                {
                    MessageBox.Show("Browser your Sp Name and Config file !");
                }
                else
                {
                    AddDataSourceToCache(txtSpNamePath.Text.Trim());

                    grbLoadSP.Enabled = true;

                    cmboDatabase.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ///// <summary>
        ///// Gets the memory cache instance
        ///// </summary>
        //protected ObjectCache CacheManager
        //{
        //    /// <summary>
        //    /// Cache manager singleton
        //    /// </summary>
        //    get
        //    {
        //        if (cacheManager == null)
        //        {
        //            lock (syncRoot)
        //            {
        //                if (cacheManager == null)
        //                {
        //                    cacheManager = MemoryCache.Default;
        //                }
        //            }
        //        }

        //        return cacheManager;
        //    }
        //}

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


        /// <summary>
        /// Returns true if the given file path is a folder.
        /// </summary>
        /// <param name="Path">File path</param>
        /// <returns>True if a folder</returns>
        protected bool IsFolder(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
        }

        /// <summary>
        /// Get all db setting configuration from the data source and than add to cache
        /// </summary>
        /// <param name="resourcePath">path to resource file for folder</param>        
        protected void AddDataSourceToCache(string resourcePath)
        {
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
                        var document = XDocument.Load(listOfResouces[i]);
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
                                databaseSetting.ConnectionString = ConnectionStringGetter.GetConnectionString(txtConnectionStringPath.Text.Trim(), databaseSetting.DbName, isEncryptedConn);

                                if (!cmboDatabase.Items.Contains(databaseSetting.DbName))
                                {
                                    cmboDatabase.Items.Add(databaseSetting.DbName);
                                }

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
                            }
                        }
                    }

                    // update to cache                   
                    _cache.Set<Dictionary<string, DbSetting>>(DbSettingCacheKeyName, settingDataSource, DateTime.MaxValue);
                }
                catch
                {
                    throw;
                }
            }
        }

        ///// <summary>
        ///// Gets the cache policy.
        ///// </summary>
        ///// <returns>return CacheItemPolicy</returns>
        //protected CacheItemPolicy GetCachePolicy()
        //{
        //    return null;
        //}

        private void cmboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSpName.Rows.Clear();

            var settingDataSource = _cache.Get<Dictionary<string, DbSetting>>(DbSettingCacheKeyName);

            foreach (var item in settingDataSource)
            {
                var itemDbSetting = item.Value as DbSetting;

                if (itemDbSetting.DbName == cmboDatabase.Text)
                {
                    gvSpName.Rows.Add(item.Key, itemDbSetting.SpName);
                }
            }

        }

        private void gvSpName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string settingKey = gvSpName[0, e.RowIndex].Value.ToString();

            //var settingDataSource = this.CacheManager.Get(DbSettingCacheKeyName) as Dictionary<string, DbSetting>;
            _cache.Set<string>("DataMainForm", settingKey, DateTime.MaxValue);

            var form = new frmExecute();
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grbLoadSP.Enabled = false;
            try
            {
                string spName = ConfigurationManager.AppSettings["SpName"];
                string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                txtSpNamePath.Text = spName;
                txtConnectionStringPath.Text = connectionString;
            }
            catch (Exception)
            {
                MessageBox.Show("Miss Configure AppSetting !");
            }
        }

        private void btnSpBrowser_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                txtSpNamePath.Text = openFileDialog1.FileName;
            }
        }

        private void btnCofigBrowser_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                txtConnectionStringPath.Text = openFileDialog1.FileName;
            }
        }

        private void txtSpNamePath_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSpNamePath.Text)
                || string.IsNullOrWhiteSpace(txtConnectionStringPath.Text))
            {
                btnLoadSpName.Enabled = false;
            }
            else
            {
                btnLoadSpName.Enabled = true;
            }
        }

        private void txtConnectionStringPath_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSpNamePath.Text)
                || string.IsNullOrWhiteSpace(txtConnectionStringPath.Text))
            {
                btnLoadSpName.Enabled = false;
            }
            else
            {
                btnLoadSpName.Enabled = true;
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            // update to cache                   
            var list = _cache.Get<Dictionary<string, DbSetting>>(DbSettingCacheKeyName);

            gvSpName.Rows.Clear();

            foreach (var item in list)
            {
                if (item.Key.ToLower().Contains(txtKey.Text.Trim().ToLower()))
                {
                    gvSpName.Rows.Add(item.Key, item.Value.SpName);
                }
            }
        }
    }
}
