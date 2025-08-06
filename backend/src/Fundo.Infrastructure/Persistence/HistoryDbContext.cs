using Fundo.Domain.Entities;
using Fundo.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Persistence;

public class HistoryDbContext(DbContextOptions<HistoryDbContext> options) : DbContext(options)
{
    public DbSet<History> History => Set<History>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LoanConfiguration());
    }
}