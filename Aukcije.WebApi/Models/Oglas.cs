using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aukcije.WebApi.Models
{
    public class Oglas
    {
        public int Id { get; set; }
        [Required] public string ItemName { get; set; }
        [Required] public string Seller { get; set; }
        [Required] public double Price { get; set; }
        public DateTime EndTime { get; set; }

    }
}