namespace Utility
{
    using System;
    using System.Web;

    public sealed class Sessions
    {
        public static int GetIntSession(string key)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(HttpContext.Current.Session[key]);
            }
            catch
            {
            }
            return num;
        }

        public static string GetStringSession(string key)
        {
            string str = string.Empty;
            try
            {
                str = HttpContext.Current.Session[key].ToString().Trim();
            }
            catch
            {
            }
            return str;
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void Abandon()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static bool SetSession(string key,string value)
        {
            try
            {
                HttpContext.Current.Session[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isAdmin()
        {
            try
            {
                if (HttpContext.Current.Session["RoleId"] != null && HttpContext.Current.Session["RoleId"].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

