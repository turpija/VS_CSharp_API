using Budget.Model;
using Budget.Repository.Common;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Service
{
    public class CategoryService : ICategoryService
    {
        public ICategoryRepository Repository { get; set; }
        public CategoryService(ICategoryRepository repository)
        {
            Repository = repository;
        }
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }
    }
}
