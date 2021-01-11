using Demo.Api.Models.Identity;
using Demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Persistence
{
    public class DemoDbContext : IdentityDbContext<User>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }

        #region Identity

        public  DbSet<User> Users { get; set; }

        public  DbSet<Role> Roles { get; set; }

        #endregion

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.ToTable(name: "Users"); });
            modelBuilder.Entity<Role>(entity => { entity.ToTable(name: "Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });


            // seed
            modelBuilder.Entity<Company>().HasData(new[] { new Company() { id=1, name = "Company1" } });

            modelBuilder.Entity<Role>().HasData(new[] {
                new Role("User"),
                new Role("Admin")
            });

            modelBuilder.Entity<Product>().HasData(new[] { 
                new Product() {id=1, name="product 1", production = DateTime.Now, CompanyId=1, ExpiresInDays=365 },
                new Product() {id=2, name="product 2", production = DateTime.Now, CompanyId=1, ExpiresInDays=7 }
            });
            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
