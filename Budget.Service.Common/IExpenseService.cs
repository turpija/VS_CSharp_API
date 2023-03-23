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
        List<Expense> GetExpenses();
        Expense GetExpenseById(string id);
    }
}
