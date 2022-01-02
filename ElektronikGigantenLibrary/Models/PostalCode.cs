using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ElektronikGigantenLibrary.Models
{
    [Table("PostalCode")]
    public partial class PostalCode
    {
        public PostalCode()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            OrderDeliveries = new HashSet<OrderDelivery>();
            Stores = new HashSet<Store>();
        }

        [Key]
        [Column("postalcodex")]
        public int Postalcodex { get; set; }
        [Required]
        [Column("city")]
        [StringLength(150)]
        public string City { get; set; }

        [InverseProperty(nameof(Customer.PostalNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty(nameof(Employee.PostalNavigation))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(OrderDelivery.PostalNavigation))]
        public virtual ICollection<OrderDelivery> OrderDeliveries { get; set; }
        [InverseProperty(nameof(Store.PostalNavigation))]
        public virtual ICollection<Store> Stores { get; set; }
    }
}
