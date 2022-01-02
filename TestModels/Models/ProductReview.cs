using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    [Table("ProductReview")]
    public partial class ProductReview
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("review", TypeName = "text")]
        public string Review { get; set; }
        [Column("rating")]
        public int Rating { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductReviews")]
        public virtual Product Product { get; set; }
    }
}
