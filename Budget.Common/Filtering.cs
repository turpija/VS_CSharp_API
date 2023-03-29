using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Common
{
    public class Filtering
    {
        public string Person { get; set; } = null;
        public string Category { get; set; } = null;
        public string DateFrom { get; set; } = null;
        public string DateTo { get; set; } = null;
        public int? CostFrom { get; set; } = null;
        public int? CostTo { get; set; } = null;
    }
}
