using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;

namespace BLL
{
    /// <summary>
    /// Token业务逻辑类
    /// </summary>
    public static class TokenManage
    {
        private static string sharedKey = "horizon2003_htps";
        /// <summary>
        /// 把token转成Authorizer
        /// </summary>
        /// <returns></returns>
        public static Model.Authorizer GetAuthorizer(string token,out string errMsg)
        {
            errMsg = "";
            Model.Authorizer auth = new Authorizer();
            try
            {
                Token jwt = JWT.JsonWebToken.DecodeToObject<Model.Token>(token, sharedKey);
                auth.Sync = jwt.Sync;
                if (jwt.UserId > 0)
                {
                    auth.IsAuthorized = true;
                    auth.UserId = jwt.UserId;
                    if (jwt.Powers.Length > 0)
                    {
                        string[] powers = jwt.Powers.Split(',');
                        if (powers.Contains(((int)PowerStatusCode.Systemer).ToString()))
                        {
                            auth.IsSystemer = true;
                        }
                        if (powers.Contains(((int)PowerStatusCode.Manager).ToString()))
                        {
                            auth.IsManager = true;
                        }
                        if (powers.Contains(((int)PowerStatusCode.Checker).ToString()))
                        {
                            auth.IsChecker = true;
                        }
                        if (powers.Contains(((int)PowerStatusCode.Scanner).ToString()))
                        {
                            auth.IsScanner = true;
                        }
                        if (powers.Contains(((int)PowerStatusCode.Loser).ToString()))
                        {
                            auth.IsLoser = true;
                        }
                    }
                }
                else
                {
                    auth.IsAuthorized = false;
                    errMsg = "没有通过登录验证！";
                }
            }
            catch (Exception ex)
            {
                auth.IsAuthorized = false;
                errMsg = ex.Message;
            }
            return auth;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="token"></param>
        /// <param name="powerStatusCode"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        public static bool CheckAuthorizer(Model.Authorizer auth, PowerStatusCode powerStatusCode, out string errMsg)
        {
            errMsg = "";
            bool reValue = false;
            switch (powerStatusCode)
            {
                case PowerStatusCode.None:
                    reValue = true;
                    break;
                case PowerStatusCode.Systemer:
                    reValue = auth.IsSystemer;
                    break;
                case PowerStatusCode.Manager:
                    reValue = auth.IsManager;
                    break;
                case PowerStatusCode.Checker:
                    reValue = auth.IsChecker;
                    break;
                case PowerStatusCode.Scanner:
                    reValue = auth.IsScanner;
                    break;
                case PowerStatusCode.Loser:
                    reValue = auth.IsLoser;
                    break;
                default:
                    reValue = false;
                    break;
            }
            if (!reValue)
            {
                errMsg = "您没有权限执行该操作！";
            }
            return reValue;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="token"></param>
        /// <param name="powerStatusCode"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        public static bool CheckAuthorizer(string token,PowerStatusCode powerStatusCode,out Model.Authorizer auth,out string errMsg) {
            auth = GetAuthorizer(token, out errMsg);
            if (auth.IsAuthorized)
            {
                return CheckAuthorizer(auth, powerStatusCode, out errMsg);
            }
            else {
                return false;
            }
        }


        public static bool CheckAuthorizer(string token, PowerStatusCode powerStatusCode, out string errMsg)
        {
            Model.Authorizer auth;
            return CheckAuthorizer(token, powerStatusCode, out auth,out errMsg);
        }
    }
}
