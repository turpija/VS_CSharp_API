using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.MVC.Models
{
    public class ExpenseView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public PersonView Person { get; set; }
       
        public CategoryView Category { get; set; }
    }
}