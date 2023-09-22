using Microsoft.EntityFrameworkCore;
using TestExchangeRates.Database.Entities;

namespace TestExchangeRates.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<ExchangeRatesTable> ExchangeRatesTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeRatesTable>().HasMany(x => x.Rates);
            modelBuilder.Entity<ExchangeRatesTable>().HasIndex(x => x.TableNumber).IsUnique();
            modelBuilder.Entity<Rate>().HasOne(x => x.Currency);
            modelBuilder.Entity<Currency>().HasIndex(x => x.Code).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Database.db");
        }
    }
}
