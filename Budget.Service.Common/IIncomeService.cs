using Budget.Common;
using Budget.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Service.Common
{
    public interface IIncomeService
    {
        Task<List<IncomeDTO>> GetAllAsync(Paging pager, Sorting sorting, Filtering filtering);
    }
}
