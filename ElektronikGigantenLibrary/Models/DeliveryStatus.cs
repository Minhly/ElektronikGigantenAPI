using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ElektronikGigantenLibrary.Models
{
    [Table("DeliveryStatus")]
    public partial class DeliveryStatus
    {
        public DeliveryStatus()
        {
            OrderDeliveries = new HashSet<OrderDelivery>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("deliverystatus")]
        [StringLength(50)]
        public string Deliverystatus1 { get; set; }

        [InverseProperty(nameof(OrderDelivery.Deliverystatus))]
        public virtual ICollection<OrderDelivery> OrderDeliveries { get; set; }
    }
}
