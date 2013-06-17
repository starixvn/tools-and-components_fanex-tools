// -----------------------------------------------------------------------
// <copyright file="DbSetting.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GenerateDataObject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class DbSetting
    {
        /// <summary>
        /// Gets or sets the name of the sp.
        /// </summary>
        /// <value>
        /// The name of the sp.
        /// </value>
        public string SpName { get; set; }

        /// <summary>
        /// Gets or sets the name of the db.
        /// </summary>
        /// <value>
        /// The name of the db.
        /// </value>
        public string DbName { get; set; }

        /// <summary>
        /// Gets or sets the custom mapping columns.
        /// </summary>
        /// <value>
        /// The custom mapping columns.
        /// </value>
        public Hashtable CustomMappingColumns { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Get or set execute time out  (seconds)
        /// </summary>
        public int ExecuteTimeOut { get; set; }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.SpName)
                && !string.IsNullOrWhiteSpace(this.DbName)
                && !string.IsNullOrWhiteSpace(this.ConnectionString);
        }
    }
}
