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
    /// 驾驶员签名逻辑类
    /// </summary>
    public static class SignatureManager
    {
        /// <summary>
        /// 添加驾驶员签名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddSignature(ref Model.Signature model, out string errMsg)
        {
            return SignatureService.AddSignature(ref model, out errMsg);
        }

        /// <summary>
        /// 获取签字内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Signature GetSignatureById(int id, out string errMsg)
        {
            return SignatureService.GetSignatureById(id, out errMsg);
        }

        /// <summary>
        /// 删除签字
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool DeleteSignatureByIds(string ids, out string errMsg)
        {
            return SignatureService.DeleteSignatureByIds(ids, out errMsg);
        }
    }
}
