using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.MVC.Models
{
    public class ExpenseView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Date { get; set; }
        public Decimal Cost { get; set; }
        public PersonView Person { get; set; }
        public CategoryView Category { get; set; }
    }
}