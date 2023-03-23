using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Model
{
    public class Category
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }

    }
}