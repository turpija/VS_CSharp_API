using Budget.Model.Common;
using Budget.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Models
{
    public class IncomeRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PersonId { get; set; }
        public Guid IncomeCategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PersonRest Person { get; set; }
        public IncomeCategoryRest IncomeCategory { get; set; }

    }
}