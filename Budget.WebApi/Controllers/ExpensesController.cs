using Budget.Model;
using Budget.Models;
using Budget.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.WebPages;
using WebGrease.Css.Extensions;

namespace Budget.Controllers
{
    public class ExpensesController : ApiController
    {
        private ExpenseRest PopulateExpenseRestView(Expense expense)
        {

            ExpenseRest expenseRestView = new ExpenseRest();
            expenseRestView.Id = expense.Id;
            expenseRestView.Name = expense.Name;
            expenseRestView.Description = expense.Description;
            expenseRestView.Cost = expense.Cost;
            expenseRestView.Date = expense.Date;
            expenseRestView.CategoryId = expense.CategoryId;
            expenseRestView.PersonId = expense.PersonId;

            return expenseRestView;
        }

        ExpenseService service = new ExpenseService();

        // GET all expenses
        [Route("api/expenses/")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            List<Expense> expenses = await service.GetAllAsync();
            List<ExpenseRest> expensesRestView = new List<ExpenseRest>();

            if (expenses == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }

            foreach(Expense item in expenses){
                //populate ExpenseRest item ...
                expensesRestView.Add(PopulateExpenseRestView(item));
            };

            return Request.CreateResponse(HttpStatusCode.OK, expensesRestView);
        }



        //GET expense by id
        [Route("api/expense/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByIdAsync(string id)
        {
            Expense expense = await service.GetByIdAsync(id);

            if (expense == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }
            ExpenseRest expenseRestView = PopulateExpenseRestView(expense);

            return Request.CreateResponse(HttpStatusCode.OK, expenseRestView);
        }



        // POST expense 
        [Route("api/expense/")]
        [HttpPost]

        public async Task<HttpResponseMessage> PostAsync(Expense expenseFromBody)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }

            int result = await service.PostAsync(expenseFromBody);
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Success, rows affected: {result}");
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "insert into database failed.");
        }



        //DELETE
        [HttpDelete]
        [Route("api/expense/{id}")]
        public async Task<HttpResponseMessage> DeleteByIdAsync(string id)
        {
            bool deleteSuccessful = await service.DeleteByIdAsync(id);
            if (!deleteSuccessful)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "delete failed");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "delete successfull");
        }



        //PUT
        [HttpPut]
        [Route("api/expense/{id}")]
        public async Task<HttpResponseMessage> UpdateByIdAsync(string id, Expense expenseFromBody)
        {
            bool updateSuccess = await service.UpdateByIdAsync(id, expenseFromBody);
            if (!updateSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no item to update");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "update successful");
        }





    }
}