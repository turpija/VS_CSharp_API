using Budget.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Model
{
    public class IncomeDTO : IIncomeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PersonId { get; set; }
        public Guid IncomeCategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PersonDTO Person { get; set; }
        public IncomeCategoryDTO IncomeCategory { get; set; }

    }
}