using Budget.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model.Common
{
    public interface IExpense
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        Guid PersonId { get; set; }
        Guid CategoryId { get; set; }
        DateTime Date { get; set; }
        Decimal Cost { get; set; }
        IPerson Person { get; set; }
        ICategory Category { get; set; }
    }
}

