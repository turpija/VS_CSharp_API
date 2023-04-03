using Budget.DAL;
using Budget.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Budget.Repository.Common
{
    public interface ICategoryRepository
    {
        BudgetV2Context Context { get; set; }

        Task<List<CategoryDTO>> GetAllAsync();
    }
}