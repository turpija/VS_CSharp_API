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
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, aukcije.List);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
                throw;
            }
        }

        //GET api/auctions/5
        [HttpGet]
        public HttpResponseMessage GetId(int id)
        {
            Oglas oglas = aukcije.List.Find(item => item.Id == id);
            if (oglas == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"item with id:{id} not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, oglas);
        }

        // POST api/auctions
        [HttpPost]
        public HttpResponseMessage Post(Oglas oglasFromBody)
        {
            Oglas oglas = aukcije.List.Find(item => item.Id == oglasFromBody.Id);
            if (oglas != null)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, $"item with id:{oglasFromBody.Id} already exists");
            }
            aukcije.List.Add(oglasFromBody);
            return Request.CreateResponse(HttpStatusCode.Accepted, oglas);
        }

        // PUT api/auctions/5
        [HttpPut]
        public HttpResponseMessage Put(int id, Oglas oglasFromBody)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }
                Oglas itemToUpdate = aukcije.List.Find(item => item.Id == id);
                if (itemToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"item with id: {id} does not exists");
                }

                itemToUpdate.ItemName = string.IsNullOrWhiteSpace(oglasFromBody.ItemName) ? itemToUpdate.ItemName : oglasFromBody.ItemName;
                itemToUpdate.Price = double.IsNaN(oglasFromBody.Price) ? itemToUpdate.Price : oglasFromBody.Price;
                itemToUpdate.Seller = string.IsNullOrWhiteSpace(oglasFromBody.Seller) ? itemToUpdate.Seller : oglasFromBody.Seller;
                itemToUpdate.EndTime = (oglasFromBody.EndTime == DateTime.MinValue) ? itemToUpdate.EndTime : oglasFromBody.EndTime;
                return Request.CreateResponse(HttpStatusCode.OK, itemToUpdate);
        }


        // DELETE api/auctions/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Oglas itemToDelete = aukcije.List.Find(item => item.Id == id);

            if (itemToDelete == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "item with that id does not exists");
            }
            aukcije.List.Remove(itemToDelete);
            return Request.CreateResponse(HttpStatusCode.OK, $"item with id:{id} deleted");
        }
    }
}