namespace Utility
{
    using System;
    using System.Web;
    using System.Configuration;

    public sealed class Configs
    {
        public static int GetIntConfig(string Key)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(ConfigurationManager.AppSettings[Key].ToString());
            }
            catch
            {
            }
            return num;
        }

        public static string GetStringConfig(string Key)
        {
            string str = string.Empty;
            try
            {
                str = ConfigurationManager.AppSettings[Key].ToString().Trim();
            }
            catch
            {
            }
            return str;
        }

        public static bool SetConfigV(string Key,string Val)
        {
            try
            {
                ConfigurationManager.AppSettings.Set(Key, Val);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

