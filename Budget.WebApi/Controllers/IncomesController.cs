using Budget.Common;
using Budget.Model;
using Budget.Models;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Budget.Controllers
{
    public class IncomesController : ApiController
    {

        // injected service
        protected IIncomeService Service { get; set; }
        public IncomesController(IIncomeService service)
        {
            Service = service;
        }

        // map DTO to Rest model
        private IncomeRest MapIncomeRest(IncomeDTO income)
        {
            IncomeRest incomeRestView = new IncomeRest()
            {
                Id = income.Id,
                Name = income.Name,
                Amount = income.Amount,
                Date = income.Date,
                Description = income.Description,
                PersonId = income.Person.Id,
                IncomeCategoryId = income.IncomeCategory.Id,
                Person = new PersonRest()
                {
                    Id = income.Person.Id,
                    Username = income.Person.Username,
                    Email = income.Person.Email
                },
                IncomeCategory = new IncomeCategoryRest()
                {
                    Id = income.IncomeCategory.Id,
                    Name = income.IncomeCategory.Name,
                }
            };
            return incomeRestView;
        }



        //---------------------------------------
        //   INCOMES - GET
        //---------------------------------------

        [Route("api/incomes/")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Paging paging, [FromUri] Sorting sorting, [FromUri] FilteringIncome filtering)
        {
            List<IncomeDTO> incomes = await Service.GetAllAsync(paging, sorting, filtering);
            List<IncomeRest> incomesRestView = new List<IncomeRest>();

            if (incomes== null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }

            foreach (IncomeDTO item in incomes)
            {
                // map ExpenseRest item ...
                incomesRestView.Add(MapIncomeRest(item));
            };

            return Request.CreateResponse(HttpStatusCode.OK, incomesRestView);
        }

        
    }
}