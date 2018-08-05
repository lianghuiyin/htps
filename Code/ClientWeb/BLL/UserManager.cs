using System;
using System.Collections.Generic;
using Model;
using DAL;
using Utility;

namespace BLL
{
    /// <summary>
    /// 用户业务逻辑类
    /// </summary>
    public static class UserManager
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public static bool Login(ref Model.Login model, out string errMsg)
        {
            return UserService.Login(ref model, out errMsg);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddUser(ref Model.User model, out string errMsg)
        {
            return UserService.AddUser(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改用户
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool ModifyUserById(ref Model.User model, out string errMsg)
        {
            return UserService.ModifyUserById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteUserByIds(string ids, out string errMsg)
        {
            return UserService.DeleteUserByIds(ids, out errMsg);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool CheckRepeatForPhone(Model.User model)
        {
            return UserService.CheckRepeatForPhone(model);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool CheckRepeatForEmail(Model.User model)
        {
            return UserService.CheckRepeatForEmail(model);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <returns></returns>
        public static bool Resetpwd(ref Model.Resetpwd model, out string errMsg)
        {
            return UserService.Resetpwd(ref model, out errMsg);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public static bool Updatepwd(ref Model.Accountpwd model, out string errMsg)
        {
            return UserService.Updatepwd(ref model, out errMsg);
        }
    }
}
