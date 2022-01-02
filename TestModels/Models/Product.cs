using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
            ProductReviews = new HashSet<ProductReview>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Required]
        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(ProductCategory.Products))]
        public virtual ProductCategory Category { get; set; }
        [InverseProperty(nameof(OrderLine.Product))]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        [InverseProperty(nameof(ProductReview.Product))]
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
