namespace Utility
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Data;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Globalization;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Xml;

    public sealed class Function
    {
        ///<summary> 
        ///�������Ӵ�����connectionName�������������ַ���
        ///</summary>
        ///<param name="connectionName"></param> 
        ///<returns></returns>
        public static string GetConnectionStringsConfig(string connectionName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();
            return connectionString;
        }
        ///<summary>
        ///���أ�.exe.config�ļ���appSettings���ýڵ�value�� 
        ///</summary> 
        ///<param name="strKey"></param>
        ///<returns></returns>
        public static string GetAppConfig(string strKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == strKey)
                {
                    return ConfigurationManager.AppSettings[strKey];
                }
            }
            return null;
        }
        ///<summary> 
        ///�޸������ַ���
        ///</summary>
        ///<param name="conName"></param> 
        ///<param name="newConString"></param> 
        ///<returns></returns>
        public static bool ModifyConnectionString(string path, string conName, string newConString)
        {
            bool re = false;
            XmlDocument doc = new XmlDocument();
            //string path = System.Windows.Forms.Application.ExecutablePath + ".config";
            doc.Load(path);
            XmlNode node = doc.SelectSingleNode("//connectionStrings");//��ȡconnectionStrings�ڵ�
            try
            {
                XmlElement element = (XmlElement)node.SelectSingleNode("//add[@name='" + conName + "']");
                if (element != null)
                {
                    //���ڸ����ӽڵ�
                    element.SetAttribute("connectionString", newConString);
                    doc.Save(path);
                    re = true;
                }
                else
                {
                    //conName������
                }
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
            return re;
        }
        ///<summary> 
        ///�޸�app�ڵ�
        ///</summary>
        ///<param name="key"></param> 
        ///<param name="newValue"></param> 
        ///<returns></returns>
        public static bool ModifyAppSettings(string path, string key, string newValue)
        {
            bool re = false;
            XmlDocument doc = new XmlDocument();
            //string path = System.Windows.Forms.Application.ExecutablePath + ".config";
            doc.Load(path);
            XmlNode node = doc.SelectSingleNode("//appSettings");//��ȡconnectionStrings�ڵ�
            try
            {
                XmlElement element = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
                if (element != null)
                {
                    //���ڸ����ӽڵ�
                    element.SetAttribute("value", newValue);
                    doc.Save(path);
                    re = true;
                }
                else
                {
                    //key������
                }
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
            return re;
        }
        /// <summary>
        /// ��datatable��ʽ����ת����json����
        /// </summary>
        /// <param name="dt">��Ҫת����DataTable</param>
        /// <param name="outOffFields">Ҫȥ�����ֶ�</param>
        /// <returns>Json���</returns>
        public static string ConvertToJsonArray(DataTable dt,params string[] outOffFields)
        {
            StringBuilder sbRe = new StringBuilder();
            sbRe.Append("[");
            if (dt != null) {
                for (int i = 0;i < dt.Rows.Count;i++)
                {
                    sbRe.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++) 
                    {
                        string strCurColuName = dt.Columns[j].ColumnName.ToString();
                        bool isFieldOut = false;
                        foreach (string f in outOffFields) {
                            if (strCurColuName == f)
                            {
                                isFieldOut = true;
                                break;
                            }
                        }
                        if (!isFieldOut)
                        {
                            sbRe.AppendFormat("{0}:\"{1}\",", strCurColuName, dt.Rows[i][j].ToString());
                        }
                    }
                    //sbRe = new StringBuilder(sbRe.ToString().TrimEnd(','));
                    sbRe.Remove(sbRe.ToString().LastIndexOf(","), 1);
                    sbRe.Append("},");
                }
                sbRe.Remove(sbRe.ToString().LastIndexOf(","), 1);
            }
            sbRe.Append("]");
            return sbRe.ToString();
        }
        /// <summary>
        /// ���������ݱ��ֶκϲ�������һ���µĺϲ���ı�
        /// </summary>
        /// <param name="dt1">��һ��DataTable</param>
        /// <param name="dt2">�ڶ���DataTable</param>
        /// <returns>�ϲ��ֶκ���±�</returns>
        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            DataTable dt = new DataTable();
            //����dt������
            int dtRowCount = 0;

            //dt������Ϊdt1��dt2��������������
            if (dt1.Rows.Count > dt2.Rows.Count)
            {
                dtRowCount = dt1.Rows.Count;
            }
            else
            {
                dtRowCount = dt2.Rows.Count;
            }

            //��dt�����dt1������
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                dt.Columns.Add(dt1.Columns[i].ColumnName.ToString());
            }

            //��dt�����dt2������
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dt.Columns.Add(dt2.Columns[i].ColumnName.ToString());
            }

            for (int i = 0; i < dtRowCount; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    for (int k = 0; k < dt1.Columns.Count; k++)
                    {
                        if ((dt1.Rows.Count - 1) >= i)
                        {
                            row[k] = dt1.Rows[i].ItemArray[k];
                        }
                    }

                    for (int k = 0; k < dt2.Columns.Count; k++)
                    {
                        if ((dt2.Rows.Count - 1) >= i)
                        {
                            row[dt1.Columns.Count + k] = dt2.Rows[i].ItemArray[k];
                        }
                    }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        public static string MapPath(string Path)
        {
            string str = string.Empty;
            try
            {
                str = HttpContext.Current.Server.MapPath(Path);
            }
            catch
            {
            }
            return str;
        }
        public static string ApplicationPath()
        {
            string str = string.Empty;
            try
            {
                str = MapPath(HttpContext.Current.Request.ApplicationPath);
            }
            catch
            {
            }
            return str;
        }
        public static string GetFileName()
        {
            #region ��ȡ���ڣ������ļ���
            System.Threading.Thread.Sleep(1000);
            string str1 = System.DateTime.Now.Year.ToString();
            str1 += System.DateTime.Now.Month < 10 ? "0" + System.DateTime.Now.Month.ToString() : System.DateTime.Now.Month.ToString();
            str1 += System.DateTime.Now.Day < 10 ? "0" + System.DateTime.Now.Day.ToString() : System.DateTime.Now.Day.ToString();
            str1 += System.DateTime.Now.Hour < 10 ? "0" + System.DateTime.Now.Hour.ToString() : System.DateTime.Now.Hour.ToString();
            str1 += System.DateTime.Now.Minute < 10 ? "0" + System.DateTime.Now.Minute.ToString() : System.DateTime.Now.Minute.ToString();
            str1 += System.DateTime.Now.Second < 10 ? "0" + System.DateTime.Now.Second.ToString() : System.DateTime.Now.Second.ToString();
            str1 += System.DateTime.Now.Millisecond;
            return str1;
            #endregion
        }
        public static string Encrypt(string sPwd, int nType)
        {
            #region MD5����
            string sPassword = string.Empty;
            switch (nType)
            {
                case 0:
                    sPassword = sPwd;
                    break;
                case 1:
                    sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(sPwd, "MD5").Substring(8, 16).ToLower();
                    break;
                case 2:
                    sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(sPwd, "MD5").ToLower();
                    break;
                case 3:
                    sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(FormsAuthentication.HashPasswordForStoringInConfigFile(sPwd, "MD5"), "MD5").ToLower();
                    break;
                case 4:
                    sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(sPwd, "SHA1").ToLower();
                    break;
            }
            return sPassword;
            #endregion
        }
        public static string GetRandomCode(int CodeCount, string allChar)
        {
            //string allChar = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            //string allChar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            int maxV = allCharArray.Length;
            string RandomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * (Guid.NewGuid().GetHashCode()));
                }

                int t = rand.Next(maxV);

                while (temp == t)
                {
                    t = rand.Next(maxV);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }

            return RandomCode;
        }
        public static bool FilesDelete(IList<string> paths,bool needMapPath)
        {
            bool re = true;
            try
            {
                string mapPath = "";
                foreach (string path in paths)
                {
                    if (needMapPath)
                    {
                        mapPath = MapPath(path);
                    }
                    else {
                        mapPath = path;
                    }
                    if (mapPath != "" && File.Exists(mapPath))
                    {
                        File.Delete(mapPath);
                    }
                }
            }
            catch (Exception ex)
            {
                re = false;
            }
            return re;
        }
        /// <summary>
        /// ɾ���ļ����µ��ļ�
        /// </summary>
        /// <param name="dirname">Ŀ¼</param>
        /// <param name="search">�����ַ���</param>
        /// <param name="time">ʱ�䳬���������ɾ��</param>
        public static bool FilesDelete(string dirname, string search, int secondsSpan)
        {
            bool re = true;
            try
            {
                System.IO.DirectoryInfo dir = new DirectoryInfo(dirname);
                System.IO.FileInfo[] files;
                if (search != string.Empty)
                {
                    files = dir.GetFiles(search);
                }
                else
                {
                    files = dir.GetFiles();
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (secondsSpan > 0)
                        {
                            TimeSpan ts = System.DateTime.Now.Subtract(files[i].CreationTime);
                            if (ts.TotalSeconds > secondsSpan)
                                files[i].Delete();
                        }
                        else
                        {
                            files[i].Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                re = false;
            }
            return re;
        }
        public static void SetDdlData(DropDownList ddl, object dataSource, string valueField, string textField)
        {
            ddl.DataSource = dataSource;
            ddl.DataValueField = valueField;
            ddl.DataTextField = textField;
            ddl.DataBind();
        }
        public static void SetDdlDefault(DropDownList ddl, string defaultValue, string defaultText)
        {
            ListItem itemDefault = new ListItem();
            itemDefault.Value = defaultValue;
            itemDefault.Text = defaultText;
            ddl.Items.Insert(0, itemDefault);
        }
        public static void SetDdlData(DropDownList ddl,Type enumType)
        {
            List<KeyValuePair<string, int>> li = new List<KeyValuePair<string, int>>();
            foreach (int v in Enum.GetValues(enumType))
            {
                li.Add(new KeyValuePair<string, int>(Enum.GetName(enumType, v), v));
            }
            ddl.DataSource = li;
            ddl.DataValueField = "Value";
            ddl.DataTextField = "Key";
            ddl.DataBind();
        }
        public static void SetGloTimeFormat()
        {
            System.Globalization.CultureInfo myCI = new System.Globalization.CultureInfo("zh-CN", true);
            myCI.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            myCI.DateTimeFormat.LongDatePattern = "yyyy-MM-dd";
            //myCI.DateTimeFormat.LongDatePattern = "yyyy'��'MM'��'dd'��'";
            myCI.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
            myCI.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            //myCI.DateTimeFormat.LongTimePattern = "H'��'mm'��'ss'��'";
            System.Threading.Thread.CurrentThread.CurrentCulture = myCI;
        }
        public static string GetIpAddr()
        {
            //��ȡ�Ǵ��������ip��ַ���������ȡ���������ip��ַ��HTTP_X_FORWARDED_FOR��
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        [System.Runtime.InteropServices.DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [System.Runtime.InteropServices.DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        public static string GetCustomerMac()
        {
            string IP = Function.GetIpAddr();
            Int32 ldest = inet_addr(IP);
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string mac_dest = "";

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }

            return mac_dest;
        } 
        public static string GetUrlPath()
        {
            return HttpContext.Current.Request.Path.ToString();
        }
        public static string KeepStringLength(string str,int length)
        {
            if (str != null && str.Length > length)
            {
                return str.Remove(length);
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// ���ַ���ת����16�����ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrToHex(string str)
        {
            string strTemp = "";
            string strTempChack = "";
            if (str == "")
                return "";
            byte[] bTemp = System.Text.Encoding.Default.GetBytes(str);
            //byte[] bTemp = System.Text.Encoding.UTF8.GetBytes(str);
            //byte[] bTemp = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(str);
            for (int i = 0; i < bTemp.Length; i++)
            {
                strTempChack = bTemp[i].ToString("X");
                if (strTempChack.Length - 1 < 1)
                {
                    strTempChack = "0" + strTempChack;
                }
                strTemp += strTempChack;
            }
            return strTemp;
        }
        /// <summary>
        /// ���ֽ����黻��16�����ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string BytesToHex(byte[] bTemp)
        {
            string strTemp = "";
            string strTempChack = "";
            if (bTemp.Length == 0)
                return "";
            for (int i = 0; i < bTemp.Length; i++)
            {
                strTempChack = bTemp[i].ToString("X");
                if (strTempChack.Length - 1 < 1)
                {
                    strTempChack = "0" + strTempChack;
                }
                strTemp += strTempChack;
            }
            return strTemp;
        }
        /// <summary>
        /// ��16���ƴ�ת���ɵ�������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float HexToFloat(string hex)
        {
            if (hex.Length == 0)
            {
                return 0;
            }
            else {
                try
                {
                    return BitConverter.ToSingle(BitConverter.GetBytes(Int32.Parse(hex, NumberStyles.AllowHexSpecifier)), 0);
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// ��16���ƴ�ת����˫������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double HexToDouble(string hex)
        {
            if (hex.Length == 0)
            {
                return 0;
            }
            else
            {
                try
                {
                    return BitConverter.ToDouble(BitConverter.GetBytes(Int64.Parse(hex, NumberStyles.AllowHexSpecifier)), 0);
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// ����������������ļ�
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="filePath"></param>
        public static void BytesToFile(byte[] bytes, string filePath)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    ms.WriteTo(fs);
                }
            }
        }
        public static bool CheckTimeRange(DateTime startTime, DateTime endTime, DateTime expStartTime, DateTime? expEndTime)
        {
            if (expEndTime != null)
            {
                return DateTime.Compare(expStartTime, startTime) <= 0 && DateTime.Compare((DateTime)expEndTime, endTime) >= 0;
            }
            else
            {
                return DateTime.Compare(expStartTime, startTime) <= 0;
            }
        }/// <summary>
        /// ��16�����ַ���ת���ֽ����飬ÿ�����ַ���һ���ֽ�byte
        /// </summary>
        /// <param name="hex">16�����ַ���</param>
        /// <returns>�ֽ�����</returns>
        public static byte[] UnHex(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                hex += "20";//�ո�
            }
            // ��Ҫ�� hex ת���� byte ���顣 
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // ÿ�����ַ���һ���ֽ�byte�� 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            return bytes;
        }
        /// <summary>
        /// ���ֽ�����ת��16�����ַ���
        /// </summary>
        /// <param name="bytes">�ֽ�����</param>
        /// <returns>16�����ַ���</returns>
        public static string ToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);//ÿ���ֽڻ�ת����2���ַ��������Կ���Ԥ�������������
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
                //sb.Append(string.Format("{0:X2}", bytes[i] ));
            }
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// �Ѻ�����ת��ʱ��
        /// </summary>
        /// <param name="ms">Ҫת���ĺ�����</param>
        /// <param name="dt">��ʼʱ�䣬ϵͳĬ��Ϊ1970-01-01���ɶ���</param>
        /// <returns>���ʱ��ֵ����ͨ��formatת�ɸ�ʽ���ַ��������磺re.format("yyyy-MM-dd hh:mm:ss.S")</returns>
        public static DateTime ParseMillisecondsToDate(double ms,DateTime startDt)
        {
            long startTicks = startDt.Ticks;
            long re = (long)(ms * 10000 + startTicks);
            return new DateTime(re);
        }
        /// <summary>
        /// �Ѻ�����ת��ʱ��
        /// </summary>
        /// <param name="ms">Ҫת���ĺ�����</param>
        /// <returns>���ʱ��ֵ����ͨ��formatת�ɸ�ʽ���ַ��������磺re.format("yyyy-MM-dd hh:mm:ss.S")</returns>
        public static DateTime ParseMillisecondsToDateFromWinCE(double ms)
        {
            //���������õ���winCEϵͳʱ��ֵ����ʼʱ����1904-01-01����8Сʱ�ǹ���ʱ��
            DateTime startDt = new DateTime(1904, 1, 1, 8, 0, 0);
            return ParseMillisecondsToDate(ms, startDt);
        }

        public static string GetSpanStr(string startStr, string endStr, string testStr)
        {
            Regex reg = new Regex(@"(?s)(?<=" + startStr + ").*?(?=" + endStr + ")");
            return reg.Matches(testStr)[0].Value;
        }

        /// <summary>
        /// ��һ�����ַ�����ֳɵȷֳ��ȵ�С�ַ���
        /// </summary>
        /// <param name="str">Ҫ��ֵ��ַ���</param>
        /// <param name="perLength">��ֺ�ÿ���ַ�������</param>
        /// <returns></returns>
        public static string[] ToArray(string str, int perLength)
        {
            Regex reg = new Regex(@"\w{" + perLength + "}");
            return reg.Matches(str).Cast<Match>().Select(m => m.Value).ToArray();
        }
    }
}

