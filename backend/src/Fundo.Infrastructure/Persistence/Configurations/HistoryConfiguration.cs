using Fundo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundo.Infrastructure.Persistence.Configurations;

public class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.LoandId)
            .IsRequired();

        entity.Property(e => e.Description)
            .IsRequired();

        entity.HasData(GetSeedLoanHistories());
    }

    private static IEnumerable<object> GetSeedLoanHistories() => new List<object>
{
    // Maria Silva
    new History(Guid.Parse("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01"), "Loan created: Amount $1,500.00", DateTime.UtcNow.AddDays(-10)),
    new History(Guid.Parse("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01"), "Payment of $1,000.00 received", DateTime.UtcNow.AddDays(-5)),

    // João Souza
    new History(Guid.Parse("2b3c934a-78d2-4b56-a5d4-55a83954ae02"), "Loan created: Amount $1,000.00", DateTime.UtcNow.AddDays(-12)),
    new History(Guid.Parse("2b3c934a-78d2-4b56-a5d4-55a83954ae02"), "Loan fully paid", DateTime.UtcNow.AddDays(-3)),

    // Alice Johnson
    new History(Guid.Parse("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), "Loan created: Amount $5,000.00", DateTime.UtcNow.AddDays(-20)),
    new History(Guid.Parse("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), "Interest charged: $50.00", DateTime.UtcNow.AddDays(-10)),

    // Michael Smith
    new History(Guid.Parse("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04"), "Loan created: Amount $7,500.00", DateTime.UtcNow.AddDays(-15)),
    new History(Guid.Parse("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04"), "Payment of $5,000.00 received", DateTime.UtcNow.AddDays(-2)),

    // Laura Martinez
    new History(Guid.Parse("5c334e60-81a2-42a0-89e2-0f80c4b1e405"), "Loan created: Amount $2,000.00", DateTime.UtcNow.AddDays(-13)),
    new History(Guid.Parse("5c334e60-81a2-42a0-89e2-0f80c4b1e405"), "Loan fully paid", DateTime.UtcNow.AddDays(-1)),

    // Daniel Kim
    new History(Guid.Parse("6a223421-38a9-43ff-b7b2-dab9b5e8e206"), "Loan created: Amount $12,000.00", DateTime.UtcNow.AddDays(-30)),
    new History(Guid.Parse("6a223421-38a9-43ff-b7b2-dab9b5e8e206"), "Interest charged: $100.00", DateTime.UtcNow.AddDays(-15)),

    // Emma Brown
    new History(Guid.Parse("7e21f316-9475-4a3e-9c6d-61c5f1a3b207"), "Loan created: Amount $3,000.00", DateTime.UtcNow.AddDays(-6)),
    new History(Guid.Parse("7e21f316-9475-4a3e-9c6d-61c5f1a3b207"), "Payment of $1,500.00 received", DateTime.UtcNow.AddDays(-2)),

    // Lucas Williams
    new History(Guid.Parse("8f42e9a1-3cdd-470b-8779-319ae346f508"), "Loan created: Amount $900.00", DateTime.UtcNow.AddDays(-8)),
    new History(Guid.Parse("8f42e9a1-3cdd-470b-8779-319ae346f508"), "Loan fully paid", DateTime.UtcNow.AddDays(-2)),

    // Olivia Davis
    new History(Guid.Parse("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), "Loan created: Amount $6,500.00", DateTime.UtcNow.AddDays(-14)),
    new History(Guid.Parse("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), "Payment of $3,500.00 received", DateTime.UtcNow.AddDays(-5)),

    // Ethan Wilson
    new History(Guid.Parse("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10"), "Loan created: Amount $4,000.00", DateTime.UtcNow.AddDays(-10)),
    new History(Guid.Parse("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10"), "Loan fully paid", DateTime.UtcNow.AddDays(-1)),

    // Sophia Moore
    new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Loan created: Amount $10,000.00", DateTime.UtcNow.AddDays(-21)),
    new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Interest charged: $120.00", DateTime.UtcNow.AddDays(-7)),

    // Gabriel Taylor
    new History(Guid.Parse("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12"), "Loan created: Amount $2,500.00", DateTime.UtcNow.AddDays(-11)),
    new History(Guid.Parse("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12"), "Payment of $2,000.00 received", DateTime.UtcNow.AddDays(-3)),
};
}