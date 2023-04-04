using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model
{
    public class ExpenseReturnDTO
    {
        public List<ExpenseDTO> Expenses { get; set; }
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string ResultMsg { get; set; }
    }
}
