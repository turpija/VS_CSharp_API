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
            return repository.GetExpenseById(id) ?? null;
        }

        public int PostExpense(Expense expenseFromBody)
        {
            return repository.PostExpense(expenseFromBody);

        }





    }
}
