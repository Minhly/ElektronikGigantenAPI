using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    [Table("OrderStatus")]
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            OrderSales = new HashSet<OrderSale>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("orderstatus")]
        [StringLength(50)]
        public string Orderstatus1 { get; set; }

        [InverseProperty(nameof(OrderSale.Orderstatus))]
        public virtual ICollection<OrderSale> OrderSales { get; set; }
    }
}
