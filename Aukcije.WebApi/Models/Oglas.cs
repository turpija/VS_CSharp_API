using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aukcije.WebApi.Models
{
    public class Oglas
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Seller { get; set; }
        public double Price { get; set; }
        public DateTime EndTime { get; set; }

    }
}