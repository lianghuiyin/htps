using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Utility
{

    /// <summary>
    /// JSONHelper类是专门提供给用于高性能、可升级的JSON操作
    /// </summary>
    public static class JSONHelper
    {
        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return retVal;
        }
        public static string Serialize<T>(IList<T> listObj)
        {
            StringBuilder sb = new StringBuilder();
            if (listObj != null)
            {
                for (int i = 0, len = listObj.Count; i < len; i++)
                {
                    sb.Append(",");
                    sb.Append(JSONHelper.Serialize<T>(listObj[i]));
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return string.Format("[{0}]", sb.ToString());
        }
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
        public static T[] Deserializes<T>(string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T[]));
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            T[] gType = (T[])ser.ReadObject(ms);
            return gType;
        }
    }
}
