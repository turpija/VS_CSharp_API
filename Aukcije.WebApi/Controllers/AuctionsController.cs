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
        static Auctions aukcije = new Auctions();

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
        [HttpPost]
        public void Post([FromBody] Oglas oglas)
        {
            aukcije.List.Add(oglas);
            Console.WriteLine(aukcije.List);
        }

        // PUT api/auctions/5
        [HttpPut]
        public void Put(int id, [FromBody] Oglas oglas)
        {
            Oglas itemToUpdate = aukcije.List.FirstOrDefault(item => item.Id == id);
            itemToUpdate.ItemName = oglas.ItemName;
            itemToUpdate.Price = oglas.Price;
            itemToUpdate.Seller = oglas.Seller;
            itemToUpdate.Price = oglas.Price;
        }

        // DELETE api/auctions/5
        [HttpDelete]
        public void Delete(int id)
        {
            Oglas itemToRemove = aukcije.List.FirstOrDefault(item => item.Id == id);

            aukcije.List.Remove(itemToRemove);
            Console.WriteLine(  );
        }
    }
}