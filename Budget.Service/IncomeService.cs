using Budget.Common;
using Budget.Model;
using Budget.Repository.Common;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Service
{
    public class IncomeService : IIncomeService
    {
        // Injected repository
        protected IIncomeRepository Repository { get; set; }
        public IncomeService(IIncomeRepository repository)
        {
            Repository = repository;
        }
        public async Task<List<IncomeDTO>> GetAllAsync(Paging pager, Sorting sorting, FilteringIncome filtering)
        {
            return await Repository.GetAllAsync(pager, sorting, filtering);
        }
    }
}
