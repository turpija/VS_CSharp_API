using Budget.Common;
using Budget.DAL;
using Budget.Model;
using Budget.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Repository
{
    public class EfExpenseRepository : IExpenseRepository
    {
        public async Task<ExpenseDTO> GetByIdAsync(string id)
        {
            throw new NotImplementedException();

        }

        public Task<bool> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExpenseDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            BudgetContext context = new BudgetContext();
            List<ExpenseDTO> expenses = new List<ExpenseDTO>();

            var listaEF = context.Expense.ToList();
            foreach (var item in listaEF)
            {
                ExpenseDTO expense = new ExpenseDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Date = item.Date,
                    Cost = item.Cost,
                    Description = item.Description,
                    Category = new CategoryDTO()
                    {
                        Id = item.Category.Id,
                        Name = item.Category.Name,
                    },
                    Person = new PersonDTO()
                    {
                        Id = item.Person.Id,
                        Username = item.Person.Username,
                        Email = item.Person.Email,
                        Password = item.Person.Password
                    }
                };
                expenses.Add(expense);
            }
            return expenses;
        }

        public Task<int> PostAsync(ExpenseDTO expenseFromBody)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateByIdAsync(string id, ExpenseDTO expenseUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
