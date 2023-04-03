using Budget.DAL;
using Budget.Model;
using Budget.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public BudgetV2Context Context { get; set; }

        public CategoryRepository(BudgetV2Context context)
        {
            Context = context;
        }
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            List<CategoryDTO> categoryDTO = new List<CategoryDTO>();

            var query = await Context.Category.ToListAsync();

            foreach (var item in query)
            {
                categoryDTO.Add(new CategoryDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return categoryDTO;

        }
    }
}
