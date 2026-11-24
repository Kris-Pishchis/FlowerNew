using FlowerShopSharedModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FlowerShopBackground.Data
{
    public class AppDataContext: DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Datalogs> DatalogsUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Korzina> Korzinas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User_orders> UserOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Street>().ToTable("streets");
            modelBuilder.Entity<Datalogs>().ToTable("datalogs");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Korzina>().ToTable("korzina");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<User_orders>().ToTable("user_orders");
            

        }
    }
}
