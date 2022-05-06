using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class StoreDbContext: DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) {
            // this.Database.EnsureCreated(); // Used if you're not using migrations.
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public DbSet<Product> Products => Set<Product>();
    }
}
