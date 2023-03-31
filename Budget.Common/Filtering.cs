using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Common
{
    public class Filtering
    {
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? CostFrom { get; set; } = null;
        public int? CostTo { get; set; } = null;
    }
}
