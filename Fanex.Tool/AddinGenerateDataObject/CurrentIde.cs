// -----------------------------------------------------------------------
// <copyright file="CurrentIde.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AddinGenerateDataObject
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using EnvDTE;
    using Process = System.Diagnostics.Process;
    using System.Collections.Generic;
    using EnvDTE80;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CurrentIde
    {
        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);
        [DllImport("ole32.dll")]
        private static extern void GetRunningObjectTable(int reserved,
                                                         out IRunningObjectTable prot);

        internal static DTE2 GetCurrent()
        {
            //rot entry for visual studio running under current process.
            string rotEntry = String.Format("!VisualStudio.DTE.10.0:{0}",
                                             Process.GetCurrentProcess().Id);
            IRunningObjectTable rot;
            GetRunningObjectTable(0, out rot);
            IEnumMoniker enumMoniker;
            rot.EnumRunning(out enumMoniker);
            enumMoniker.Reset();
            IntPtr fetched = IntPtr.Zero;
            IMoniker[] moniker = new IMoniker[1];
            while (enumMoniker.Next(1, moniker, fetched) == 0)
            {
                IBindCtx bindCtx;
                CreateBindCtx(0, out bindCtx);
                string displayName;
                moniker[0].GetDisplayName(bindCtx, null, out displayName);
                if (displayName == rotEntry)
                {
                    object comObject;
                    rot.GetObject(moniker[0], out comObject);
                    return (DTE2)comObject;
                }
            }
            return null;
        }

        internal static List<Project> GetAllProject(DTE2 _applicationObject)
        {
            List<Project> list = new List<Project>();

            Projects projects = _applicationObject.Solution.Projects;
            var item = projects.GetEnumerator();

            while (item.MoveNext())
            {
                var project = item.Current as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    list.AddRange(GetSolutionFolderProjects(project));
                }
                else
                {
                    list.Add(project);
                }
            }

            return list;
        }

        internal static IEnumerable<Project> GetSolutionFolderProjects(Project solutionFolder)
        {
            List<Project> list = new List<Project>();
            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
                if (subProject == null)
                {
                    continue;
                }

                // If this is another solution folder, do a recursive call, otherwise add
                if (subProject.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    list.AddRange(GetSolutionFolderProjects(subProject));
                }
                else
                {
                    list.Add(subProject);
                }
            }

            return list;
        }
    }
}
