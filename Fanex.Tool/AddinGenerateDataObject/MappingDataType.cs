using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace AddinGenerateDataObject
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingDataType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public static string GetTypeAlias(string systemType)
        {
            string alias = "";

            switch (systemType)
            {
                case "AnsiString":
                    alias = "string";
                    break;
                case "Binary":
                    alias = "byte[]";
                    break;
                case "Byte":
                    alias = "byte";
                    break;
                case "Boolean":
                    alias = "bool";
                    break;
                case "Currency":
                    alias = "decimal";
                    break;
                case "Date":
                    alias = "DateTime";
                    break;
                case "DateTime":
                    alias = "DateTime";
                    break;
                case "Decimal":
                    alias = "decimal";
                    break;
                case "Double":
                    alias = "double";
                    break;
                case "Guid":
                    alias = "Guid";
                    break;
                case "Int16":
                    alias = "short";
                    break;
                case "Int32":
                    alias = "int";
                    break;
                case "Int64":
                    alias = "long";
                    break;
                case "Object":
                    alias = "object";
                    break;
                case "SByte":
                    alias = "sbyte";
                    break;
                case "Single":
                    alias = "float";
                    break;
                case "String":
                    alias = "string";
                    break;
                case "Time":
                    alias = "DateTime";
                    break;
                case "UInt16":
                    alias = "ushort";
                    break;
                case "UInt32":
                    alias = "uint";
                    break;
                case "UInt64":
                    alias = "ulong";
                    break;
                case "VarNumeric":
                    alias = "decimal";
                    break;
                case "AnsiStringFixedLength":
                    alias = "string";
                    break;
                case "StringFixedLength":
                    alias = "char";
                    break;
                case "Xml":
                    alias = "Xml";
                    break;
                case "DateTime2":
                    alias = "DateTime?";
                    break;
                case "DateTimeOffset":
                    alias = "DateTimeOffset";
                    break;
                default:
                    alias = systemType;
                    break;
            }
            return alias;
        }
    }
}
