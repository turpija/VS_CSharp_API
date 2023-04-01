using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Budget.DAL
{
    public partial class BudgetV2Context : DbContext
    {
        public BudgetV2Context()
            : base("name=BudgetV2Context")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<IncomeCategory> IncomeCategory { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        //public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Cost)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Income>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Income>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Income>()
                .Property(e => e.Amount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<IncomeCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeCategory>()
                .HasMany(e => e.Incomes)
                .WithRequired(e => e.IncomeCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Incomes)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
