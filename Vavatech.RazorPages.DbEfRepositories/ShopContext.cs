using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.RazorPages.FakeRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.DbEfRepositories
{
    // Install-Package Microsoft.EntityFrameworkCore.SqlServer
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .OwnsOne(p => p.InvoiceAddress);

            modelBuilder.Entity<Customer>()
                .OwnsOne(p => p.ShippedAddress);

            modelBuilder.Entity<Customer>()
                .Property(p => p.Id).HasColumnName("CustomerId");

            modelBuilder.Entity<Customer>()
                .Property(p => p.Email)
                .IsUnicode(false).HasMaxLength(200);

            modelBuilder.Entity<Customer>()
                .HasIndex(p => p.Email);

            modelBuilder.Entity<Customer>()
                .Property(p=>p.FullName)
                .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");

            FakeCustomerGroupRepository fakeCustomerGroupRepository = new FakeCustomerGroupRepository();

            modelBuilder.Entity<CustomerGroup>()
                .HasData(fakeCustomerGroupRepository.Get());

            base.OnModelCreating(modelBuilder);
        }
    }
}
