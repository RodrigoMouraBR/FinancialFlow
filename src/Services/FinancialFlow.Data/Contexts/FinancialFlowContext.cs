using FinancialFlow.Core.Interfaces;
using FinancialFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialFlow.Data.Contexts
{
    public class FinancialFlowContext : DbContext, IUnitOfWork
    {
        public FinancialFlowContext(DbContextOptions<FinancialFlowContext> options) : base(options) { }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialFlowContext).Assembly);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                    entry.Property("CreatedAt").IsModified = false;

            }
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("UpdatedAt").IsModified = false;

                if (entry.State == EntityState.Modified)
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }

            var success = await base.SaveChangesAsync() > 0;
            return success;
        }
    }
}
