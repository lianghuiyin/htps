using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace API.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public string Get(int? project, int? department, DateTime startDate, DateTime endDate)
        {
            return "value1";
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value2";
        }

        // GET api/values/5
        public string Get()
        {
            return "value2";
        }

        //public List<Bill> Get(BillsFilter billsFilter)
        //{
        //    List<Bill> re = new List<Bill>();
        //    Bill bill = new Bill();
        //    bill.Id = 22;
        //    bill.Car = 2;
        //    re.Add(bill);
        //    return re;
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}