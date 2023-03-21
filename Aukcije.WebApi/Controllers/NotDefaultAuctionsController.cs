using Aukcije.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aukcije.WebApi.Controllers
{
    public class NotDefaultAuctionsController : ApiController
    {
        static Auctions aukcije = new Auctions();

        // GET api/notdefaultauctions/getall
        [Route("api/getall")]
        [HttpGet]
        public IEnumerable<Oglas> GetAll()
        {
            return aukcije.List;
        }

        // GET api/notdefaultauctions/getid
        [Route("api/getid")]
        [HttpGet]
        public Oglas Get([FromBody] int id)
        {
            return aukcije.List.Find(item => item.Id == id);
        }

        // POST api/auctions --from uri
        [HttpPost]
        public void Post([FromUri] Oglas oglas)
        {
            aukcije.List.Add(oglas);
            Console.WriteLine(aukcije.List);
        }



        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}