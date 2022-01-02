using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ElektronikGigantenLibrary.Models
{
    [Table("OrderLine")]
    public partial class OrderLine
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(OrderSale.OrderLines))]
        public virtual OrderSale Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderLines")]
        public virtual Product Product { get; set; }
    }
}
