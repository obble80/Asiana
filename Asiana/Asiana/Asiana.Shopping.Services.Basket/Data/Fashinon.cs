using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Shipping;
using Asiana.Shopping.Services.Promotions;
using Asiana.Shopping.Services.Payments;
using Asiana.Shopping.Services.Products;
using System.ComponentModel.DataAnnotations;
using Asiana.Shopping.Services.Customers;
namespace Asiana.Shopping.Services.Data
{
    public class Fashinon : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public void Update<T>(T entity) where T : class
        {
            this.Set<T>().Attach(entity);
            this.Entry(entity).State = System.Data.EntityState.Modified;
            this.SaveChanges();
        }

 

        private void ConfigureOrder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(x => x.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Order>()
               .HasOptional(a => a.BillingAddress)
               .WithMany()
               .Map(m => m.MapKey("BillingAddressID"));

            modelBuilder.Entity<Order>()
                .HasRequired(a => a.ShippingAddress)
                .WithOptional()
                .Map(m => m.MapKey("ShippingAddressID"));

            modelBuilder.Entity<Order>()
                .HasRequired(a => a.Payment)
                .WithOptional()
                .Map(m => m.MapKey("PaymentID"));

            modelBuilder.Entity<Order>()
              .HasRequired(a => a.ShippingMethod)
              .WithMany()
              .Map(m => m.MapKey("ShippingMethodID"));

            modelBuilder.Entity<Order>()
           .HasRequired(a => a.Customer);
            //.Map(m => m.MapKey("ShippingMethodID"));

            modelBuilder.Entity<Order>()
                .HasMany(x => x.Items);
        }
        private void ConfigureProduct(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
             .HasKey(x => x.LineItemID);
            modelBuilder.Entity<Product>()
                .Property(x => x.LineItemID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>()
                .Ignore(x => x.ApplicablePromotions);
            modelBuilder.Entity<Product>()
                .Ignore(x => x.AppliedPromotions);
            modelBuilder.Entity<Product>()
                .Ignore(x => x.Image);
        }
        private void ConfigureCustomer(DbModelBuilder modelBuilder)
        {

            // Configure Key
            modelBuilder.Entity<Customer>()
                         .HasKey(x => x.CustomerID);
            modelBuilder.Entity<Customer>()
                .Property(x => x.CustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Customer>()
                .Ignore(x => x.Addresses);
            modelBuilder.Entity<Customer>()
                .Ignore(x => x.ContactAddress);
                

            //TODO: Configure relations
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>()
                .Property(x => x.AddressID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Address>()
                .Ignore(x => x.CustomerID);
            modelBuilder.Entity<Address>()
                .Ignore(x => x.CountryCode);

            modelBuilder.Entity<ShippingMethod>()
                .Property(x => x.ShippingMethodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ConfigureCustomer(modelBuilder);
            ConfigureOrder(modelBuilder);
            ConfigureProduct(modelBuilder);
        }
    }
}
