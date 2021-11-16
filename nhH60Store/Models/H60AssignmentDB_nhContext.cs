using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace nhH60Store.Models {
    public partial class H60AssignmentDB_nhContext : DbContext {

        public H60AssignmentDB_nhContext() {
        }

        public H60AssignmentDB_nhContext(DbContextOptions<H60AssignmentDB_nhContext> options)
            : base(options) {
            Database.EnsureCreated();
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server=cssql.cegep-heritage.qc.ca;Database=H60Assignment3DB_nh;User id=nhaile; Password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Product>(entity => {

                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BuyPrice).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.SellPrice).HasColumnType("numeric(8, 2)");

                entity.HasOne(d => d.ProdCat)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProdCatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity => {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProdCat)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            //modelBuilder.Entity<CartItem>(entity => {
            //    entity.HasKey(e => e.CartItemId);

            //    entity.Property(e => e.CartItemId).HasColumnName("CartItemId");

            //    entity.Property(e => e.Price).HasColumnType("numeric(8, 2)");

            //    entity.Property(e => e.CartId).IsRequired();

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.CartItems)
            //        .HasForeignKey(d => d.ProductId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CartItem_Product");

            //    entity.HasOne(d => d.Cart)
            //        .WithMany(p => p.CartItems)
            //        .HasForeignKey(d => d.CartId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CartItem_ShoppingCart");
            //});

            //modelBuilder.Entity<ShoppingCart>(entity => {
            //    entity.HasKey(e => e.CartId);

            //    entity.Property(e => e.CartId).HasColumnName("CartId");

            //    entity.Property(e => e.DateCreated).HasColumnType("date");
            //});

            //modelBuilder.Entity<OrderItem>(entity => {
            //    entity.HasKey(e => e.OrderItemId);

            //    entity.Property(e => e.OrderItemId).HasColumnName("OrderItemId");

            //    entity.Property(e => e.Price).HasColumnType("numeric(8, 2)");

            //    entity.HasOne(d => d.Order)
            //        .WithMany(p => p.OrderItems)
            //        .HasForeignKey(d => d.OrderId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_OrderItem_Order");

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.OrderItems)
            //        .HasForeignKey(d => d.ProductId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_OrderItem_Product");
            //});

            //modelBuilder.Entity<Order>(entity => {
            //    entity.HasKey(e => e.OrderId);

            //    entity.Property(e => e.OrderId).HasColumnName("OrderId");

            //    entity.Property(e => e.Total).HasColumnType("numeric(10, 2)");

            //    entity.Property(e => e.Total).HasColumnType("numeric(8, 2)");

            //    entity.Property(e => e.DateCreated).HasColumnType("date");

            //    entity.Property(e => e.DateFulfilled).HasColumnType("date");

            //    entity.Property(e => e.Total).HasColumnType("numeric(10, 2)");

            //    entity.Property(e => e.Taxes).HasColumnType("numeric(8, 2)");

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.Orders)
            //        .HasForeignKey(d => d.CustomerId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Order_Customer");
            //});

            //modelBuilder.Entity<Customer>(entity => {
            //    entity.HasKey(e => e.CustomerId);

            //    entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

            //    entity.Property(e => e.FirstName)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.LastName)
            //        .HasMaxLength(30)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(30)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(30)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PhoneNumber)
            //        .HasMaxLength(10)
            //        .IsUnicode(false)
            //        .IsFixedLength();

            //    entity.Property(e => e.Province)
            //        .HasMaxLength(2)
            //        .IsUnicode(false)
            //        .IsFixedLength()
            //        .HasDefaultValueSql("('QC')");

            //    entity.Property(e => e.CreditCard)
            //        .HasMaxLength(16)
            //        .IsUnicode(false);

            //});            

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Nahom", LastName = "Haile", Email = "nahomH@gmail.com", PhoneNumber = "8191111111", CreditCard = "4309827387307397", Province = "QC" },
                new Customer { CustomerId = 2, FirstName = "Serge", LastName = "Arsenault", Email = "sergeH@gmail.com", PhoneNumber = "8192222222", CreditCard = "4309827387307397", Province = "QC" },
                new Customer { CustomerId = 3, FirstName = "Emmanuelle", LastName = "Fontaine", Email = "emmanuelleF@gmail.com", PhoneNumber = "8193333333", CreditCard = "4309827387307397", Province = "QC" }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, DateCreated = new DateTime(2021, 9, 01), DateFulfilled = new DateTime(2021, 9, 03), Total = 540.00m, Taxes = 3.00m }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 2, Price = 20 },
                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 9, Quantity = 1, Price = 55 },
                new OrderItem { OrderItemId = 3, OrderId = 1, ProductId = 18, Quantity = 1, Price = 465 }
            );

            modelBuilder.Entity<ShoppingCart>().HasData(
                new ShoppingCart { CartId = 1, CustomerId = 2, DateCreated = new DateTime(2021, 9, 04) }
            );

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { CartItemId = 1, CartId = 1, ProductId = 24, Quantity = 1, Price = 180.00m },
                new CartItem { CartItemId = 2, CartId = 1, ProductId = 28, Quantity = 6, Price = 60.00m }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}