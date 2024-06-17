using Carrefas.FinancialFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrefas.FinancialFlow.Data.Mapping
{
    public class DailyConsolidatedMapping : IEntityTypeConfiguration<DailyConsolidated>
    {
        public void Configure(EntityTypeBuilder<DailyConsolidated> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreatedAt)
               .IsRequired()
               .HasColumnType("timestamp with time zone");

            builder.Property(c => c.UpdatedAt)
                  .HasColumnType("timestamp with time zone");

            builder.ToTable("DailyConsolidated");
        }
    }
}
