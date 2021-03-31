using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TourManagementApi.Controllers
{
    public class TourController : ApiController
    {
        // GET: api/Tour
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tour/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tour
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tour/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tour/5
        public void Delete(int id)
        {
        }
    }
}
