using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.ModelBuilderExtensions;

namespace WebWinkelIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //Store dbsets
        public DbSet<WeekOpeningTimes> WeekOpeningTimes { get; set; }
        public DbSet<Store> Stores { get; set; }

        //Products dbsets
        public DbSet<Product> Products { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<ProductStockChange> ProductStockChanges { get; set; }
        public DbSet<LoadStockChange> LoadStockChanges { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        //User dbsets
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //Order dbsets
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SeedDataAndConfigureFluentAPI();
        }
    }
}
