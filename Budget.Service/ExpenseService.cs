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

        public List<Expense> GetExpenses()
        {
            return repository.GetExpenses();
        }

        public Expense GetExpenseById(string id)
        {
            return repository.GetExpenseById(id);
        }

        public int PostExpense(Expense expenseFromBody)
        {
            return repository.PostExpense(expenseFromBody);
        }

        public bool DeleteById(string id)
        {
            if (GetExpenseById(id) == null)
            {
                return false;
            }
            return repository.DeleteById(id);
        }

        public bool UpdateById(string id, Expense newExpense)
        {
            // postoji li expense s tim ID ? 
            Expense currentExpense = GetExpenseById(id);
            if (currentExpense == null)
            {
                return false;
            }

            //postoji expense s tim ID
            // usporedi koje se vrijednosti trebaju promjeniti
            Expense expenseUpdated = new Expense();

            expenseUpdated.Name = newExpense.Name == default ? currentExpense.Name : newExpense.Name;
            expenseUpdated.Cost = newExpense.Cost == default ? currentExpense.Cost : newExpense.Cost;
            expenseUpdated.Date = newExpense.Date == default ? currentExpense.Date : newExpense.Date;
            expenseUpdated.PersonId = newExpense.PersonId == default ? currentExpense.PersonId : newExpense.PersonId;
            expenseUpdated.CategoryId = newExpense.CategoryId == default ? currentExpense.CategoryId : newExpense.CategoryId;

            return repository.UpdateById(id, expenseUpdated);
        }




    }
}
