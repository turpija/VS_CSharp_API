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
        public HttpResponseMessage GetAll()
        {
            return Request.CreateResponse(HttpStatusCode.OK, aukcije.List);
        }

        //GET api/auctions/5
        [HttpGet]
        public HttpResponseMessage GetId(int id)
        {
            Oglas oglas = aukcije.List.Find(item => item.Id == id);
            if (oglas == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "id not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, aukcije.List.Find(item => item.Id == id));
        }

        // POST api/auctions
        [HttpPost]
        public HttpResponseMessage Post(Oglas oglasFromBody)
        {
            Oglas oglas = aukcije.List.Find(item => item.Id == oglasFromBody.Id);
            if (oglas != null)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "item with that id already exists");
            }
            aukcije.List.Add(oglasFromBody);
            return Request.CreateResponse(HttpStatusCode.Accepted, aukcije.List.Find(item => item.Id == oglasFromBody.Id));
        }

        // PUT api/auctions/5
        [HttpPut]
        public void Put(int id, Oglas oglas)
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
        }
    }
}