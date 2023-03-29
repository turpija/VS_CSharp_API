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
        //ExpenseRepository repository = new ExpenseRepository();

        public IExpenseRepository Repository { get; set; }

        public ExpenseService(IExpenseRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<Expense>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            return await Repository.GetAllAsync(paging, sorting, filtering);
        }

        public async Task<Expense> GetByIdAsync(string id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public async Task<int> PostAsync(Expense expense)
        {
            return await Repository.PostAsync(expense);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            if (await GetByIdAsync(id) == null)
            {
                return false;
            }
            return await Repository.DeleteByIdAsync(id);
        }

        public async Task<bool> UpdateByIdAsync(string id, Expense newExpense)
        {
            // postoji li expense s tim ID ? 
            Expense currentExpense = await GetByIdAsync(id);
            if (currentExpense == null)
            {
                return false;
            }

            Expense expenseUpdated = new Expense();

            expenseUpdated.Name = newExpense.Name == default ? currentExpense.Name : newExpense.Name;
            expenseUpdated.Cost = newExpense.Cost == default ? currentExpense.Cost : newExpense.Cost;
            expenseUpdated.Date = newExpense.Date == default ? currentExpense.Date : newExpense.Date;
            expenseUpdated.PersonId = newExpense.PersonId == default ? currentExpense.PersonId : newExpense.PersonId;
            expenseUpdated.CategoryId = newExpense.CategoryId == default ? currentExpense.CategoryId : newExpense.CategoryId;

            return await Repository.UpdateByIdAsync(id, expenseUpdated);
        }




    }
}
