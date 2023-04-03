using Budget.Common;
using Budget.Model;
using Budget.MVC.Models;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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



        //---------------------------------------
        //            LIST - GET ALL
        //---------------------------------------

        public async Task<ActionResult> List(Paging paging, Sorting sorting, Filtering filtering)
        {
            List<ExpenseDTO> expenses = await Service.GetAllAsync(paging, sorting, filtering);
            List<ExpenseView> expensesView = new List<ExpenseView>();

            if (expenses == null)
            {
                return View();
            }

            foreach (var item in expenses)
            {
                expensesView.Add(MapExpenseView(item));
            }

            return View(expensesView);
        }


        //---------------------------------------
        //                 GET BY ID
        //---------------------------------------
        public ActionResult Details(int id)
        {
            return View();
        }


        //---------------------------------------
        //                 CREATE
        //---------------------------------------

        public ActionResult Create()
        {
            return View();
        }

        // POST: Expense/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //---------------------------------------
        //                 UPDATE - GET
        //---------------------------------------

        public ActionResult Edit(int id)
        {
            return View();
        }


        //---------------------------------------
        //                 UPDATE - POST
        //---------------------------------------

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //---------------------------------------
        //                 DELETE - GET
        //---------------------------------------
        public ActionResult Delete(int id)
        {
            return View();
        }


        //---------------------------------------
        //                 DELETE - POST
        //---------------------------------------

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
