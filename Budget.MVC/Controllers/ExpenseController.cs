using Budget.Common;
using Budget.Model;
using Budget.Model.Common;
using Budget.MVC.Models;
using Budget.Service.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Budget.MVC.Controllers
{
    public class ExpenseController : Controller
    {
        public IExpenseService Service { get; set; }
        public ExpenseController(IExpenseService service)
        {
            Service = service;
        }

        // map DTO to View model
        private ExpenseView MapExpenseView(ExpenseDTO expense)
        {
            ExpenseView expenseView = new ExpenseView()
            {
                Id = expense.Id,
                Name = expense.Name,
                Cost = expense.Cost,
                Date = expense.Date,
                Description = expense.Description,
                PersonId = expense.Person.Id,
                CategoryId = expense.Category.Id,

                Person = new PersonView()
                {
                    Id = expense.Person.Id,
                    Username = expense.Person.Username,
                    Email = expense.Person.Email
                },
                Category = new CategoryView()
                {
                    Id = expense.Category.Id,
                    Name = expense.Category.Name,
                }
            };
            return expenseView;
        }


        // map View to DTO model
        private ExpenseDTO MapExpense(ExpenseView expenseView)
        {
            ExpenseDTO expense = new ExpenseDTO()
            {
                Name = expenseView.Name,
                Description = expenseView.Description,
                Date = expenseView.Date,
                Cost = expenseView.Cost,
                PersonId = expenseView.PersonId,
                CategoryId = expenseView.CategoryId,
            };
            return expense;
        }



        //---------------------------------------
        //            LIST - GET ALL
        //---------------------------------------

        public async Task<ActionResult> List(Paging paging, Sorting sorting, Filtering filtering)
        {
            ExpenseReturnDTO expenseResult = await Service.GetAllAsync(paging, sorting, filtering);
            List<ExpenseView> expensesView = new List<ExpenseView>();

            if (expenseResult.Expenses == null)
            {
                return View();
            }

            foreach (var item in expenseResult.Expenses)
            {
                expensesView.Add(MapExpenseView(item));
            }

            ViewBag.Filering = filtering;

            ViewBag.OrderBy = sorting == null ? null : sorting.OrderBy;
            ViewBag.SortOrderAsc = sorting.SortOrderAsc;
            ViewBag.TotalPages = expenseResult.TotalPages;
            ViewBag.TotalCount = expenseResult.TotalCount;
            ViewBag.ItemsPerPage = expenseResult.ItemsPerPage;
            ViewBag.PageNumber = expenseResult.PageNumber; 
            ViewBag.Category = new SelectList(await Service.GetCategoriesAsync(), "Id", "Name");


            return View(expensesView);
            //return View(expensesView.ToPagedList(paging.PageNumber, paging.PageSize));

        }


        //---------------------------------------
        //                 GET BY ID
        //---------------------------------------
        public async Task<ActionResult> Details(Guid id)
        {
            ExpenseDTO expense = await Service.GetByIdAsync(id);
            if (expense == null)
            {
                return View();
            }

            return View(MapExpenseView(expense));
        }


        //---------------------------------------
        //                 CREATE
        //---------------------------------------

        public async Task<ActionResult> Create()
        {
            //ViewBag.category = await Service.GetCategoriesAsync() ;
            ViewBag.Category = new SelectList(await Service.GetCategoriesAsync(), "Id", "Name");

            return View();
        }

        // POST: Expense/Create
        [HttpPost]
        public async Task<ActionResult> Create(ExpenseView expenseView)
        {
            try
            {
                ExpenseDTO expenseDTO = MapExpense(expenseView);

                int result = await Service.PostAsync(expenseDTO);
                if (result > 0)
                {
                    // success
                }

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }


        //---------------------------------------
        //                 UPDATE - GET
        //---------------------------------------

        public async Task<ActionResult> Edit(Guid id)
        {
            ExpenseDTO expense = await Service.GetByIdAsync(id);

            if (expense == null)
            {
                return View();
            }

            return View(MapExpenseView(expense));
        }


        //---------------------------------------
        //                 UPDATE - POST
        //---------------------------------------

        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, ExpenseView expenseView)
        {
            try
            {
                ExpenseDTO expense = MapExpense(expenseView);

                bool updateSuccess = await Service.UpdateByIdAsync(id, expense);
                if (!updateSuccess)
                {
                    //no item to update
                }
                // successfully updated

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }


        //---------------------------------------
        //                 DELETE - GET
        //---------------------------------------
        public async Task<ActionResult> Delete(Guid id)
        {
            ExpenseDTO expense = await Service.GetByIdAsync(id);
            if (expense == null)
            {
                return View();
            }
            return View(MapExpenseView(expense));
        }


        //---------------------------------------
        //                 DELETE - POST
        //---------------------------------------

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, ExpenseView expenseView)
        {
            try
            {
                bool deleteSuccessful = await Service.DeleteByIdAsync(id);
                if (!deleteSuccessful)
                {
                    // delete failed
                }
                // delete successfull

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
