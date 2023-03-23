using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model.Common
{
    public interface IPerson
    {
        [Required]  int Id { get; set; }
        [Required] string Name { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        ICollection<IExpense> Expenses { get; set; }
        ICollection<IEarning> Earnings { get; set; }
    }
}
