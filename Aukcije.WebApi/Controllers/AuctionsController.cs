using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aukcije.WebApi.Controllers
{
    public class AuctionsController : ApiController
    {
        // GET api/auctions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/auctions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/auctions
        public void Post([FromBody] string value)
        {
        }

        // PUT api/auctions/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/auctions/5
        public void Delete(int id)
        {
        }
    }
}