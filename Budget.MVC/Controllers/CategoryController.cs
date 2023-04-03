using Budget.Model;
using Budget.MVC.Models;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Budget.MVC.Controllers
{
    public class CategoryController : Controller
    {
        // create service & interface
        // create repository & interface
        // create DI
        // create get all method 

        public ICategoryService Service { get; set; }

        public CategoryController(ICategoryService service)
        {
            Service = service;
        }

        // List all categories
        public async Task<List<CategoryView>> List()
        {
            List<CategoryDTO> categories = await Service.GetAllAsync();
            List<CategoryView> categoriesView = new List<CategoryView>();

            if (categories == null)
            {
                return null;
            }

            foreach (var item in categories)
            {
                categoriesView.Add(new CategoryView()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }

            return categoriesView;
        }
    }
}