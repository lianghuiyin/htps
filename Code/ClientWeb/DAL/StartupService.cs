using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;
using Utility;

namespace DAL
{
    /// <summary>
    /// Startup数据访问类
    /// </summary>
    public static class StartupService
    {
        /// <summary>
        /// 获取Startup
        /// </summary>
        /// <returns></returns>
        public static bool GetStartup(
            ref Model.Startup model,
            out string errMsg)
        {
            IList<Model.User> listUsers = new List<Model.User>();
            IList<Model.Role> listRoles = new List<Model.Role>();
            IList<Model.Power> listPowers = new List<Model.Power>();
            IList<Model.Project> listProjects = new List<Model.Project>();
            IList<Model.Department> listDepartments = new List<Model.Department>();
            IList<Model.Oil> listOils = new List<Model.Oil>();
            IList<Model.Preference> listPreferences = new List<Model.Preference>();
            IList<Model.Car> listCars = new List<Model.Car>();
            IList<Model.Instance> listInstances = new List<Model.Instance>();
            IList<Model.Trace> listTraces = new List<Model.Trace>();
            IList<Model.Bill> listBills = new List<Model.Bill>();
            IList<Model.Piece> listPieces = new List<Model.Piece>();
            //IList<Model.Signature> listSignatures = new List<Model.Signature>();
            //IList<Model.Message> listMessages = new List<Model.Message>();
            errMsg = "";
            bool result;
            try
            {
                DataSet ds = DBHelper.ExecuteGetDataSet(CommandType.StoredProcedure, "proc_StartupSelect", "User,Role,Power,Project,Department,Oil,Preference,Car,Instance,Trace,Bill,Piece", null);
                DataTable dtUsers = ds.Tables["User"];
                foreach (DataRow dr in dtUsers.Rows)
                {
                    listUsers.Add(new User
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Phone = (string)dr["Phone"],
                        Email = (string)dr["Email"],
                        Role = (int)dr["Role"],
                        Signature = (string)dr["Signature"],
                        IsSignNeeded = (bool)dr["IsSignNeeded"],
                        IsEnable = (bool)dr["IsEnable"],
                        Creater = (int)dr["Creater"],
                        CreatedDate = (DateTime)dr["CreatedDate"],
                        Modifier = (int)dr["Modifier"],
                        ModifiedDate = (DateTime)dr["ModifiedDate"]
                    });
                }
                DataTable dtRoles = ds.Tables["Role"];
                foreach (DataRow dr in dtRoles.Rows)
                {
                    Role role = new Role();
                    role.Id = (int)dr["Id"];
                    role.Name = (string)dr["Name"];
                    if (!DBNull.Value.Equals(dr["Powers"]))
                    {
                        string powersStr = (string)dr["Powers"];
                        string[] powersArray = powersStr.Split(new char[]
						{
							','
						});
                        IList<int> powers = new List<int>();
                        int i = 0;
                        int len = powersArray.Length;
                        while (i < len)
                        {
                            int power;
                            if (int.TryParse(powersArray[i], out power))
                            {
                                powers.Add(power);
                            }
                            i++;
                        }
                        role.Powers = powers;
                    }
                    role.Description = (string)dr["Description"];
                    role.Creater = (int)dr["Creater"];
                    role.CreatedDate = (DateTime)dr["CreatedDate"];
                    role.Modifier = (int)dr["Modifier"];
                    role.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listRoles.Add(role);
                }
                DataTable dtPowers = ds.Tables["Power"];
                foreach (DataRow dr in dtPowers.Rows)
                {
                    listPowers.Add(new Power
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Description = (string)dr["Description"]
                    });
                }
                DataTable dtDepartments = ds.Tables["Department"];
                foreach (DataRow dr in dtDepartments.Rows)
                {
                    listDepartments.Add(new Department
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Description = (string)dr["Description"],
                        Creater = (int)dr["Creater"],
                        CreatedDate = (DateTime)dr["CreatedDate"],
                        Modifier = (int)dr["Modifier"],
                        ModifiedDate = (DateTime)dr["ModifiedDate"]
                    });
                }
                DataTable dtProjects = ds.Tables["Project"];
                foreach (DataRow dr in dtProjects.Rows)
                {
                    listProjects.Add(new Project
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        IsEnable = (bool)dr["IsEnable"],
                        Description = (string)dr["Description"],
                        Creater = (int)dr["Creater"],
                        CreatedDate = (DateTime)dr["CreatedDate"],
                        Modifier = (int)dr["Modifier"],
                        ModifiedDate = (DateTime)dr["ModifiedDate"]
                    });
                }
                DataTable dtOils = ds.Tables["Oil"];
                foreach (DataRow dr in dtOils.Rows)
                {
                    listOils.Add(new Oil
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        YellowRate = double.Parse(dr["YellowRate"].ToString()),
                        RedRate = double.Parse(dr["RedRate"].ToString()),
                        Description = (string)dr["Description"],
                        Creater = (int)dr["Creater"],
                        CreatedDate = (DateTime)dr["CreatedDate"],
                        Modifier = (int)dr["Modifier"],
                        ModifiedDate = (DateTime)dr["ModifiedDate"]
                    });
                }
                DataTable dtPreferences = ds.Tables["Preference"];
                foreach (DataRow dr in dtPreferences.Rows)
                {
                    listPreferences.Add(new Preference
                    {
                        Id = (int)dr["Id"],
                        ShortcutHour = (int)dr["ShortcutHour"],
                        FinishHour = (int)dr["FinishHour"],
                        Creater = (int)dr["Creater"],
                        CreatedDate = (DateTime)dr["CreatedDate"],
                        Modifier = (int)dr["Modifier"],
                        ModifiedDate = (DateTime)dr["ModifiedDate"]
                    });
                }
                DataTable dtCars = ds.Tables["Car"];
                foreach (DataRow dr in dtCars.Rows)
                {
                    Car car = new Car();
                    car.Id = (int)dr["Id"];
                    car.Number = (string)dr["Number"];
                    car.Vin = (string)dr["Vin"];
                    car.Model = (string)dr["Model"];
                    car.IsArchived = (bool)dr["IsArchived"];
                    //if (!DBNull.Value.Equals(dr["Instances"]))
                    //{
                    //    string instancesStr = (string)dr["Instances"];
                    //    string[] instsArrayTemp = instancesStr.Split(new char[]
                    //    {
                    //        ','
                    //    });
                    //    int[] instsArray = Array.ConvertAll<string, int>(instsArrayTemp, (string s) => int.Parse(s));
                    //    car.Instances = instsArray.ToList<int>();
                    //}
                    car.InstanceCount = (int)dr["InstanceCount"];
                    car.BillCount = (int)dr["BillCount"];
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        car.PreviousOil = null;
                    }
                    else
                    {
                        car.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    if (DBNull.Value.Equals(dr["LastOil"]))
                    {
                        car.LastOil = null;
                    }
                    else
                    {
                        car.LastOil = new int?((int)dr["LastOil"]);
                    }
                    car.LastVolume = (double)dr["LastVolume"];
                    car.LastMileage = (double)dr["LastMileage"];
                    car.LastRate = (double)dr["LastRate"];
                    car.Description = (string)dr["Description"];
                    car.Creater = (int)dr["Creater"];
                    car.CreatedDate = (DateTime)dr["CreatedDate"];
                    car.Modifier = (int)dr["Modifier"];
                    car.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listCars.Add(car);
                }
                DataTable dtInstances = ds.Tables["Instance"];
                foreach (DataRow dr in dtInstances.Rows)
                {
                    Instance instance = new Instance();
                    instance.Id = (int)dr["Id"];
                    instance.Car = (int)dr["Car"];
                    instance.Project = (int)dr["Project"];
                    instance.Department = (int)dr["Department"];
                    instance.UserName = (string)dr["UserName"];
                    if (!DBNull.Value.Equals(dr["Oils"]))
                    {
                        string oilsStr = (string)dr["Oils"];
                        string[] oilsArrayTemp = oilsStr.Split(new char[]
						{
							','
						});
                        int[] oilsArray = Array.ConvertAll<string, int>(oilsArrayTemp, (string s) => int.Parse(s));
                        instance.Oils = oilsArray.ToList<int>();
                    }
                    instance.Goal = (string)dr["Goal"];
                    instance.StartDate = (DateTime)dr["StartDate"];
                    instance.EndDate = (DateTime)dr["EndDate"];
                    instance.IsReleased = (bool)dr["IsReleased"];
                    instance.IsPending = (bool)dr["IsPending"];
                    instance.IsArchived = (bool)dr["IsArchived"];
                    instance.IsEnable = (bool)dr["IsEnable"];
                    //if (DBNull.Value.Equals(dr["Message"]))
                    //{
                    //    instance.Message = null;
                    //}
                    //else
                    //{
                    //    instance.Message = new int?((int)dr["Message"]);
                    //}
                    instance.BillCount = (int)dr["BillCount"];
                    instance.Creater = (int)dr["Creater"];
                    instance.CreatedDate = (DateTime)dr["CreatedDate"];
                    instance.Modifier = (int)dr["Modifier"];
                    instance.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listInstances.Add(instance);
                }
                DataTable dtTraces = ds.Tables["Trace"];
                foreach (DataRow dr in dtTraces.Rows)
                {
                    Trace trace = new Trace();
                    trace.Id = (int)dr["Id"];
                    trace.Car = (int)dr["Car"];
                    trace.Instance = (int)dr["Instance"];
                    if (DBNull.Value.Equals(dr["PreviousTrace"]))
                    {
                        trace.PreviousTrace = null;
                    }
                    else
                    {
                        trace.PreviousTrace = new int?((int)dr["PreviousTrace"]);
                    }
                    trace.Status = (string)dr["Status"];
                    trace.IsFinished = (bool)dr["IsFinished"];
                    trace.IsArchived = (bool)dr["IsArchived"];
                    trace.Project = (int)dr["Project"];
                    trace.Department = (int)dr["Department"];
                    trace.UserName = (string)dr["UserName"];
                    if (!DBNull.Value.Equals(dr["Oils"]))
                    {
                        string oilsStr = (string)dr["Oils"];
                        string[] oilsArrayTemp = oilsStr.Split(new char[]
						{
							','
						});
                        int[] oilsArray = Array.ConvertAll<string, int>(oilsArrayTemp, (string s) => int.Parse(s));
                        trace.Oils = oilsArray.ToList<int>();
                    }
                    trace.Goal = (string)dr["Goal"];
                    trace.StartDate = (DateTime)dr["StartDate"];
                    trace.EndDate = (DateTime)dr["EndDate"];
                    trace.StartInfo = (string)dr["StartInfo"];
                    trace.EndInfo = (string)dr["EndInfo"];
                    trace.Creater = (int)dr["Creater"];
                    trace.CreatedDate = (DateTime)dr["CreatedDate"];
                    trace.Modifier = (int)dr["Modifier"];
                    trace.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listTraces.Add(trace);
                }
                DataTable dtBills = ds.Tables["Bill"];
                foreach (DataRow dr in dtBills.Rows)
                {
                    Bill bill = new Bill();
                    bill.Id = (int)dr["Id"];
                    bill.Car = (int)dr["Car"];
                    bill.Instance = (int)dr["Instance"];
                    bill.Project = (int)dr["Project"];
                    bill.Department = (int)dr["Department"];
                    bill.Oil = (int)dr["Oil"];
                    bill.Volume = double.Parse(dr["Volume"].ToString());
                    bill.Mileage = double.Parse(dr["Mileage"].ToString());
                    bill.DriverName = (string)dr["DriverName"];
                    if (DBNull.Value.Equals(dr["Signature"]))
                    {
                        bill.Signature = null;
                    }
                    else
                    {
                        bill.Signature = new int?((int)dr["Signature"]);
                    }
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        bill.PreviousOil = null;
                    }
                    else
                    {
                        bill.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    bill.Rate = double.Parse(dr["Rate"].ToString());
                    bill.Oiler = (int)dr["Oiler"];
                    bill.Time = (DateTime)dr["Time"];
                    bill.IsLost = (bool)dr["IsLost"];
                    bill.IsPrinted = (bool)dr["IsPrinted"];
                    bill.Creater = (int)dr["Creater"];
                    bill.CreatedDate = (DateTime)dr["CreatedDate"];
                    bill.Modifier = (int)dr["Modifier"];
                    bill.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listBills.Add(bill);
                }
                DataTable dtPieces = ds.Tables["Piece"];
                foreach (DataRow dr in dtPieces.Rows)
                {
                    Piece piece = new Piece();
                    piece.Id = (int)dr["Id"];
                    piece.Name = (string)dr["Name"];
                    piece.Number = (string)dr["Number"];
                    piece.Order = (string)dr["Order"];
                    piece.Count = (int)dr["Count"];
                    piece.PrintedCount = (int)dr["PrintedCount"];
                    piece.IsPrinted = (bool)dr["IsPrinted"];
                    piece.Ots = (string)dr["Ots"];
                    piece.DelegateNumber = (string)dr["DelegateNumber"];
                    piece.AccessoryFactory = (string)dr["AccessoryFactory"];
                    piece.VehicleType = (string)dr["VehicleType"];
                    piece.TestContent = (string)dr["TestContent"];
                    piece.SendPerson = (string)dr["SendPerson"];
                    piece.ChargePerson = (string)dr["ChargePerson"];
                    if (DBNull.Value.Equals(dr["SendDate"]))
                    {
                        piece.SendDate = null;
                    }
                    else
                    {
                        piece.SendDate = (DateTime?)dr["SendDate"];
                    }
                    piece.Place = (string)dr["Place"];
                    piece.IsEnable = (bool)dr["IsEnable"];
                    piece.Description = (string)dr["Description"];
                    piece.Creater = (int)dr["Creater"];
                    piece.CreatedDate = (DateTime)dr["CreatedDate"];
                    piece.Modifier = (int)dr["Modifier"];
                    piece.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listPieces.Add(piece);
                }
                //DataTable dtSignature = ds.Tables["Signature"];
                //string tempBase64String = null;
                //foreach (DataRow dr in dtSignature.Rows)
                //{
                //    tempBase64String = Convert.ToBase64String((byte[])dr["Sign"]);
                //    listSignatures.Add(new Signature
                //    {
                //        Id = (int)dr["Id"],
                //        Name = (string)dr["Name"],
                //        Sign = tempBase64String,
                //        Creater = (int)dr["Creater"],
                //        CreatedDate = (DateTime)dr["CreatedDate"]
                //    });
                //}
                //DataTable dtMessages = ds.Tables["Message"];
                //foreach (DataRow dr in dtMessages.Rows)
                //{
                //    listMessages.Add(new Message
                //    {
                //        Id = (int)dr["Id"],
                //        Type = (string)dr["Type"],
                //        IsArchived = (bool)dr["IsArchived"],
                //        Title = (string)dr["Title"],
                //        Text = (string)dr["Text"],
                //        Href = (string)dr["Href"],
                //        Creater = (int)dr["Creater"],
                //        CreatedDate = (DateTime)dr["CreatedDate"],
                //        Modifier = (int)dr["Modifier"],
                //        ModifiedDate = (DateTime)dr["ModifiedDate"]
                //    });
                //}
                model.Users = listUsers;
                model.Roles = listRoles;
                model.Powers = listPowers;
                model.Projects = listProjects;
                model.Departments = listDepartments;
                model.Oils = listOils;
                model.Preferences = listPreferences;
                model.Cars = listCars;
                model.Instances = listInstances;
                model.Traces = listTraces;
                model.Bills = listBills;
                model.Pieces = listPieces;
                //model.Signatures = listSignatures;
                //model.Messages = listMessages;
                result = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }
            return result;
        }
    }
}
