using Demo.Api.Models.Identity;
using Demo.Api.Models.Security;
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

        public DbSet<Permission> Permissions { get; set; }

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
            modelBuilder.Entity<Company>().HasData(new[] { new Company() { Id = 1, Name = "Company1" } });
            modelBuilder.Entity<Company>().HasData(new[] { new Company() { Id = 2, Name = "Company2" } });

            modelBuilder.Entity<Role>().HasData(new[] {
                new Role(){ Id = "84e7b6dc-7889-415d-8198-799f263bfb9d", ConcurrencyStamp="84e7b6dc-7889-415d-8198-799f263bfb9d", Name="User", NormalizedName= "USER" },
                new Role(){ Id = "a51fba13-a9c0-43e4-b9af-56448c4942fc", ConcurrencyStamp="a51fba13-a9c0-43e4-b9af-56448c4942fc", Name="Admin", NormalizedName="ADMIN"}
            });

            modelBuilder.Entity<Product>().HasData(new[] {
                new Product() {Id=1, name="product 1", production = DateTime.Now, CompanyId=1, ExpiresInDays=365 },
                new Product() {Id=2, name="product 2", production = DateTime.Now, CompanyId=1, ExpiresInDays=7 }
            });

            modelBuilder.Entity<Permission>().HasData(
                   new Permission() { Id = 1, Name = "Login" },
                   new Permission() { Id = 2, Name = "Users" },
                   new Permission() { Id = 3, Name = "Role" },
                   new Permission() { Id = 4, Name = "Permissions" },
                   new Permission() { Id = 5, Name = "AddProduct" },
                   new Permission() { Id = 6, Name = "EditProduct" });

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(
                new IdentityRoleClaim<string> { Id = 1, RoleId = "84e7b6dc-7889-415d-8198-799f263bfb9d", ClaimType = "permission", ClaimValue = "Login" },
                new IdentityRoleClaim<string> { Id = 2, RoleId = "a51fba13-a9c0-43e4-b9af-56448c4942fc", ClaimType = "permission", ClaimValue = "Users" },
                new IdentityRoleClaim<string> { Id = 3, RoleId = "a51fba13-a9c0-43e4-b9af-56448c4942fc", ClaimType = "permission", ClaimValue = "Permissions" },
                new IdentityRoleClaim<string> { Id = 4, RoleId = "a51fba13-a9c0-43e4-b9af-56448c4942fc", ClaimType = "permission", ClaimValue = "AddProduct" },
                new IdentityRoleClaim<string> { Id = 5, RoleId = "a51fba13-a9c0-43e4-b9af-56448c4942fc", ClaimType = "permission", ClaimValue = "EditProduct" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
