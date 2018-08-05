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
             * ���������ַ������ǿ��ܵĲ�������
             * �ַ����鳤����С2�������4��
             * 4������Ԫ�طֱ��ʾ��
             * 1��json����
             * 2��json��ֵ
             * 3���Ƿ�ȡ��json��ֵ˫���Ű�����Ĭ�ϼ�ֵ�Ǳ�����˫���Ű����ģ�
             * ֻ��ֵΪ1ʱ����ȡ��˫���Ű�������ϣ��ȡ��˫���Ű����������õ�3��Ԫ��ֵΪ1��
             * ����ֵΪ���֡������json����ʱ���û����Թ涨ȡ��˫���Ű�����
             * Ҫע����Ǽ�������ȡ��˫���Ű�������ʹ������ȡ������˫���Ű����ú���Ҳ����ȡ������Ϊ������˫�����Ǳ���ġ�
             * 4���Ƿ�ȡ�������ݽ���ת�����˫����ת�壬
             * ֻ��ֵΪ1ʱ����ȡ�������ݽ���ת�����˫����ת�壬��ϣ��ȡ�������ݽ���ת�����˫����ת��������õ�4��Ԫ��ֵΪ1��
             * Ĭ���Ǳ����json������ֵ�е�������ţ�˫���ź�ת���������ת��ģ�
             * ������Щ������Ż����json��ԭ�����岢ʹjson����ʧ��
             * ��Ҫ�������������ԭ���Ǽ�ֵ���������ͳ����ַ����������ܻ��������json����
             * ����ֵΪ�����json����ʱ�����������Ϊȡ��˫���Ű�������ô��Щ���鼰json�����е����������json��������ֵ�������˫���žͻᱻת��
             * �����Ļ�����һ�����������json����ʧ��
             * ���ԣ�Ҫ�ǳ�С�ģ����json��ֵΪ�����json����ʱ��һ��Ҫ���õ�4������ֵΪ1
             * 
             */
            StringBuilder sb = new StringBuilder();
            string tempKey = "";//����
            string tempValue = "";//��ֵ
            bool cancelWarpDQM = false;//�Ƿ�ȡ��˫���Ű���
            bool cancelEscapeDQM = false;//�Ƿ�ȡ�������ݽ���ת�����˫����ת��
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

                //�������
                if (!cancelEscapeDQM)
                {
                    tempKey = escapeDQMForKeyValue(tempKey);//����ת�����˫����ת��
                }
                tempKey = string.Format("\"{0}\"", tempKey);//����һ������ȡ��˫���Ű���


                //�����ֵ
                if (!cancelEscapeDQM)
                {
                    tempValue = escapeDQMForKeyValue(tempValue);//��ֵת�����˫����ת��
                    tempValue = escapeOtherForKeyValue(tempValue);
                }

                if (cancelWarpDQM)
                {
                    tempValue = string.Format("{0}", tempValue);
                }
                else
                {
                    tempValue = string.Format("\"{0}\"", tempValue);//��ֵ˫���Ű���
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
             *  ע�ⷵ�ص���һ�����飬������һ��json����������ÿ��Ԫ�ض���json��������Щ�����ʽͳһ
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
             *  ע�ⷵ�ص���һ�����飬������һ��json����������ÿ��Ԫ�ض���json��������Щ�����ʽͳһ
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
            //�Լ����ͼ�ֵ����ת�����˫����ת��
            //return keyValue.Replace("\"", "&quot;");
            string re = keyValue;
            re = re.Replace("\\", "\\\\");//ת���ת��
            re = re.Replace("\"", "\\\"");//˫����ת��
            return re;
        }
        private static string escapeOtherForKeyValue(string value)
        {
            //�Լ�ֵ���г�˫������������ַ�ת��
            string re = value;
            re = re.Replace("\r\n", "\\r\\n").Replace("\r", "\\r").Replace("\n", "\\n");//�س���ת��
            return re;
        }
    }
}

