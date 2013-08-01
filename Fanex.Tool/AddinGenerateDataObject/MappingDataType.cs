// --------------------------------------------------------------------------
// <copyright file="MappingDataType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.7.31">Created</change>
// </history>

namespace AddinGenerateDataObject
{
    /// <summary>
    /// Mapping
    /// </summary>
    public class MappingDataType
    {
        /// <summary>
        /// Mapping Sql Data Type to C# Data Type
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        public static string GetTypeName(string sqlType)
        {
            string alias = "";

            switch (sqlType)
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
                    alias = sqlType;
                    break;
            }
            return alias;
        }

        /// <summary>
        /// Mapping conver to
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ReaderTo(string type)
        {
            string strConvert = "";

            switch (type)
            {
                case "string":
                    strConvert = "ToString";
                    break;
                case "byte[]":
                    strConvert = "ToByteArray";
                    break;
                case "byte":
                    strConvert = "ToByte";
                    break;
                case "bool":
                    strConvert = "ToBool";
                    break;
                case "decimal":
                    strConvert = "ToDecimal";
                    break;
                case "DateTime":
                    strConvert = "ToDateTime";
                    break;
                case "double":
                    strConvert = "ToDouble";
                    break;
                case "Guid":
                    strConvert = "To<Guid>";
                    break;
                case "short":
                    strConvert = "ToShort";
                    break;
                case "int":
                    strConvert = "ToInt";
                    break;
                case "long":
                    strConvert = "ToLong";
                    break;
                case "object":
                    strConvert = "To<object>";
                    break;
                case "sbyte":
                    strConvert = "To<sbyte>";
                    break;
                case "float":
                    strConvert = "ToFloat";
                    break;
                case "ushort":
                    strConvert = "To<ushort>";
                    break;
                case "uint":
                    strConvert = "To<uint>";
                    break;
                case "ulong":
                    strConvert = "To<ulong>";
                    break;
                case "char":
                    strConvert = "ToChar";
                    break;
                case "Xml":
                    strConvert = "To<Xml>";
                    break;
                case "DateTime2":
                    strConvert = "ToDateTime2";
                    break;
                case "DateTimeOffset":
                    strConvert = "To<DateTimeOffset>";
                    break;
                default:
                    strConvert = type;
                    break;
            }

            return strConvert;
        }
    }
}
