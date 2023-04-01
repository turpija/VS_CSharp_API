using Budget.Common;
using Budget.DAL;
using Budget.Model;
using Budget.Model.Common;
using Budget.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Budget.Repository
{
    public class EfIncomeRepository : IIncomeRepository
    {
        // injected DB Context
        protected BudgetV2Context Context { get; set; }
        public EfIncomeRepository(BudgetV2Context context)
        {
            Context = context;
        }

        // map Entity model and return DTO
        private IncomeDTO MapIncomeDTO(Income income)
        {
            return new IncomeDTO()
            {
                Id = income.Id,
                Name = income.Name,
                Date = income.Date,
                Amount= income.Amount,
                Description = income.Description,
                IncomeCategory = new IncomeCategoryDTO()
                {
                    Id = income.IncomeCategory.Id,
                    Name = income.IncomeCategory.Name,
                },
                Person = new PersonDTO()
                {
                    Id = income.Person.Id,
                    Username = income.Person.Username,
                    Email = income.Person.Email,
                    Password = income.Person.Password
                }
            };
        }

        // map DTO and return Entity model
        //private Expense mapExpense(ExpenseDTO expenseDTO)
        //{
        //    Expense expense = new Expense()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = expenseDTO.Name,
        //        Date = expenseDTO.Date,
        //        Cost = expenseDTO.Cost,
        //        Description = expenseDTO.Description,
        //        CategoryId = expenseDTO.CategoryId,
        //        PersonId = expenseDTO.PersonId,
        //    };
        //    return expense;
        //}



        //---------------------------------------
        //                 GET
        //---------------------------------------

        public async Task<List<IncomeDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            try
            {

                List<IncomeDTO> incomesDTO = new List<IncomeDTO>();

                IQueryable<Income> query = Context.Income.AsQueryable();

                await query.ToListAsync();

                // map list to DTO model
                foreach (var item in query)
                {
                    incomesDTO.Add(MapIncomeDTO(item));
                }
                return incomesDTO;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }




    }
}
