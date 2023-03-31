using Budget.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Model
{
    public class CategoryDTO : ICategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<IExpense> Expenses { get; set; }

    }
}