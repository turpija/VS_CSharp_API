﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Budget.Model.Common;


namespace Budget.Model
{
    public class Expense : IExpense
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required] public Guid PersonId { get; set; }
        [Required] public Guid CategoryId { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public Decimal Cost { get; set; }
        public IPerson Person { get; set; }
        public ICategory Category { get; set; }
    }
}