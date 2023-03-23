using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Model
{
    public class Earning
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public int PersonId { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public double Amount { get; set; }
        public Person Person { get; set; }
    }
}