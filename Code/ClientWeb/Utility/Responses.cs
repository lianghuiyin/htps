namespace Utility
{
    using System;
    using System.Web;

    public sealed class Responses
    {
        public static void Clear()
        {
            HttpContext.Current.Response.Clear();
        }

        public static void End()
        {
            HttpContext.Current.Response.End();
        }

        public static void Redirect(string URL)
        {
            HttpContext.Current.Response.Redirect(URL);
        }

        public static void Write(string Content)
        {
            HttpContext.Current.Response.Write(Content);
        }
    }
}

