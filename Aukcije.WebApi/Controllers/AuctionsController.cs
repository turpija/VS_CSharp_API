using Aukcije.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace Aukcije.WebApi.Controllers
{
    public class AuctionsController : ApiController
    {
        Auctions aukcije = new Auctions();

        // GET api/auctions
        [HttpGet]
        public IEnumerable<Oglas> Get()
        {
            return aukcije.List;
        }

        // GET api/auctions/5
        [HttpGet]
        public Oglas Get(int id)
        {
            return aukcije.List.Find(item => item.Id == id);
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