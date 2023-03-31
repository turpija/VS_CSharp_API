using Budget.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Model
{
    public class PersonDTO : IPersonDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<IExpenseDTO> Expenses { get; set; }
        public ICollection<IIncomeDTO> Incomes { get; set; }

    }
}