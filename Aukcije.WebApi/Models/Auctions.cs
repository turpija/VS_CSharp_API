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
                    ItemName = "Renault",
                    Price = 5000,
                    Seller = "Marko",
                    EndTime = DateTime.Now.AddDays(60)
                },

                new Oglas
                {
                    Id = 2,
                    ItemName = "Bmw",
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
                },

                new Oglas
                {
                    Id = 5,
                    ItemName = "Mercedes",
                    Price = 18000,
                    Seller = "Vjekoslav",
                    EndTime = DateTime.Now.AddDays(30)
                },
                new Oglas
                {
                    Id = 6,
                    ItemName = "Porsche",
                    Price = 39000,
                    Seller = "Dalibor",
                    EndTime = DateTime.Now.AddDays(70)
                }
            };
        }
        

    }
}