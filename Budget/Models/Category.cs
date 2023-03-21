using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Models
{
    public class Category
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}