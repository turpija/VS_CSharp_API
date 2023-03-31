﻿using Budget.Common;
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
        Task<List<ExpenseDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering);
        Task<ExpenseDTO> GetByIdAsync(Guid id);
        Task<int> PostAsync(ExpenseDTO expenseFromBody);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> UpdateByIdAsync(Guid id, ExpenseDTO expenseUpdated);
    }
}
