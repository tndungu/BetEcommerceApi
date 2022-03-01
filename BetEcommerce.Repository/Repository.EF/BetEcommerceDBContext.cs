using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Repository.EF
{
    public class BetEcommerceDBContext : DbContext
    {
        public IConfiguration _config;

        public BetEcommerceDBContext(DbContextOptions<BetEcommerceDBContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetConnectionString("BetEcommerceContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.Cart)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.CartId);
        }
   
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItem { get; set;}
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartsItem { get; set; }

    }
}
