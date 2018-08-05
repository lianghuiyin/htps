using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json;
using Utility;
using Model;
using BLL;

namespace API.Controllers
{
    public class CarsController : ApiController
    {
        private Authorizer auth;

        public HttpResponseMessage Get(string key, int count, int lastId)
        {
            string errMsg = "";
            IList<Model.Car> listCar = BLL.CarManager.GetArchivedCars(key, count, lastId, out errMsg);
            HttpStatusCode status;
            string json;
            //errMsg = "网络繁忙，请稍稍再度";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new
                {
                    errors = new
                    {
                        ServerSideError = errMsg
                    }
                };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg2 = new
                {
                    Cars = listCar
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Get(int id)
        {
            string errMsg = "";
            Model.Car car = BLL.CarManager.GetCarById(id, out errMsg);
            HttpStatusCode status;
            string json;
            //errMsg = "网络繁忙，请稍稍再度";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new
                {
                    errors = new
                    {
                        ServerSideError = errMsg
                    }
                };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg2 = new
                {
                    car = car
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Post(CarWrap carWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Car model = carWrap.Car;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Manager, out auth, out errMsg);
                if (isChecked)
                {
                    if (tryValidate(model, out errMsg))
                    {
                        if (this.tryPost(ref model, out errMsg))
                        {
                            if (!ChangesetManager.tryFetch(auth.Sync, ref changeset, out errMsgForChangeset))
                            {
                                isChangesetError = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            var status = HttpStatusCode.OK;
            string json = "";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new { errors = new { ServerSideError = errMsg } };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg = new { Car = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Put(int id, CarWrap carWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Car model = carWrap.Car;
            model.Id = id;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Manager, out auth, out errMsg);
                if (isChecked)
                {
                    if (tryValidate(model, out errMsg))
                    {
                        if (this.tryPut(ref model, out errMsg))
                        {
                            if (!ChangesetManager.tryFetch(auth.Sync, ref changeset, out errMsgForChangeset)) {
                                isChangesetError = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            var status = HttpStatusCode.OK;
            string json = "";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new { errors = new { ServerSideError = errMsg } };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg = new { Car = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Delete(int id)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Manager, out auth, out errMsg);
                if (isChecked)
                {
                    if (id > 0)
                    {
                        if (this.tryDelete(id.ToString(), out errMsg))
                        {
                            if (!ChangesetManager.tryFetch(auth.Sync, ref changeset, out errMsgForChangeset))
                            {
                                isChangesetError = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            var status = HttpStatusCode.OK;
            string json = "";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new { errors = new { ServerSideError = errMsg } };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg = new { Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        private bool tryValidate(Model.Car model, out string errMsg)
        {
            errMsg = "";
            bool re = true;
            if (model.Number == null || model.Number.Length == 0 || model.Number.Length > 20
                || model.Vin == null || model.Vin.Length == 0 || model.Vin.Length > 100
                || model.Model.Length > 20
                || model.Description.Length > 200)
            {
                errMsg = "输入数据不合法";
                re = false;
            }
            if (re && CarManager.CheckRepeatForNumber(model))
            {
                errMsg = "内部编号不能重复";
                re = false;
            }
            if (re && CarManager.CheckRepeatForVin(model))
            {
                errMsg = "Vin不能重复";
                re = false;
            }
            if (re && model.Modifier != auth.UserId)
            {
                errMsg = "登录信息异常，请刷新浏览器以重启应用(APP请退出应用后重新打开)";
                re = false;
            }
            return re;
        }

        private bool tryPut(ref Model.Car model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.Modifier = this.auth.UserId;
            model.ModifiedDate = DateTime.Now;
            if (CarManager.ModifyCarById(ref model, out errMsg))
            {
                re = true;
            }
            return re;
        }

        private bool tryPost(ref Model.Car model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.Creater = this.auth.UserId;
            model.CreatedDate = DateTime.Now;
            model.Modifier = this.auth.UserId;
            model.ModifiedDate = DateTime.Now;
            if (CarManager.AddCar(ref model, out errMsg))
            {
                re = true;
            }
            return re;
        }

        private bool tryDelete(string ids, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            if (ids != string.Empty)
            {
                if (CarManager.DeleteCarByIds(ids, out errMsg))
                {
                    re = true;
                }
            }
            return re;
        }
    }
}