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
        // injected service
        protected IExpenseService Service { get; set; }
        public ExpensesController(IExpenseService service)
        {
            Service = service;
        }

        // map DTO to Rest model
        private ExpenseRest MapExpenseRest(ExpenseDTO expense)
        {
            ExpenseRest expenseRestView = new ExpenseRest()
            {
                Id = expense.Id,
                Name = expense.Name,
                Cost = expense.Cost,
                Date = expense.Date,
                Description = expense.Description,
                PersonId = expense.Person.Id,
                CategoryId = expense.Category.Id,
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

        // map Rest to DTO model
        private ExpenseDTO MapExpense(ExpenseInputRest expenseInputRest)
        {
            ExpenseDTO expense = new ExpenseDTO()
            {
                Name = expenseInputRest.Name,
                Description = expenseInputRest.Description,
                Date = expenseInputRest.Date,
                Cost = expenseInputRest.Cost,
                PersonId = expenseInputRest.PersonId,
                CategoryId = expenseInputRest.CategoryId,
            };
            return expense;
        }


        //---------------------------------------
        //                 GET
        //---------------------------------------
        
        [Route("api/expenses/")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Paging paging, [FromUri] Sorting sorting, [FromUri] Filtering filtering)
        {

            List<ExpenseDTO> expenses = await Service.GetAllAsync(paging, sorting, filtering);
            List<ExpenseRest> expensesRestView = new List<ExpenseRest>();

            if (expenses == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }

            foreach (ExpenseDTO item in expenses)
            {
                // map ExpenseRest item ...
                expensesRestView.Add(MapExpenseRest(item));
            };

            return Request.CreateResponse(HttpStatusCode.OK, expensesRestView);
        }


        //---------------------------------------
        //                 GET BY ID
        //---------------------------------------
        
        [Route("api/expense/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
        {
            ExpenseDTO expense = await Service.GetByIdAsync(id);

            if (expense == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }
            ExpenseRest expenseRestView = MapExpenseRest(expense);

            return Request.CreateResponse(HttpStatusCode.OK, expenseRestView);
        }


        //---------------------------------------
        //                 POST
        //---------------------------------------

        [Route("api/expense/")]
        [HttpPost]

        public async Task<HttpResponseMessage> PostAsync(ExpenseInputRest expenseInputRest)
        {
            ExpenseDTO expense = MapExpense(expenseInputRest);

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


        //---------------------------------------
        //                 DELETE
        //---------------------------------------

        [HttpDelete]
        [Route("api/expense/{id}")]
        public async Task<HttpResponseMessage> DeleteByIdAsync(Guid id)
        {
            bool deleteSuccessful = await Service.DeleteByIdAsync(id);
            if (!deleteSuccessful)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "delete failed");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "delete successfull");
        }


        //---------------------------------------
        //                 UPDATE
        //---------------------------------------

        [HttpPut]
        [Route("api/expense/{id}")]
        public async Task<HttpResponseMessage> UpdateByIdAsync(Guid id, ExpenseInputRest expenseInputRest)
        {
            ExpenseDTO expense = MapExpense(expenseInputRest);

            bool updateSuccess = await Service.UpdateByIdAsync(id, expense);
            if (!updateSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no item to update");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "update successful");
        }





    }
}