namespace Utility
{
    using System;
    using System.Web;
    using System.Collections.Generic;

    public sealed class Apps
    {
        public static int GetInt(string key)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(HttpContext.Current.Application[key]);
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
                str = HttpContext.Current.Application[key].ToString().Trim();
            }
            catch
            {
            }
            return str;
        }

        public static Dictionary<string, int> getDictionary(string key)
        {
            try
            {
                return (Dictionary<string, int>)HttpContext.Current.Application[key];
            }
            catch
            {
                return null;
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Application.Clear();
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Application.Remove(key);
        }

        public static bool SetInt(string key,int value)
        {
            try
            {
                HttpContext.Current.Application[key] = value;
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
                HttpContext.Current.Application[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetDictionary(string key, Dictionary<string, int> value)
        {
            try
            {
                HttpContext.Current.Application[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

