using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Model
{
    public class DummyList
    {
        public List<Category> Kategorije { get; set; }
        public List<Person> Persons { get; set; }
        public List<Expense> Expenses { get; set; }

        public DummyList()
        {
            Person ivan = new Person { Id = 1, Name = "Ivan" };
            Person katarina = new Person { Id = 2, Name = "Katarina" };

            Category hrana = new Category { Id = 1, Name = "Hrana" };
            Category kuca = new Category { Id = 2, Name = "Kuća" };
            Category potrebno = new Category { Id = 3, Name = "Potrebno" };
            Category kredit = new Category { Id = 4, Name = "Kredit" };
            Category luksuz = new Category { Id = 5, Name = "Luksuz" };

            Kategorije = new List<Category> { hrana, kuca, potrebno, kredit, luksuz };

            Persons = new List<Person> { ivan, katarina };

            Expenses = new List<Expense>
            {
                //new Expense() {Id=1, Name="Kaufland",PersonId = 1, CategoryId=1,Date=new DateTime(2023,2,3), Cost=45.30, Person=ivan,Category=hrana},
                //new Expense() {Id=2, Name="Konzum",PersonId = 2, CategoryId=1,Date=new DateTime(2023,02,17), Cost=17.50, Person=katarina,Category=hrana},
                //new Expense() {Id=3, Name="Kredit za kuću",PersonId = 1, CategoryId=4,Date=new DateTime(2023,02,20), Cost=170.00, Person=ivan,Category=kredit},
                //new Expense() {Id=4, Name="Rođendan",PersonId = 2, CategoryId=5,Date=new DateTime(2023,04,10), Cost=75.00, Person=katarina,Category=luksuz},

            };

        }
    }
}