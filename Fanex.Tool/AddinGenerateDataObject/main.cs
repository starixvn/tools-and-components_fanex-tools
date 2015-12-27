// --------------------------------------------------------------------------
// <copyright file="main.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.6.19">Created</change>
// </history>

namespace AddinGenerateDataObject
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using EnvDTE;
    using EnvDTE80;

    public partial class main : UserControl
    {
        #region Member

        /// <summary>
        /// syncRoot to check thread safe when implementing singleton pattern
        /// </summary>
        protected static object syncRoot = new object();

        /// <summary>
        /// cache manager
        /// </summary>
        private static volatile ObjectCache cacheManager;

        public static DTE2 VisualStudioObject;

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

        private IList<ProjectModel> _listProjectModel = new List<ProjectModel>();

        #endregion Member

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public main()
        {
            InitializeComponent();
        }

        #endregion Ctor

        /// <summary>
        /// Get Application Config and Resource path from Project
        /// </summary>
        /// <param name="project"></param>
        /// <param name="fileConfigPath"></param>
        /// <param name="spNamePath"></param>
        private void GetConfigurationPath(Project project, ref string fileConfigPath, ref string spNamePath)
        {
            try
            {
                fileConfigPath = string.Empty;
                spNamePath = string.Empty;

                var projectFile = project.FullName;

                if (!string.IsNullOrWhiteSpace(projectFile))
                {
                    string foler = Path.GetDirectoryName(projectFile);

                    string[] filesPath = Directory.GetFiles(foler, "*.config");

                    // Get config path
                    foreach (var item in filesPath)
                    {
                        if (item.ToLower().Contains("app.config")
                            || item.ToLower().Contains("web.config"))
                        {
                            fileConfigPath = item;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(fileConfigPath))
                    {
                        // Get resource path
                        XDocument document = XDocument.Load(fileConfigPath);

                        var configurationDocument = document.Descendants("configuration");
                        foreach (var itemConfig in configurationDocument)
                        {
                            var fanexProvider = itemConfig.Descendants("Fanex.Data.DbSettingProvider");
                            foreach (var itemFanexProvider in fanexProvider)
                            {
                                var provider = itemFanexProvider.Descendants("providers");
                                foreach (var itemProvider in provider)
                                {
                                    var itemAdd = itemProvider.Descendants("add");
                                    foreach (var item in itemAdd)
                                    {
                                        spNamePath = foler + "\\" + item.Attribute("resourcePath").Value.Replace("~", string.Empty);
                                        break;
                                    }
                                }
                            }

                            // Get connection string config path with configSource
                            var connectionConfig = itemConfig.Descendants("connectionStrings");
                            foreach (var itemConection in connectionConfig)
                            {
                                var configSource = itemConection.Attribute("configSource");
                                if (configSource != null)
                                {
                                    string configSourcePath = configSource.Value;
                                    fileConfigPath = foler + "\\" + configSourcePath;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Get all project configuration of current solution
        /// </summary>
        private IList<ProjectModel> GetProjectConfiguration()
        {
            if (VisualStudioObject != null)
            {
                var listProject = new List<ProjectModel>();
                string configPath = string.Empty;
                string spNamePath = string.Empty;

                var projects = CurrentIde.GetAllProject(VisualStudioObject);

                for (int i = 0; i < projects.Count; i++)
                {
                    Project project = projects[i];

                    if (!ReferenceEquals(project, null))
                    {
                        this.GetConfigurationPath(project, ref configPath, ref spNamePath);

                        if (!string.IsNullOrWhiteSpace(configPath)
                            && !string.IsNullOrWhiteSpace(spNamePath))
                        {
                            listProject.Add(new ProjectModel
                            {
                                Name = project.Name,
                                ConfigPath = configPath,
                                ResourcePath = spNamePath,
                                IsActive = VisualStudioObject.ActiveDocument != null 
                                ? VisualStudioObject.ActiveDocument.ActiveWindow.Project.Name == project.Name
                                : false
                            });
                        }
                    }
                }

                this.CacheManager.Set(Constant.ProjectKey, listProject, new CacheItemPolicy());

                return listProject;
            }

            throw new Exception("Visual Studio Object is null!");
        }

        private void main_Load(object sender, EventArgs e)
        {
            try
            {
                _listProjectModel = GetProjectConfiguration();

                BindDataToProjectCombo(_listProjectModel);

                ProjectModel project = _listProjectModel.Where(x => x.IsActive).FirstOrDefault();

                if (!ReferenceEquals(project, null))
                {
                    if (string.IsNullOrWhiteSpace(project.ConfigPath)
                     || string.IsNullOrWhiteSpace(project.ResourcePath))
                    {
                        MessageBox.Show("Can not find Application config or SP name file !");
                        cmboDatabase.Items.Clear();
                        gvSpName.Rows.Clear();
                    }
                    else
                    {
                        AddDataSourceToCache(project.ResourcePath, project.ConfigPath);

                        if (cmboDatabase.Items.Count > 0)
                        {
                            cmboDatabase.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
        protected void AddDataSourceToCache(string resourcePath, string connectionPath, string database = "", string strKey = "", bool isClear = true)
        {
            lbResourcePath.Text = "Resource Path : " + resourcePath.Replace("\\", "/");

            if (isClear)
            {
                cmboDatabase.Items.Clear();
            }

            gvSpName.Rows.Clear();

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
                                    string obj = item.Attribute("Object") != null ? item.Attribute("Object").Value : string.Empty;
                                    customMapping.Add(string.Format("{0};{1}", dataField, obj), objectProperty);
                                    //customMapping.Add(dataField, objectProperty);
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

                                if (key.ToLower().Contains(strKey.ToLower()))
                                {
                                    if (string.IsNullOrWhiteSpace(database))
                                    {
                                        gvSpName.Rows.Add(key, databaseSetting.SpName);
                                    }
                                    else
                                    {
                                        if (string.Compare(database, databaseSetting.DbName, true) == 0)
                                        {
                                            gvSpName.Rows.Add(key, databaseSetting.SpName);
                                        }
                                    }
                                }

                                this.CacheManager.Set(Constant.DbSettingCacheKeyName, settingDataSource, new CacheItemPolicy());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //if (cmboDatabase.Items.Count - 1 > index && index >= 0)
            //{
            //    cmboDatabase.SelectedIndex = index;
            //}
        }

        private void cmboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectModel project = (ProjectModel)cmboProject.SelectedItem;

            if (!ReferenceEquals(project, null))
            {
                if (string.IsNullOrWhiteSpace(project.ConfigPath)
                 || string.IsNullOrWhiteSpace(project.ResourcePath))
                {
                    MessageBox.Show("Can not find Application config or SP name file !");
                    cmboDatabase.Items.Clear();
                    gvSpName.Rows.Clear();
                }
                else
                {
                    AddDataSourceToCache(
                        resourcePath: project.ResourcePath
                        , connectionPath: project.ConfigPath
                        , database: cmboDatabase.SelectedItem.ToString()
                        , isClear: false);
                }
            }
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            ProjectModel project = (ProjectModel)cmboProject.SelectedItem;

            if (!ReferenceEquals(project, null))
            {
                if (string.IsNullOrWhiteSpace(project.ConfigPath)
                 || string.IsNullOrWhiteSpace(project.ResourcePath))
                {
                    MessageBox.Show("Can not find Application config or SP name file !");
                    cmboDatabase.Items.Clear();
                    gvSpName.Rows.Clear();
                }
                else
                {
                    AddDataSourceToCache(
                        resourcePath: project.ResourcePath
                        , connectionPath: project.ConfigPath
                        , database: cmboDatabase.Text
                        , strKey: txtKey.Text
                        , isClear: false);
                }
            }
        }

        private void gvSpName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string settingKey = gvSpName[0, e.RowIndex].Value.ToString();

            this.CacheManager.Set(Constant.NameKey, settingKey, new CacheItemPolicy());
            this.CacheManager.Set(Constant.ProjectNameKey, cmboProject.SelectedItem.ToString(), new CacheItemPolicy());
            Form from = new frmExecute();
            from.Show();
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

        private void gvSpName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                gvSpName_CellDoubleClick(sender, e);
            }
        }

        /// <summary>
        /// Load data from list project to combobox
        /// </summary>
        /// <param name="listProject"></param>
        private void BindDataToProjectCombo(IList<ProjectModel> listProject)
        {
            cmboProject.Items.Clear();
            foreach (var item in listProject)
            {
                cmboProject.Items.Add(item);
                if (item.IsActive)
                {
                    cmboProject.SelectedItem = item;
                }
            }

            if (cmboProject.Items.Count > 0 && cmboProject.SelectedIndex == -1)
            {
                cmboProject.SelectedIndex = 0;
            }
        }

        private void cmboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectModel project = (ProjectModel)cmboProject.SelectedItem;

            if (!ReferenceEquals(project, null))
            {
                if (string.IsNullOrWhiteSpace(project.ConfigPath)
                 || string.IsNullOrWhiteSpace(project.ResourcePath))
                {
                    MessageBox.Show("Can not find Application config or SP name file !");
                    cmboDatabase.Items.Clear();
                    gvSpName.Rows.Clear();
                }
                else
                {
                    AddDataSourceToCache(project.ResourcePath, project.ConfigPath);

                    if (cmboDatabase.Items.Count > 0)
                    {
                        cmboDatabase.SelectedIndex = 0;
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            main_Load(null, null);
            int selectConnectionIndex = cmboDatabase.SelectedIndex;
            this.cmboProject_SelectedIndexChanged(null, null);
            cmboDatabase.SelectedIndex = selectConnectionIndex;
        }
    }
}