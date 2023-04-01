using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Common
{
    public class FilteringIncome
    {
        public Guid PersonId { get; set; }
        public Guid IncomeCategoryId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? AmountFrom { get; set; } = null;
        public int? AmountTo { get; set; } = null;
    }
}
