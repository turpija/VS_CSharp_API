namespace Budget.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Income")]
    public partial class Income
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid IncomeCategoryId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public virtual IncomeCategory IncomeCategory { get; set; }

        public virtual Person Person { get; set; }
    }
}
