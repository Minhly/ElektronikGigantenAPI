using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    [Table("OrderSale")]
    public partial class OrderSale
    {
        public OrderSale()
        {
            OrderDeliveries = new HashSet<OrderDelivery>();
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("orderdate", TypeName = "date")]
        public DateTime Orderdate { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("orderstatus_id")]
        public int OrderstatusId { get; set; }
        [Column("store_id")]
        public int StoreId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("OrderSales")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(OrderstatusId))]
        [InverseProperty(nameof(OrderStatus.OrderSales))]
        public virtual OrderStatus Orderstatus { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("OrderSales")]
        public virtual Store Store { get; set; }
        [InverseProperty(nameof(OrderDelivery.Order))]
        public virtual ICollection<OrderDelivery> OrderDeliveries { get; set; }
        [InverseProperty(nameof(OrderLine.Order))]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
