using FinancialFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialFlow.Data.Mapping
{
    public class FinancialTransactionMapping : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreatedAt)
               .IsRequired()
               .HasColumnType("timestamp with time zone");

            builder.Property(c => c.UpdatedAt)
                  .HasColumnType("timestamp with time zone");

            builder.ToTable("FinancialTransaction");
        }
    }
}
