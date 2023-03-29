using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Common
{
    public class Filtering
    {
        public string Person { get; set; }
        public string Category { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int CostFrom { get; set; }
        public int CostTo { get; set; }
    }
}
