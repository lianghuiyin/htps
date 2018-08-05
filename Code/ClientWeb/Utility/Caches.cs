namespace Utility
{
    using System;
    using System.Web;
    using System.Collections.Generic;

    public sealed class Caches
    {
        public static int GetInt(string key)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(HttpContext.Current.Cache[key]);
            }
            catch
            {
            }
            return num;
        }

        public static string GetString(string key)
        {
            string str = string.Empty;
            try
            {
                str = HttpContext.Current.Cache[key].ToString().Trim();
            }
            catch
            {
            }
            return str;
        }

        public static Dictionary<string, object> getDictionary(string key)
        {
            try
            {
                return (Dictionary<string, object>)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public static bool SetInt(string key,int value)
        {
            try
            {
                HttpContext.Current.Cache[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetString(string key, string value)
        {
            try
            {
                HttpContext.Current.Cache[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetDictionary(string key, Dictionary<string, object> value)
        {
            try
            {
                HttpContext.Current.Cache[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

