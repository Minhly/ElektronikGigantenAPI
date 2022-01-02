using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    [Table("CreditCard")]
    public partial class CreditCard
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("card_number")]
        public int CardNumber { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CreditCards")]
        public virtual Customer Customer { get; set; }
    }
}
