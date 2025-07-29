using Fundo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Persistence;

public class LoanDbContext(DbContextOptions<LoanDbContext> options) : DbContext(options)
{
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var loan1Id = Guid.Parse("29a5e3d1-4b91-4be1-9181-cb99ea9de0a1");
        var loan2Id = Guid.Parse("3bd9b948-37ec-452a-b3d5-9cf8d90e42f3");

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Amount)
                .IsRequired();

            entity.Property(e => e.CurrentBalance)
                .IsRequired();

            entity.Property(e => e.ApplicantName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired();

            entity.HasData(
                new
                {
                    Id = loan1Id,
                    Amount = 1500m,
                    CurrentBalance = 500m,
                    ApplicantName = "Maria Silva",
                    Status = LoanStatus.Active
                },
                new
                {
                    Id = loan2Id,
                    Amount = 1000m,
                    CurrentBalance = 0m,
                    ApplicantName = "João Souza",
                    Status = LoanStatus.Paid
                }
            );
        });
    }
}