using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model.Common
{
    public interface IEarningDTO
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int PersonId { get; set; }
        DateTime Date { get; set; }
        double Amount { get; set; }
        IPersonDTO Person { get; set; }
    }
}
