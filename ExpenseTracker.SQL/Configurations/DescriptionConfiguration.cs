using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.SQL.Configurations
{
    public class DescriptionConfiguration : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder.ToTable("Descriptions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
