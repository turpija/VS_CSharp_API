using Budget.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Repository.Common
{
    public interface IExpenseRepository
    {
        List<Expense> GetExpenses();
        Expense GetExpenseById(string id);
        int PostExpense(Expense expenseFromBody);
        bool DeleteById(string id);
        bool UpdateById(string id, Expense expenseUpdated);
    }
}
