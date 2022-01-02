using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ElektronikGigantenLibrary.Models
{
    [Table("OrderDelivery")]
    public partial class OrderDelivery
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("deliverystatus_id")]
        public int DeliverystatusId { get; set; }
        [Column("datedelivered", TypeName = "date")]
        public DateTime Datedelivered { get; set; }
        [Column("postal")]
        public int Postal { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("OrderDeliveries")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(DeliverystatusId))]
        [InverseProperty(nameof(DeliveryStatus.OrderDeliveries))]
        public virtual DeliveryStatus Deliverystatus { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(OrderSale.OrderDeliveries))]
        public virtual OrderSale Order { get; set; }
        [ForeignKey(nameof(Postal))]
        [InverseProperty(nameof(PostalCode.OrderDeliveries))]
        public virtual PostalCode PostalNavigation { get; set; }
    }
}
