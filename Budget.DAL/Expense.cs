namespace Budget.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Expense")]
    public partial class Expense
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal Cost { get; set; }

        public virtual Category Category { get; set; }

        public virtual Person Person { get; set; }
    }
}
