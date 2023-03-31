using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model.Common
{
    public class IIncomeCategoryDTO
    {
        Guid Id { get; set; }
        string Name { get; set; }
        ICollection<IIncomeDTO> Incomes { get; set; }
    }
}
