using Budget.Model;
using Budget.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        ExpenseService service = new ExpenseService();

        // GET all expenses
        [Route("api/expenses/")]
        [HttpGet]
        public HttpResponseMessage GetExpenses()
        {
            List<Expense> expenses = service.GetExpenses();

            if (expenses == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }
            return Request.CreateResponse(HttpStatusCode.OK, expenses);
        }



        //GET expense by id
        [Route("api/expense/{id}")]
        [HttpGet]
        public HttpResponseMessage GetExpenseById(string id)
        {
            Expense expense = service.GetExpenseById(id);
            if (expense == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }

            return Request.CreateResponse(HttpStatusCode.OK, expense);
        }



        // POST expense 
        [Route("api/expense/")]
        [HttpPost]

        public HttpResponseMessage PostExpense(Expense expenseFromBody)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }

            int result = service.PostExpense(expenseFromBody);
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Success, rows affected: {result}");
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "insert into database failed.");
        }



        //DELETE
        [HttpDelete]
        [Route("api/expense/{id}")]
        public HttpResponseMessage DeleteById(string id)
        {
            bool deleteSuccessful = service.DeleteById(id);
            if (!deleteSuccessful)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "delete failed");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "delete successfull");
        }




        //PUT
        [HttpPut]
        [Route("api/expense/{id}")]
        public HttpResponseMessage UpdateById(string id, Expense expenseFromBody)
        {
            bool updateSuccess = service.UpdateById(id, expenseFromBody);
            if (!updateSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no item to update");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "update successful");
        }





    }
}