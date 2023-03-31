using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model.Common
{
    public interface IPersonDTO
    {
        Guid Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        ICollection<IExpenseDTO> Expenses { get; set; }
        ICollection<IIncomeDTO> Incomes { get; set; }
    }
}
