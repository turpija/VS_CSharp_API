using Budget.Common;
using Budget.DAL;
using Budget.Model;
using Budget.Model.Common;
using Budget.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Budget.Repository
{
    public class EfExpenseRepository : IExpenseRepository
    {
        // injected DB Context
        protected BudgetContext Context { get; set; }
        public EfExpenseRepository(BudgetContext context)
        {
            Context = context;
        }

        // map Entity model and return DTO
        private ExpenseDTO MapExpenseDTO(Expense expense)
        {
            return new ExpenseDTO()
            {
                Id = expense.Id,
                Name = expense.Name,
                Date = expense.Date,
                Cost = expense.Cost,
                Description = expense.Description,
                Category = new CategoryDTO()
                {
                    Id = expense.Category.Id,
                    Name = expense.Category.Name,
                },
                Person = new PersonDTO()
                {
                    Id = expense.Person.Id,
                    Username = expense.Person.Username,
                    Email = expense.Person.Email,
                    Password = expense.Person.Password
                }
            };
        }

        // map DTO and return Entity model
        private Expense mapExpense(ExpenseDTO expenseDTO)
        {
            Expense expense = new Expense()
            {
                Id = Guid.NewGuid(),
                Name = expenseDTO.Name,
                Date = expenseDTO.Date,
                Cost = expenseDTO.Cost,
                Description = expenseDTO.Description,
                CategoryId = expenseDTO.CategoryId,
                PersonId = expenseDTO.PersonId,
            };
            return expense;
        }


        //---------------------------------------
        //                 GET BY ID
        //---------------------------------------

        public async Task<ExpenseDTO> GetByIdAsync(Guid id)
        {
            try
            {
                Expense result = await Context.Expense
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (result != null)
                {
                    return MapExpenseDTO(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


        //---------------------------------------
        //                 GET
        //---------------------------------------

        public async Task<List<ExpenseDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            try
            {
                // if paging and sorting is not provided, create with default values
                if (paging == null) paging = new Paging();
                if (sorting == null) sorting = new Sorting();

                List<ExpenseDTO> expensesDTO = new List<ExpenseDTO>();

                IQueryable<Expense> query = Context.Expense.AsQueryable();

                // filtering parameters provided
                if (filtering != null)
                {
                    if (filtering.PersonId != default) query = query.Where(s => s.Person.Id == filtering.PersonId);
                    if (filtering.CategoryId != default) query = query.Where(s => s.Category.Id == filtering.CategoryId);
                    if (filtering.DateFrom != default) query = query.Where(s => s.Date >= filtering.DateFrom);
                    if (filtering.DateTo != default) query = query.Where(s => s.Date <= filtering.DateTo);
                    if (filtering.CostFrom != default) query = query.Where(s => s.Cost >= filtering.CostFrom);
                    if (filtering.CostTo != default) query = query.Where(s => s.Cost <= filtering.CostTo);
                }

                // sorting parameters provided
                if (sorting != null)
                {

                    switch (sorting.OrderBy)
                    {
                        case "Cost":
                            if (sorting.SortOrderAsc) query = query.OrderBy(s => s.Cost);
                            else query = query.OrderByDescending(s => s.Cost);
                            break;
                        case "Date":
                            if (sorting.SortOrderAsc) query = query.OrderBy(s => s.Date);
                            else query = query.OrderByDescending(s => s.Date);
                            break;
                        case "Name":
                            if (sorting.SortOrderAsc) query = query.OrderBy(s => s.Name);
                            else query = query.OrderByDescending(s => s.Name);
                            break;
                        default:
                            break;
                    }
                }

                // set paging
                query = query
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize);

                await query.ToListAsync();

                // map list to DTO model
                foreach (var item in query)
                {
                    expensesDTO.Add(MapExpenseDTO(item));
                }
                return expensesDTO;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


        //---------------------------------------
        //                 DELETE
        //---------------------------------------

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                // get item by id
                var itemToRemove = await Context.Expense.Where(s => s.Id == id).FirstOrDefaultAsync();

                // if exists, remove it                
                if (itemToRemove != null)
                {
                    Context.Expense.Remove(itemToRemove);
                    int result = await Context.SaveChangesAsync();
                    if (result > 0) return true;
                    else return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        //---------------------------------------
        //                 POST
        //---------------------------------------
        public async Task<int> PostAsync(ExpenseDTO expenseFromBody)
        {
            try
            {
                // get DTO, map it to Entity and add to DB
                Context.Expense.Add(mapExpense(expenseFromBody));
                int result = await Context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }


        //---------------------------------------
        //                 UPDATE
        //---------------------------------------

        public async Task<bool> UpdateByIdAsync(Guid id, ExpenseDTO expenseUpdated)
        {
            try
            {
                // get item by id
                var itemToUpdate = Context.Expense.Where(s => s.Id == id).FirstOrDefault();

                // if exist, map DTO to Entity model and update it to DB
                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = expenseUpdated.Name;
                    itemToUpdate.Date = expenseUpdated.Date;
                    itemToUpdate.Cost = expenseUpdated.Cost;
                    itemToUpdate.Description = expenseUpdated.Description;
                    itemToUpdate.CategoryId = expenseUpdated.CategoryId;
                    itemToUpdate.PersonId = expenseUpdated.PersonId;
                    int result = await Context.SaveChangesAsync();
                    if (result > 0) return true;
                    else return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
