using Budget.Common;
using Budget.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Repository.Common
{
    public interface IIncomeRepository
    {
        Task<List<IncomeDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering);

    }
}
