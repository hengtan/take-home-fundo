using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fundo.Infrastructure.Persistence;

public class LoanDbContextFactory : IDesignTimeDbContextFactory<LoanDbContext>
{
    public LoanDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LoanDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=LoanDb;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True");

        return new LoanDbContext(optionsBuilder.Options);
    }
}