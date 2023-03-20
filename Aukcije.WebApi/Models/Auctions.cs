using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aukcije.WebApi.Models
{
    public class Auctions
    {
        public List<Oglas> List { get; set; }

        public Auctions()
        {
            List.Add(new Oglas()
            {
                Id = Guid.NewGuid(),
                ItemName = "Vikendica",
                Price = 50000,
                Seller = "Marko",
                EndTime = DateTime.Now.AddDays(60)
            });

            List.Add(new Oglas
            {
                Id = Guid.NewGuid(),
                ItemName = "Auto",
                Price = 15000,
                Seller = "Darko",
                EndTime = DateTime.Now.AddDays(30)
            });
        }
    }
}