namespace Utility
{
    using System;

    public sealed class Script
    {
        public static void Alert(string sMessage)
        {
            Responses.Write("<script>alert('" + sMessage + "');history.go(-1);</script>");
            Responses.End();
        }

        public static void Close()
        {
            Responses.Write("<script type = \"text/javascript\">window.opener = null;window.open('', '_self');window.close();</script>");
            Responses.End();
        }

        public static void Go(string sUrl)
        {
            Responses.Write("<script type = \"text/javascript\">window.location.href='" + sUrl + "';</script>");
            Responses.End();
        }

        public static void MsgClose(string sMessage)
        {
            Responses.Write("<script type = \"text/javascript\">alert('" + sMessage + "');window.opener = null;window.open('', '_self');window.close();</script>");
            Responses.End();
        }

        public static void MsgGo(string sMessage, string sUrl)
        {
            Responses.Write("<script type = \"text/javascript\">alert('" + sMessage + "');window.location.href='" + sUrl + "';</script>");
            Responses.End();
        }

        public static void WriteJs(string jsStr)
        {
            Responses.Write("<script type = \"text/javascript\">" + jsStr + "</script>");
            Responses.End();
        }
    }
}

