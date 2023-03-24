using Budget.Model;
using Budget.Repository;
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
        ExpenseRepository repository = new ExpenseRepository();

        public async Task<List<Expense>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Expense> GetByIdAsync(string id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<int> PostAsync(Expense expenseFromBody)
        {
            return await repository.PostAsync(expenseFromBody);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            if (await GetByIdAsync(id) == null)
            {
                return false;
            }
            return await repository.DeleteByIdAsync(id);
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

            return await repository.UpdateByIdAsync(id, expenseUpdated);
        }




    }
}
