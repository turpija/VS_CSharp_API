
using Budget.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Budget.Service.Common
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();
    }
}