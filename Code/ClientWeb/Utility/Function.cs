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
        ///依据连接串名字connectionName返回数据连接字符串
        ///</summary>
        ///<param name="connectionName"></param> 
        ///<returns></returns>
        public static string GetConnectionStringsConfig(string connectionName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();
            return connectionString;
        }
        ///<summary>
        ///返回＊.exe.config文件中appSettings配置节的value项 
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
        ///修改连接字符串
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
            XmlNode node = doc.SelectSingleNode("//connectionStrings");//获取connectionStrings节点
            try
            {
                XmlElement element = (XmlElement)node.SelectSingleNode("//add[@name='" + conName + "']");
                if (element != null)
                {
                    //存在更新子节点
                    element.SetAttribute("connectionString", newConString);
                    doc.Save(path);
                    re = true;
                }
                else
                {
                    //conName不存在
                }
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
            return re;
        }
        ///<summary> 
        ///修改app节点
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
            XmlNode node = doc.SelectSingleNode("//appSettings");//获取connectionStrings节点
            try
            {
                XmlElement element = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
                if (element != null)
                {
                    //存在更新子节点
                    element.SetAttribute("value", newValue);
                    doc.Save(path);
                    re = true;
                }
                else
                {
                    //key不存在
                }
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
            return re;
        }
        /// <summary>
        /// 把datatable格式数据转换成json数据
        /// </summary>
        /// <param name="dt">需要转换的DataTable</param>
        /// <param name="outOffFields">要去掉的字段</param>
        /// <returns>Json结果</returns>
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
        /// 把两个数据表字段合并并返回一个新的合并后的表
        /// </summary>
        /// <param name="dt1">第一个DataTable</param>
        /// <param name="dt2">第二个DataTable</param>
        /// <returns>合并字段后的新表</returns>
        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            DataTable dt = new DataTable();
            //定义dt的行数
            int dtRowCount = 0;

            //dt的行数为dt1或dt2中行数最大的行数
            if (dt1.Rows.Count > dt2.Rows.Count)
            {
                dtRowCount = dt1.Rows.Count;
            }
            else
            {
                dtRowCount = dt2.Rows.Count;
            }

            //向dt中添加dt1的列名
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                dt.Columns.Add(dt1.Columns[i].ColumnName.ToString());
            }

            //向dt中添加dt2的列名
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
            #region 获取日期，用作文件名
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
            #region MD5加密
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
        /// 删除文件夹下的文件
        /// </summary>
        /// <param name="dirname">目录</param>
        /// <param name="search">搜索字符串</param>
        /// <param name="time">时间超过多少秒的删除</param>
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
            //myCI.DateTimeFormat.LongDatePattern = "yyyy'年'MM'月'dd'日'";
            myCI.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
            myCI.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            //myCI.DateTimeFormat.LongTimePattern = "H'点'mm'分'ss'秒'";
            System.Threading.Thread.CurrentThread.CurrentCulture = myCI;
        }
        public static string GetIpAddr()
        {
            //获取非代理服务器ip地址，不建议获取代理服务器ip地址“HTTP_X_FORWARDED_FOR”
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
        /// 把字符串转换成16进制字符串
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
        /// 把字节数组换成16进制字符串
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
        /// 把16进制串转换成单浮点数
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
        /// 把16进制串转换成双浮点数
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
        /// 二进制数据输出到文件
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
        /// 把16进制字符串转成字节数组，每两个字符是一个字节byte
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] UnHex(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个字节byte。 
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
        /// 把字节数组转成16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>16进制字符串</returns>
        public static string ToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);//每个字节会转换成2个字符串，所以可以预定容量提高性能
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
                //sb.Append(string.Format("{0:X2}", bytes[i] ));
            }
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// 把毫秒数转成时间
        /// </summary>
        /// <param name="ms">要转换的毫秒数</param>
        /// <param name="dt">起始时间，系统默认为1970-01-01，可定制</param>
        /// <returns>输出时间值，可通过format转成格式化字符串，比如：re.format("yyyy-MM-dd hh:mm:ss.S")</returns>
        public static DateTime ParseMillisecondsToDate(double ms,DateTime startDt)
        {
            long startTicks = startDt.Ticks;
            long re = (long)(ms * 10000 + startTicks);
            return new DateTime(re);
        }
        /// <summary>
        /// 把毫秒数转成时间
        /// </summary>
        /// <param name="ms">要转换的毫秒数</param>
        /// <returns>输出时间值，可通过format转成格式化字符串，比如：re.format("yyyy-MM-dd hh:mm:ss.S")</returns>
        public static DateTime ParseMillisecondsToDateFromWinCE(double ms)
        {
            //江铃升级用的是winCE系统时间值，起始时间是1904-01-01，加8小时是国际时间
            DateTime startDt = new DateTime(1904, 1, 1, 8, 0, 0);
            return ParseMillisecondsToDate(ms, startDt);
        }

        public static string GetSpanStr(string startStr, string endStr, string testStr)
        {
            Regex reg = new Regex(@"(?s)(?<=" + startStr + ").*?(?=" + endStr + ")");
            return reg.Matches(testStr)[0].Value;
        }

        /// <summary>
        /// 把一个长字符串拆分成等分长度的小字符串
        /// </summary>
        /// <param name="str">要拆分的字符串</param>
        /// <param name="perLength">拆分后每个字符串长度</param>
        /// <returns></returns>
        public static string[] ToArray(string str, int perLength)
        {
            Regex reg = new Regex(@"\w{" + perLength + "}");
            return reg.Matches(str).Cast<Match>().Select(m => m.Value).ToArray();
        }
    }
}

