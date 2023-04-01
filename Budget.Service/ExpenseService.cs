using Budget.Common;
using Budget.Model;
using Budget.Repository;
using Budget.Repository.Common;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Service
{
    public class ExpenseService : IExpenseService
    {
        // Injected repository
        protected IExpenseRepository Repository { get; set; }
        public ExpenseService(IExpenseRepository repository)
        {
            Repository = repository;
        }


        //---------------------------------------
        //                GET
        //---------------------------------------

        public async Task<List<ExpenseDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            return await Repository.GetAllAsync(paging, sorting, filtering);
        }


        //---------------------------------------
        //                 GET BY ID
        //---------------------------------------

        public async Task<ExpenseDTO> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id);
        }


        //---------------------------------------
        //                 POST
        //---------------------------------------

        public async Task<int> PostAsync(ExpenseDTO expense)
        {
            return await Repository.PostAsync(expense);
        }


        //---------------------------------------
        //                 DELETE
        //---------------------------------------

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            if (await GetByIdAsync(id) == null)
            {
                return false;
            }
            return await Repository.DeleteByIdAsync(id);
        }


        //---------------------------------------
        //                 UPDATE
        //---------------------------------------

        public async Task<bool> UpdateByIdAsync(Guid id, ExpenseDTO newExpense)
        {
            // does item with id exists ? 
            ExpenseDTO currentExpense = await GetByIdAsync(id);
            if (currentExpense == null)
            {
                return false;
            }

            // check what values are provided and create new DTO with updated values

            ExpenseDTO expenseUpdated = new ExpenseDTO();

            expenseUpdated.Name = newExpense.Name == default ? currentExpense.Name : newExpense.Name;
            expenseUpdated.Cost = newExpense.Cost == default ? currentExpense.Cost : newExpense.Cost;
            expenseUpdated.Date = newExpense.Date == default ? currentExpense.Date : newExpense.Date;
            expenseUpdated.PersonId = newExpense.PersonId == default ? currentExpense.Person.Id : newExpense.PersonId;
            expenseUpdated.CategoryId = newExpense.CategoryId == default ? currentExpense.Category.Id : newExpense.CategoryId;

            return await Repository.UpdateByIdAsync(id, expenseUpdated);
        }




    }
}
