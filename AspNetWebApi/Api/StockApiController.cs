using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Api.Controllers
{
    public class StockApiController : ApiController
    {
        // GET /api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET /api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST /api/<controller>
        public void Post(string value)
        {
        }

        // PUT /api/<controller>/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}