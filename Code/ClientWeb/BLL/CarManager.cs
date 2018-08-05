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
    /// 车辆逻辑类
    /// </summary>
    public static class CarManager
    {
        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddCar(ref Model.Car model, out string errMsg)
        {
            return CarService.AddCar(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改车辆
        /// </summary>
        /// <param name="Model.Car"></param>
        /// <returns></returns>
        public static bool ModifyCarById(ref Model.Car model, out string errMsg)
        {
            return CarService.ModifyCarById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteCarByIds(string ids, out string errMsg)
        {
            return CarService.DeleteCarByIds(ids, out errMsg);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Car"></param>
        /// <returns></returns>
        public static bool CheckRepeatForNumber(Model.Car model)
        {
            return CarService.CheckRepeatForNumber(model);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Car"></param>
        /// <returns></returns>
        public static bool CheckRepeatForVin(Model.Car model)
        {
            return CarService.CheckRepeatForVin(model);
        }

        /// <summary>
        /// 车辆归档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ArchiveCar(ref Model.Cararchive model, out string errMsg)
        {
            return CarService.ArchiveCar(ref model,out errMsg);
        }

        /// <summary>
        /// 车辆还原
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool RestoreCar(ref Model.Carrestore model, out string errMsg)
        {
            return CarService.RestoreCar(ref model, out errMsg);
        }

        /// <summary>
        /// 获取已归档车辆
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Car> GetArchivedCars(string key, int count, int lastId, out string errMsg)
        {
            return CarService.GetArchivedCars(key, count, lastId, out errMsg);
        }

        /// <summary>
        /// 获取单个车辆
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Car GetCarById(int id, out string errMsg)
        {
            return CarService.GetCarById(id, out errMsg);
        }
    }
}
