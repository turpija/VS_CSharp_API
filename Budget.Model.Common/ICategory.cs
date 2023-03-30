using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model.Common
{
    public interface ICategory
    {
        Guid Id { get; set; }
        string Name { get; set; }
        ICollection<IExpense> Expenses { get; set; }

    }
}
