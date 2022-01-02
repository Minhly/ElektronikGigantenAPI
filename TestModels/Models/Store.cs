using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    [Table("Store")]
    public partial class Store
    {
        public Store()
        {
            OrderSales = new HashSet<OrderSale>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [Column("address")]
        [StringLength(250)]
        public string Address { get; set; }
        [Column("postal")]
        public int Postal { get; set; }

        [ForeignKey(nameof(Postal))]
        [InverseProperty(nameof(PostalCode.Stores))]
        public virtual PostalCode PostalNavigation { get; set; }
        [InverseProperty(nameof(OrderSale.Store))]
        public virtual ICollection<OrderSale> OrderSales { get; set; }
    }
}
