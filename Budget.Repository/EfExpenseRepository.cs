using Budget.Common;
using Budget.DAL;
using Budget.Model;
using Budget.Model.Common;
using Budget.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Repository
{
    public class EfExpenseRepository : IExpenseRepository
    {
        public BudgetContext Context { get; set; }
        public EfExpenseRepository(BudgetContext context)
        {
            Context = context;
        }
        private ExpenseDTO MapExpenseDTO(Expense expense)
        {
            return new ExpenseDTO()
            {
                Id = expense.Id,
                Name = expense.Name,
                Date = expense.Date,
                Cost = expense.Cost,
                Description = expense.Description,
                Category = new CategoryDTO()
                {
                    Id = expense.Category.Id,
                    Name = expense.Category.Name,
                },
                Person = new PersonDTO()
                {
                    Id = expense.Person.Id,
                    Username = expense.Person.Username,
                    Email = expense.Person.Email,
                    Password = expense.Person.Password
                }
            };
        }

        private Expense mapExpense(ExpenseDTO expenseDTO)
        {
            return new Expense()
            {
                Id = expenseDTO.Id,
                Name = expenseDTO.Name,
                Date = expenseDTO.Date,
                Cost = expenseDTO.Cost,
                Description = expenseDTO.Description,
                CategoryId = expenseDTO.Category.Id,
                PersonId = expenseDTO.Person.Id,
            };
        }

        public async Task<ExpenseDTO> GetByIdAsync(Guid id)
        {
            Expense result = await Context.Expense
                .FirstOrDefaultAsync(s => s.Id == id);

            return MapExpenseDTO(result);
        }

        public async Task<List<ExpenseDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            List<Expense> expenses = await Context.Expense.ToListAsync();
            List<ExpenseDTO> expensesDTO = new List<ExpenseDTO>();
            foreach (var item in expenses)
            {
                expensesDTO.Add(MapExpenseDTO(item));
            }
            return expensesDTO;
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();

        }

        public async Task<int> PostAsync(ExpenseDTO expenseFromBody)
        {
            Context.Expense.Add(mapExpense(expenseFromBody));
            var result = await Context.SaveChangesAsync();
            return result;
        }


        public Task<bool> UpdateByIdAsync(Guid id, ExpenseDTO expenseUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
