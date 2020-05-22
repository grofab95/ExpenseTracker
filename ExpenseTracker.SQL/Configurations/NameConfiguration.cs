using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.SQL.Configurations
{
    public class NameConfiguration : IEntityTypeConfiguration<Name>
    {
        public void Configure(EntityTypeBuilder<Name> builder)
        {
            builder.ToTable("Names");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
