namespace Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Reflection;

    public sealed class Json
    {
        public static string Parse(IList<string[]> list)
        {
            /*
             * new string[] { "urlPath", urlPath }
             * new string[] { "totalRecord", totalRecord.ToString(), "1", "1" }
             * new string[] { "roleList", roleList, "1", "1"}
             * 上面三个字符数组是可能的参数案例
             * 字符数组长度最小2个，最大4个
             * 4个数组元素分别表示：
             * 1：json键名
             * 2：json键值
             * 3：是否取消json键值双引号包裹，默认键值是必须有双引号包裹的，
             * 只有值为1时才能取消双引号包裹，即希望取消双引号包裹必须设置第3个元素值为1，
             * 当键值为数字、数组或json对象时，用户可以规定取消双引号包裹，
             * 要注意的是键名不能取消双引号包裹，即使设置了取消键名双引号包裹该函数也不会取消，因为键名中双引号是必须的。
             * 4：是否取消对内容进行转义符及双引号转义，
             * 只有值为1时才能取消对内容进行转义符及双引号转义，即希望取消对内容进行转义符及双引号转义必须设置第4个元素值为1，
             * 默认是必须对json键名键值中的特殊符号（双引号和转义符）进行转义的，
             * 否则这些特殊符号会混淆json的原符意义并使json解析失败
             * 需要设置这个参数的原因是键值的数据类型除了字符串名还可能会是数组或json对象
             * 当键值为数组或json对象时，如果不设置为取消双引号包裹，那么这些数组及json对象中的有意义的子json键名及键值的外包裹双引号就会被转义
             * 那样的话，就一定会造成整个json解析失败
             * 所以，要非常小心，如果json键值为数组或json对象时，一定要设置第4个参数值为1
             * 
             */
            StringBuilder sb = new StringBuilder();
            string tempKey = "";//键名
            string tempValue = "";//键值
            bool cancelWarpDQM = false;//是否取消双引号包裹
            bool cancelEscapeDQM = false;//是否取消对内容进行转义符及双引号转义
            foreach (string[] info in list) 
            {
                tempKey = info[0];
                tempValue = info[1];
                if (info.Length > 3 && info[3] == "1")
                {
                    cancelEscapeDQM = true;
                }
                if (info.Length > 2 && info[2] == "1")
                {
                    cancelWarpDQM = true;
                }

                //处理键名
                if (!cancelEscapeDQM)
                {
                    tempKey = escapeDQMForKeyValue(tempKey);//键名转义符及双引号转义
                }
                tempKey = string.Format("\"{0}\"", tempKey);//键名一定不能取消双引号包裹


                //处理键值
                if (!cancelEscapeDQM)
                {
                    tempValue = escapeDQMForKeyValue(tempValue);//键值转义符及双引号转义
                    tempValue = escapeOtherForKeyValue(tempValue);
                }

                if (cancelWarpDQM)
                {
                    tempValue = string.Format("{0}", tempValue);
                }
                else
                {
                    tempValue = string.Format("\"{0}\"", tempValue);//键值双引号包裹
                }


                sb.AppendFormat(",{0}:{1}", tempKey, tempValue);

                cancelWarpDQM = false;
                cancelEscapeDQM = false;
            }
            if (sb.Length > 0) 
            {
                sb.Remove(0, 1);
            }
            return "{" + sb.ToString() + "}";
        }

        public static string Parse(object o, params List<string>[] propNames)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propertys = o.GetType().GetProperties();
            string tempPropName = "";
            bool needFilter = propNames.Length > 0 ? true : false;
            List<string> listPropNames = new List<string>();
            if (needFilter) {
                listPropNames = propNames[0];
            }
            foreach (PropertyInfo p in propertys) 
            {
                tempPropName = p.Name.ToString();
                if (!needFilter || listPropNames.Exists(r => r.Equals(tempPropName)))
                {
                    sb.Append(",");
                    sb.Append("\"");
                    sb.Append(escapeDQMForKeyValue(tempPropName));
                    sb.Append("\"");
                    sb.Append(":");
                    sb.Append("\"");
                    sb.Append(escapeOtherForKeyValue(escapeDQMForKeyValue(p.GetValue(o, null).ToString())));
                    sb.Append("\"");
                }
                tempPropName = "";
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return "{" + sb.ToString() + "}";
        }

        public static string ListParse(IList<object> list, params List<string>[] propNames)
        {
            /*
             *  注意返回的是一个数组，而不是一个json对象，数组中每个元素都是json对象，且这些对象格式统一
             */
            StringBuilder sb = new StringBuilder();
            foreach (object info in list)
            {
                sb.Append(",");
                sb.Append(Parse(info, propNames));
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return "[" + sb.ToString() + "]";
        }

        public static string ListParse(DataTable list, params List<string>[] propNames)
        {
            /*
             *  注意返回的是一个数组，而不是一个json对象，数组中每个元素都是json对象，且这些对象格式统一
             */

            StringBuilder sb = new StringBuilder();
            foreach (DataRow info in list.Rows)
            {
                sb.Append(",");
                sb.Append(Parse(info, propNames));
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return "[" + sb.ToString() + "]";
        }

        public static string Parse(DataRow o, params List<string>[] propNames)
        {
            StringBuilder sb = new StringBuilder();

            DataColumnCollection propertys = o.Table.Columns;
            //PropertyInfo[] propertys = o.GetType().GetProperties();
            string tempPropName = "";
            bool needFilter = propNames.Length > 0 ? true : false;
            List<string> listPropNames = new List<string>();
            if (needFilter)
            {
                listPropNames = propNames[0];
            }
            foreach (DataColumn p in propertys)
            {
                tempPropName = p.ColumnName.ToString();
                if (!needFilter || listPropNames.Exists(r => r.Equals(tempPropName)))
                {
                    sb.Append(",");
                    sb.Append("\"");
                    sb.Append(escapeDQMForKeyValue(tempPropName));
                    sb.Append("\"");
                    sb.Append(":");
                    sb.Append("\"");
                    sb.Append(escapeOtherForKeyValue(escapeDQMForKeyValue(o[tempPropName].ToString())));
                    sb.Append("\"");
                }
                tempPropName = "";
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return "{" + sb.ToString() + "}";
        }

        private static string escapeDQMForKeyValue(string keyValue) 
        {
            //对键名和键值进行转义符及双引号转义
            //return keyValue.Replace("\"", "&quot;");
            string re = keyValue;
            re = re.Replace("\\", "\\\\");//转义符转义
            re = re.Replace("\"", "\\\"");//双引号转义
            return re;
        }
        private static string escapeOtherForKeyValue(string value)
        {
            //对键值进行除双引号外的特殊字符转义
            string re = value;
            re = re.Replace("\r\n", "\\r\\n").Replace("\r", "\\r").Replace("\n", "\\n");//回车符转义
            return re;
        }
    }
}

