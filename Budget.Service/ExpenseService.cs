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

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await repository.GetExpensesAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(string id)
        {
            return await repository.GetExpenseByIdAsync(id);
        }

        public async Task<int> PostExpenseAsync(Expense expenseFromBody)
        {
            return await repository.PostExpenseAsync(expenseFromBody);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            if (await GetExpenseByIdAsync(id) == null)
            {
                return false;
            }
            return await repository.DeleteByIdAsync(id);
        }

        public async Task<bool> UpdateByIdAsync(string id, Expense newExpense)
        {
            // postoji li expense s tim ID ? 
            Expense currentExpense = await GetExpenseByIdAsync(id);
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
