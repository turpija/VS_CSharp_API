using Budget.Common;
using Budget.DAL;
using Budget.Model;
using Budget.Model.Common;
using Budget.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        private Expense mapExpense(ExpenseDTO expenseDTO, [Optional] Guid id)
        {
            Expense expense = new Expense()
            {
                Name = expenseDTO.Name,
                Date = expenseDTO.Date,
                Cost = expenseDTO.Cost,
                Description = expenseDTO.Description,
                CategoryId = expenseDTO.CategoryId,
                PersonId = expenseDTO.PersonId,
            };
            if (id != null)
            {
                expense.Id = id;
            }
            else
            {
                expense.Id = Guid.NewGuid();
            }
            return expense;
        }


        //---------------------------------------
        //                 GET BY ID
        //---------------------------------------

        public async Task<ExpenseDTO> GetByIdAsync(Guid id)
        {
            Expense result = await Context.Expense
                .FirstOrDefaultAsync(s => s.Id == id);

            return MapExpenseDTO(result);
        }
        //---------------------------------------
        //                 GET
        //---------------------------------------

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
        //---------------------------------------
        //                 DELETE
        //---------------------------------------

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var itemToRemove = Context.Expense.Where(s => s.Id == id).FirstOrDefault();

            if (itemToRemove != null)
            {
                Context.Expense.Remove(itemToRemove);
                int result = await Context.SaveChangesAsync();
                if (result > 0) return true;
                else return false;
            }
            return false;
        }

        //---------------------------------------
        //                 POST
        //---------------------------------------
        public async Task<int> PostAsync(ExpenseDTO expenseFromBody)
        {
            Context.Expense.Add(mapExpense(expenseFromBody));
            int result = await Context.SaveChangesAsync();
            return result;
        }

        //---------------------------------------
        //                 UPDATE
        //---------------------------------------

        public async Task<bool> UpdateByIdAsync(Guid id, ExpenseDTO expenseUpdated)
        {
            var itemToUpdate = Context.Expense.Where(s => s.Id == id).FirstOrDefault();

            if (itemToUpdate != null)
            {
                //itemToUpdate = mapExpense(expenseUpdated, id);
                itemToUpdate.Name = expenseUpdated.Name;
                itemToUpdate.Date = expenseUpdated.Date;
                itemToUpdate.Cost = expenseUpdated.Cost;
                itemToUpdate.Description = expenseUpdated.Description;
                itemToUpdate.CategoryId = expenseUpdated.CategoryId;
                itemToUpdate.PersonId = expenseUpdated.PersonId;
                int result = await Context.SaveChangesAsync();
                if (result > 0) return true;
                else return false;
            }
            return false;
        }
    }
}
