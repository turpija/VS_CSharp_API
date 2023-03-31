using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Budget.Model.Common;


namespace Budget.Model
{
    public class ExpenseDTO : IExpenseDTO
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
        public IPersonDTO Person { get; set; }
        public ICategoryDTO Category { get; set; }
    }
}