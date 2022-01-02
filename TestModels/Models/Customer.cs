using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            CreditCards = new HashSet<CreditCard>();
            OrderDeliveries = new HashSet<OrderDelivery>();
            OrderSales = new HashSet<OrderSale>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("firstname")]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [Column("lastname")]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [Column("email")]
        [StringLength(150)]
        public string Email { get; set; }
        [Column("phone")]
        public int Phone { get; set; }
        [Required]
        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [Column("addressline")]
        [StringLength(150)]
        public string Addressline { get; set; }
        [Column("postal")]
        public int Postal { get; set; }

        [ForeignKey(nameof(Postal))]
        [InverseProperty(nameof(PostalCode.Customers))]
        public virtual PostalCode PostalNavigation { get; set; }
        [InverseProperty(nameof(CreditCard.Customer))]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty(nameof(OrderDelivery.Customer))]
        public virtual ICollection<OrderDelivery> OrderDeliveries { get; set; }
        [InverseProperty(nameof(OrderSale.Customer))]
        public virtual ICollection<OrderSale> OrderSales { get; set; }
    }
}
