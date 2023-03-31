using Budget.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model
{
    public class IncomeCategoryDTO : IIncomeCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<IIncomeDTO> Incomes { get; set; }
    }
}
