using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model.Common
{
    public class IIncomeDTO
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        Guid PersonId { get; set; }
        Guid IncomeCategoryId { get; set; }
        DateTime Date { get; set; }
        decimal Amount { get; set; }
        IPersonDTO Person { get; set; }
        IIncomeCategoryDTO IncomeCategory { get; set; }
    }
}
