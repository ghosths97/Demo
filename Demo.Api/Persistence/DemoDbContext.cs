using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Persistence
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed
            modelBuilder.Entity<Company>().HasData(new[] { new Company() { id=1, name = "Company1" } });
            
            modelBuilder.Entity<Product>().HasData(new[] { new Product() {id=1, name="product 1", production = DateTime.Now, CompanyId=1 },
            new Product() {id=2, name="product 2", production = DateTime.Now, CompanyId=1 }});
            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
