using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Models
{
    public class Item
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public int PersonId { get; set; }
        [Required] public int CategoryId { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public double Cost { get; set; }
        public Person Person { get; set; }
        public Category Category { get; set; }
    }
}