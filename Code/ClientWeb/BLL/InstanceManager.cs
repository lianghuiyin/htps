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
    /// 申请单逻辑类
    /// </summary>
    public static class InstanceManager
    {
        /// <summary>
        /// 新建申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddInstance(ref Model.Instancenew model, out string errMsg)
        {
            return InstanceService.AddInstance(ref model, out errMsg);
        }

        /// <summary>
        /// 新建申请单履历
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddTrace(ref Model.Tracenew model, out string errMsg)
        {
            return InstanceService.AddTrace(ref model, out errMsg);
        }

        /// <summary>
        /// 取回申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool RecaptureTrace(ref Model.Tracerecapture model, out string errMsg)
        {
            return InstanceService.RecaptureTrace(ref model, out errMsg);
        }

        /// <summary>
        /// 归档申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ArchiveInstance(ref Model.Instancearchive model, out string errMsg)
        {
            return InstanceService.ArchiveInstance(ref model, out errMsg);
        }

        /// <summary>
        /// 审核申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CheckInstance(ref Model.Instancecheck model, out string errMsg)
        {
            return InstanceService.CheckInstance(ref model, out errMsg);
        }

        /// <summary>
        /// 禁用申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForbidInstance(ref Model.Instanceforbid model, out string errMsg)
        {
            return InstanceService.ForbidInstance(ref model, out errMsg);
        }

        /// <summary>
        /// 启用申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EnableInstance(ref Model.Instanceenable model, out string errMsg)
        {
            return InstanceService.EnableInstance(ref model, out errMsg);
        }
        
        /// <summary>
        /// 中止申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AbortInstance(ref Model.Instanceabort model, out string errMsg)
        {
            return InstanceService.AbortInstance(ref model, out errMsg);
        }

        /// <summary>
        /// 获取车辆申请单
        /// </summary>
        /// <param name="car"></param>
        /// <param name="count"></param>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Instance> GetInstancesByCar(int car, int count, int lastId, out string errMsg)
        {
            return InstanceService.GetInstancesByCar(car, count,lastId, out errMsg);
        }

        /// <summary>
        /// 获取单个申请单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Instance GetInstanceById(int id, out string errMsg)
        {
            return InstanceService.GetInstanceById(id, out errMsg);
        }
        
        /// <summary>
        /// 一键优化
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OneTouchOptimize(ref Model.Optimizer model, out string errMsg)
        {
            return InstanceService.OneTouchOptimize(ref model,out errMsg);
        }
    }
}
