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
            List = new List<Oglas>
            {
                new Oglas()
                {
                    Id = 1,
                    ItemName = "Vikendica",
                    Price = 50000,
                    Seller = "Marko",
                    EndTime = DateTime.Now.AddDays(60)
                },

                new Oglas
                {
                    Id = 2,
                    ItemName = "Auto",
                    Price = 15000,
                    Seller = "Darko",
                    EndTime = DateTime.Now.AddDays(30)
                },

                new Oglas
                {
                    Id = 3,
                    ItemName = "Telefon",
                    Price = 1500,
                    Seller = "Slavko",
                    EndTime = DateTime.Now.AddDays(10)
                }
            };
        }
        

    }
}