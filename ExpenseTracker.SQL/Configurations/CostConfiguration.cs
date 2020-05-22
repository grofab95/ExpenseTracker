using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.SQL.Configurations
{
    public class CostConfiguration : IEntityTypeConfiguration<Cost>
    {
        public void Configure(EntityTypeBuilder<Cost> builder)
        {
            builder.ToTable("Costs");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Costs)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Name)
                .WithMany(x => x.Costs)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Description)
                .WithMany(x => x.Costs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
