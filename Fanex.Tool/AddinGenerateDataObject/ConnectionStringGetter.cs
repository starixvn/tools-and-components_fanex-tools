// --------------------------------------------------------------------------
// <copyright file="ConnectionStringGetter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.6.19">Created</change>
// </history>

namespace AddinGenerateDataObject
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConnectionStringGetter
    {
        /// <summary>
        /// Cache key of connection string
        /// </summary>
        private const string CacheKeyFormat = "{0}:ConnectionString";

        /// <summary>
        /// contain connection string key by key
        /// </summary>
        private static volatile StringDictionary connDictionary = new StringDictionary();

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the db.</param>
        /// <param name="isEncryptedConnection">if set to <c>true</c> [is encrypted connection].</param>
        /// <returns>plain connection string</returns>
        public static string GetConnectionString(string path, string connectionStringName, bool isEncryptedConnection = true)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentNullException();
            }

            var connectionString = string.Empty;
            string cacheKey = string.Format(CacheKeyFormat, connectionStringName);

            if (connDictionary.ContainsKey(cacheKey))
            {
                connectionString = connDictionary[cacheKey];
            }
            else
            {
                var document = XDocument.Load(path);
                var functions = document.Descendants("configuration");
                if (functions.Any())
                {
                    foreach (var functionItem in functions)
                    {
                        var settingList = functionItem.Descendants("connectionStrings");

                        foreach (var setting in settingList)
                        {
                            var itemAdd = setting.Descendants("add");

                            foreach (var itemDatabase in itemAdd)
                            {
                                connectionString = itemDatabase.Attribute("name").Value;

                                if (string.Compare(connectionString, connectionStringName) == 0)
                                {
                                    connectionString = itemDatabase.Attribute("connectionString").Value;

                                    if (string.IsNullOrWhiteSpace(connectionString))
                                    {
                                        throw new NullReferenceException("ConnectionStringIsNullOrEmpty");
                                    }

                                    // decrypt connection string
                                    if (isEncryptedConnection)
                                    {
                                        connectionString = Cryptography.DeCryptConnectionString(connectionString);
                                    }

                                    if (!connDictionary.ContainsKey(cacheKey))
                                    {
                                        connDictionary.Add(cacheKey, connectionString);
                                    }

                                    return connectionString;
                                }
                            }

                        }
                    }
                }
                else
                {
                    var settingList = document.Descendants("connectionStrings");
                    foreach (var setting in settingList)
                    {
                        var itemAdd = setting.Descendants("add");

                        foreach (var itemDatabase in itemAdd)
                        {
                            connectionString = itemDatabase.Attribute("name").Value;

                            if (string.Compare(connectionString, connectionStringName) == 0)
                            {
                                connectionString = itemDatabase.Attribute("connectionString").Value;

                                if (string.IsNullOrWhiteSpace(connectionString))
                                {
                                    throw new NullReferenceException("ConnectionStringIsNullOrEmpty");
                                }

                                // decrypt connection string
                                if (isEncryptedConnection)
                                {
                                    connectionString = Cryptography.DeCryptConnectionString(connectionString);
                                }

                                if (!connDictionary.ContainsKey(cacheKey))
                                {
                                    connDictionary.Add(cacheKey, connectionString);
                                }

                                return connectionString;
                            }
                        }

                    }
                }

            }

            return connectionString;
        }
    }
}
