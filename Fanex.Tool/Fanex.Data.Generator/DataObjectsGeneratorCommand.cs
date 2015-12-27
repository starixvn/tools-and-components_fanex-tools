namespace Fanex.Data.Generator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Runtime.Caching;
    using System.Xml.Linq;
    using AddinGenerateDataObject;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class DataObjectsGeneratorCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("f1de5f68-ebfc-4985-b7a5-740f2ee1b284");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectsGeneratorCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private DataObjectsGeneratorCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.ShowToolWindow, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static DataObjectsGeneratorCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package, DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
            Instance = new DataObjectsGeneratorCommand(package);
        }

        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            this.GetProjectConfiguration();

            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.package.FindToolWindow(typeof(DataObjectsGenerator), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        private static DTE2 _applicationObject;

        private static volatile ObjectCache cacheManager;

        private static object syncRoot = new object();

        private ObjectCache CacheManager
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
        private void GetProjectConfiguration()
        {
            if (_applicationObject != null)
            {
                var listProject = new List<ProjectModel>();
                string configPath = string.Empty;
                string spNamePath = string.Empty;

                var projects = CurrentIde.GetAllProject(_applicationObject);

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
                                IsActive = _applicationObject.ActiveDocument.ActiveWindow.Project.Name == project.Name
                            });
                        }
                    }
                }

                this.CacheManager.Set(Constant.ProjectKey, listProject, new CacheItemPolicy());
            }
        }
    }
}