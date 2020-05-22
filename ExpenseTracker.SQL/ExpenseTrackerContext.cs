using ExpenseTracker.Common;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.SQL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.SQL
{
    public class ExpenseTrackerContext : DbContext
    {
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Name> Names { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.Get().SqlConnection);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CostConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DescriptionConfiguration());
            modelBuilder.ApplyConfiguration(new NameConfiguration());
        }
    }
}
