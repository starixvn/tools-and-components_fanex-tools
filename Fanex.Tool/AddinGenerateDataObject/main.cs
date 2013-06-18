using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Collections;
using System.Runtime.Caching;

namespace AddinGenerateDataObject
{
    public partial class main : UserControl
    {
        /// <summary>
        /// syncRoot to check thread safe when implementing singleton pattern
        /// </summary>
        protected static object syncRoot = new object();

        protected const string DbSettingCacheKeyName = "GenerateDTOXmlDbSettingProvider:DBSetting";

        /// <summary>
        /// cache manager
        /// </summary>
        private static volatile ObjectCache cacheManager;

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

        public main()
        {
            InitializeComponent();
        }

        string spNamePath = string.Empty;
        string configPath = string.Empty;

        private void main_Load(object sender, EventArgs e)
        {
            spNamePath = (string)CacheManager.Get(Constant.SpNamePathKey);
            configPath = (string)CacheManager.Get(Constant.ConfigPathKey);

            try
            {
                if (string.IsNullOrWhiteSpace(configPath)
                 || string.IsNullOrWhiteSpace(spNamePath))
                {
                    MessageBox.Show("Browser your Sp Name and Config file !");
                }
                else
                {
                    AddDataSourceToCache(spNamePath, configPath);

                    cmboDatabase.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        protected void AddDataSourceToCache(string resourcePath, string connectionPath)
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
                                databaseSetting.ConnectionString = ConnectionStringGetter.GetConnectionString(connectionPath, databaseSetting.DbName, isEncryptedConn);

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
                    this.CacheManager.Set(DbSettingCacheKeyName, settingDataSource, new CacheItemPolicy());
                    //_cache.Set<Dictionary<string, DbSetting>>(DbSettingCacheKeyName, settingDataSource, DateTime.MaxValue);
                }
                catch
                {
                    throw;
                }
            }
        }

        private void cmboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSpName.Rows.Clear();

            var settingDataSource = (Dictionary<string, DbSetting>)this.CacheManager.Get(DbSettingCacheKeyName);

            foreach (var item in settingDataSource)
            {
                var itemDbSetting = item.Value as DbSetting;

                if (itemDbSetting.DbName == cmboDatabase.Text)
                {
                    gvSpName.Rows.Add(item.Key, itemDbSetting.SpName);
                }
            }
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            // update to cache                   
            var list = (Dictionary<string, DbSetting>)this.CacheManager.Get(DbSettingCacheKeyName);

            gvSpName.Rows.Clear();

            foreach (var item in list)
            {
                if (item.Key.ToLower().Contains(txtKey.Text.Trim().ToLower()))
                {
                    gvSpName.Rows.Add(item.Key, item.Value.SpName);
                }
            }
        }

        private void gvSpName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string settingKey = gvSpName[0, e.RowIndex].Value.ToString();

            //var settingDataSource = this.CacheManager.Get(DbSettingCacheKeyName) as Dictionary<string, DbSetting>;
            this.CacheManager.Set(Constant.NameKey, settingKey, new CacheItemPolicy());
            Form from = new frmExecute();
            from.Show();
        }
    }
}
