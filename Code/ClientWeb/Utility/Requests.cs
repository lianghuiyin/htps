namespace Utility
{
    using System;
    using System.Web;

    public sealed class Requests
    {
        public static int GetIntForm(string Key)
        {
            int num = 0;
            try
            {
                //num = Convert.ToInt32(HttpContext.Current.Request.Form[Key]);
                num = Convert.ToInt32(HttpContext.Current.Request[Key]);
            }
            catch
            {
            }
            return num;
        }

        public static int GetIntQueryString(string Key)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(HttpContext.Current.Request.QueryString[Key]);
            }
            catch
            {
            }
            return num;
        }

        public static string GetIntsForm(string Key)
        {
            string str = "0";
            try
            {
                //string str2 = HttpContext.Current.Request.Form[Key];
                string str2 = HttpContext.Current.Request[Key];
                foreach (string str3 in str2.Split(new char[] { ',' }))
                {
                    if (str == "0")
                    {
                        str = Convert.ToInt32(str3).ToString();
                    }
                    else
                    {
                        str = str + "," + Convert.ToInt32(str3).ToString();
                    }
                }
            }
            catch
            {
            }
            return str;
        }

        public static double GetDoubleQueryString(string Key)
        {
            double num = 0;
            try
            {
                num = double.Parse(HttpContext.Current.Request.QueryString[Key].ToString());
            }
            catch
            {
            }
            return num;
        }

        public static float GetFloatForm(string Key)
        {
            float num = 0;
            try
            {
                num = float.Parse(HttpContext.Current.Request[Key].ToString());
            }
            catch
            {
            }
            return num;
        }

        public static string GetStringForm(string Key)
        {
            string str = string.Empty;
            try
            {
                //str = HttpContext.Current.Request.Form[Key].Trim();
                str = HttpContext.Current.Request[Key].Trim();
            }
            catch
            {
            }
            return str;
        }

        public static string GetStringQueryString(string Key)
        {
            string str = "";
            try
            {
                str = HttpContext.Current.Request.QueryString[Key].Trim();
            }
            catch
            {
            }
            return str;
        }

        public static string GetUrlQueryString()
        {
            string str = string.Empty;
            try
            {
                str = HttpContext.Current.Request.QueryString.ToString().Trim();
            }
            catch
            {
            }
            return str;
        }

        public static string RawUrl
        {
            get
            {
                return HttpContext.Current.Request.RawUrl;
            }
        }

        public static string ApplicationPath
        {
            get
            {
                return HttpContext.Current.Request.ApplicationPath;
            }
        }

        public static string Path
        {
            get
            {
                return HttpContext.Current.Request.Path;
            }
        }

        public static Nullable<DateTime> GetDateTimeQueryString(string Key)
        {
            Nullable<DateTime> re = null;
            try
            {
                re = DateTime.Parse(HttpContext.Current.Request.QueryString[Key].Trim());
            }
            catch
            {
            }
            return re;
        }

        public static Nullable<DateTime> GetDateTimeForm(string Key)
        {
            Nullable<DateTime> re = null;
            try
            {
                //re = DateTime.Parse(HttpContext.Current.Request.Form[Key].Trim());
                re = DateTime.Parse(HttpContext.Current.Request[Key].Trim());
            }
            catch
            {
            }
            return re;
        }
    }
}

