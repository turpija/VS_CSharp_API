using Budget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using WebGrease.Css.Extensions;

namespace Budget.Controllers
{
    public class ExpensesController : ApiController
    {
        static DummyList baza = new DummyList();

        // GET all categories
        [Route("api/expenses/categories")]
        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            return Request.CreateResponse(HttpStatusCode.OK, baza.Kategorije);
        }

        // GET all expenses
        [Route("api/expenses/items")]
        [HttpGet]
        public HttpResponseMessage GetItems()
        {
            return Request.CreateResponse(HttpStatusCode.OK, baza.Items);
        }

        //GET all expenses for person
        [Route("api/expenses/{personName}")]
        [HttpGet]
        public HttpResponseMessage GetForPerson(string personName)
        {
            Person osoba = baza.Persons.Find(item => item.Name == personName);
            if (osoba == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "nema tako nekog");
            }
            List<Item> allItems = baza.Items.Where(item => item.Person.Name == personName).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, allItems);
        }

        // POST add new category
        [Route("api/expenses/category")]
        [HttpPost]
        public HttpResponseMessage Post(Category categoryFromBody)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }
            Category categoryToAdd = baza.Kategorije.Find(item => item.Id == categoryFromBody.Id);
            if (categoryToAdd != null)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, $"category with id:{categoryFromBody.Id} already exists");
            }
            baza.Kategorije.Add(categoryFromBody);
            return Request.CreateResponse(HttpStatusCode.Accepted, categoryFromBody);
        }

        // PUT update category
        [Route("api/expenses/category")]
        [HttpPut]
        public HttpResponseMessage Put(Category categoryFromBody)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }
            Category categoryToUpdate = baza.Kategorije.Find(item => item.Id == categoryFromBody.Id);
            if (categoryToUpdate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"category with id: {categoryFromBody.Id} does not exists");
            }

            categoryToUpdate.Name = string.IsNullOrWhiteSpace(categoryFromBody.Name) ? categoryToUpdate.Name : categoryFromBody.Name;
            return Request.CreateResponse(HttpStatusCode.OK, categoryToUpdate);
        }
        // DELETE category
        [Route("api/expenses/category")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Category categoryToDelete = baza.Kategorije.Find(item => item.Id == id);

            if (categoryToDelete == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"category with id:{id} does not exists");
            }
            baza.Kategorije.Remove(categoryToDelete);
            return Request.CreateResponse(HttpStatusCode.OK, $"item with id:{id} deleted");

        }
    }
}