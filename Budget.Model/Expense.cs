using System;
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
        public Guid PersonId { get; set; }
        //public string PersonName { get; set; }
        public Guid CategoryId { get; set; }
        //public string CategoryName { get; set; }
        public DateTime Date { get; set; }
        public Decimal Cost { get; set; }
        public IPerson Person { get; set; }
        public ICategory Category { get; set; }
    }
}