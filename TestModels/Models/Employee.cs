using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestModels.Models
{
    [Table("Employee")]
    public partial class Employee
    {
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
        [Column("phone")]
        public int Phone { get; set; }
        [Required]
        [Column("email")]
        [StringLength(150)]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [Column("address")]
        [StringLength(100)]
        public string Address { get; set; }
        [Column("postal")]
        public int Postal { get; set; }
        [Column("department_id")]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Employees")]
        public virtual Department Department { get; set; }
        [ForeignKey(nameof(Postal))]
        [InverseProperty(nameof(PostalCode.Employees))]
        public virtual PostalCode PostalNavigation { get; set; }
    }
}
