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

        public List<Expense> GetAll()
        {
            return repository.GetAll();
        }

        public Expense GetById(string id)
        {
            return repository.GetById(id);
        }

        public int Post(Expense expenseFromBody)
        {
            return repository.Post(expenseFromBody);
        }

        public bool DeleteById(string id)
        {
            if (GetById(id) == null)
            {
                return false;
            }
            return repository.DeleteById(id);
        }

        public bool UpdateById(string id, Expense newExpense)
        {
            Expense currentExpense = GetById(id);
            
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

            return repository.UpdateById(id, expenseUpdated);
        }




    }
}
