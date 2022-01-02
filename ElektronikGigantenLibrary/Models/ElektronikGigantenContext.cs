using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ElektronikGigantenLibrary.Models
{
    public partial class ElektronikGigantenContext : DbContext
    {
        public ElektronikGigantenContext()
        {
        }

        public ElektronikGigantenContext(DbContextOptions<ElektronikGigantenContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<OrderDelivery> OrderDeliveries { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<OrderSale> OrderSales { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<PostalCode> PostalCodes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Danish_Norwegian_CI_AS");

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CreditCards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditCard_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasOne(d => d.PostalNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Postal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_PostalCode");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength(true);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.PostalNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Postal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_PostalCode");
            });

            modelBuilder.Entity<OrderDelivery>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderDeliveries)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDelivery_Customer");

                entity.HasOne(d => d.Deliverystatus)
                    .WithMany(p => p.OrderDeliveries)
                    .HasForeignKey(d => d.DeliverystatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDelivery_DeliveryStatus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDeliveries)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDelivery_OrderSale");

                entity.HasOne(d => d.PostalNavigation)
                    .WithMany(p => p.OrderDeliveries)
                    .HasForeignKey(d => d.Postal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDelivery_PostalCode");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderLine_OrderSale");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderLine_Products");
            });

            modelBuilder.Entity<OrderSale>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderSales)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSale_Customer");

                entity.HasOne(d => d.Orderstatus)
                    .WithMany(p => p.OrderSales)
                    .HasForeignKey(d => d.OrderstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSale_OrderStatus");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.OrderSales)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSale_Store");
            });

            modelBuilder.Entity<PostalCode>(entity =>
            {
                entity.Property(e => e.Postalcodex).ValueGeneratedNever();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductCategory");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductReview_Products");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasOne(d => d.PostalNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.Postal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_PostalCode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
