using Budget.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Service.Common
{
    public interface IExpenseService
    {
        List<Expense> GetAll();
        Expense GetById(string id);
        int Post(Expense expenseFromBody);
        bool DeleteById(string id);
        bool UpdateById(string id, Expense newExpense);


    }
}
