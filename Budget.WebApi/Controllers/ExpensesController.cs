using Budget.Common;
using Budget.Model;
using Budget.Models;
using Budget.Service;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using WebGrease.Css.Extensions;

namespace Budget.Controllers
{
    public class ExpensesController : ApiController
    {
        //ExpenseService service = new ExpenseService();

        public IExpenseService Service { get; set; }
        public ExpensesController(IExpenseService service)
        {
            Service = service;
        }
        private ExpenseRest PopulateExpenseRest(Expense expense)
        {
            ExpenseRest expenseRestView = new ExpenseRest()
            {
                Id = expense.Id,
                Name = expense.Name,
                Cost = expense.Cost,
                Date = expense.Date,
                Description = expense.Description,
                Person = new PersonRest()
                {
                    Id = expense.Person.Id,
                    Username = expense.Person.Username,
                    Email = expense.Person.Email
                },
                Category = new CategoryRest()
                {
                    Id = expense.Category.Id,
                    Name = expense.Category.Name,
                }
            };
            return expenseRestView;
        }

        private Expense PopulateExpense(ExpenseRest expenseRest)
        {
            Expense expense = new Expense()
            {
                Name = expenseRest.Name,
                Description = expenseRest.Description,
                Date = expenseRest.Date,
                Cost = expenseRest.Cost,
                Person = new Person() {Id = expenseRest.Person.Id },
                Category = new Category() {Id = expenseRest.Category.Id }
                //PersonId = expenseRest.PersonId,
                //CategoryId = expenseRest.CategoryId,
            };
            return expense;
        }


        // GET all expenses
        [Route("api/expenses/")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Paging paging, [FromUri] Sorting sorting, [FromUri] Filtering filtering)
        {

            List<Expense> expenses = await Service.GetAllAsync(paging, sorting, filtering);
            List<ExpenseRest> expensesRestView = new List<ExpenseRest>();

            if (expenses == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }

            foreach (Expense item in expenses)
            {
                //populate ExpenseRest item ...
                expensesRestView.Add(PopulateExpenseRest(item));
            };

            return Request.CreateResponse(HttpStatusCode.OK, expensesRestView);
        }



        //GET expense by id
        [Route("api/expense/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByIdAsync(string id)
        {
            Expense expense = await Service.GetByIdAsync(id);

            if (expense == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }
            ExpenseRest expenseRestView = PopulateExpenseRest(expense);

            return Request.CreateResponse(HttpStatusCode.OK, expenseRestView);
        }



        // POST expense 
        [Route("api/expense/")]
        [HttpPost]

        public async Task<HttpResponseMessage> PostAsync(ExpenseRest expenseRest)
        {
            Expense expense = PopulateExpense(expenseRest);

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }

            int result = await Service.PostAsync(expense);
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
            bool deleteSuccessful = await Service.DeleteByIdAsync(id);
            if (!deleteSuccessful)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "delete failed");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "delete successfull");
        }

        //PUT
        [HttpPut]
        [Route("api/expense/{id}")]
        public async Task<HttpResponseMessage> UpdateByIdAsync(string id, ExpenseRest expenseFromBody)
        {
            Expense expense = PopulateExpense(expenseFromBody);

            bool updateSuccess = await Service.UpdateByIdAsync(id, expense);
            if (!updateSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no item to update");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "update successful");
        }





    }
}