using Budget.Models;
using Budget.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Service
{
    public class ExpenseService
    {

        public List<Expense> GetExpenses()
        {
            ExpenseRepository repository = new ExpenseRepository();
            List<Expense> expenses = repository.GetExpenses();

            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }
    }




}
