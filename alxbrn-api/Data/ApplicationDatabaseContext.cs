using alxbrn_api.Models;
using System.Data.Entity;

namespace alxbrn_api.Data
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext() :
            base("DatabaseConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<HistoryAction> HistoryActions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AuthorizedApp> AuthorizedApps { get; set; }
    }
}