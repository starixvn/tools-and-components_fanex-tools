namespace Fanex.Data.Generator
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using AddinGenerateDataObject;
    using System.Windows.Forms;

    [Guid("b5e7f061-b1e4-4d9a-b503-e449c4bd38b5")]
    public class DataObjectsGenerator : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectsGenerator"/> class.
        /// </summary>
        public DataObjectsGenerator() : base(null)
        {
            this.Caption = "DataObjectsGenerator";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            _mainWindow = new main();
        }

        private main _mainWindow;

        public override IWin32Window Window
        {
            get
            {
                return _mainWindow;
            }
        }
    }
}
