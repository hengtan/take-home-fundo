using Fundo.Domain.Entities;
using Fundo.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<History> Histories => Set<History>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LoanConfiguration());
        modelBuilder.ApplyConfiguration(new HistoryConfiguration());
    }
}