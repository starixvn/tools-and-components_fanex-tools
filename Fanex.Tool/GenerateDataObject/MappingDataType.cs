using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GenerateDataObject
{
    public class MappingDataType
    {
        public static string GetTypeAlias(string systemType)
        {
            string alias = "";
            switch (systemType)
            {
                case "SByte":
                    alias = "sbyte";
                    break;
                case "Byte":
                    alias = "byte";
                    break;
                case "Boolean":
                    alias = "bool";
                    break;
                case "Decimal":
                    alias = "decimal";
                    break;
                case "Double":
                    alias = "double";
                    break;
                case "Int16":
                    alias = "short";
                    break;
                case "UInt16":
                    alias = "ushort";
                    break;
                case "Int32":
                    alias = "int";
                    break;
                case "UInt32":
                    alias = "uint";
                    break;
                case "Int64":
                    alias = "long";
                    break;
                case "UInt64":
                    alias = "ulong";
                    break;
                case "Char":
                    alias = "char";
                    break;
                case "Single":
                    alias = "float";
                    break;
                case "Object":
                    alias = "object";
                    break;
                case "String":
                    alias = "string";
                    break;
                default:
                    alias = systemType;
                    break;
            }
            return alias;
        }
    }
}
