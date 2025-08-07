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
        // Maria Silva (3)
        new History(Guid.Parse("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01"), "Loan created: Amount $1,500.00",
                new DateTime(2024, 08, 01, 10, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("10000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01"), "Payment of $800.00 received",
                new DateTime(2024, 08, 03, 9, 30, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("10000000-0000-0000-0000-000000000002") },
        new History(Guid.Parse("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01"), "Payment of $200.00 received",
                new DateTime(2024, 08, 06, 14, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("10000000-0000-0000-0000-000000000003") },

        // João Souza (2)
        new History(Guid.Parse("2b3c934a-78d2-4b56-a5d4-55a83954ae02"), "Loan created: Amount $1,000.00",
                new DateTime(2024, 08, 01, 11, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("20000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("2b3c934a-78d2-4b56-a5d4-55a83954ae02"), "Loan fully paid",
                new DateTime(2024, 08, 05, 13, 30, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("20000000-0000-0000-0000-000000000002") },

        // Alice Johnson (5)
        new History(Guid.Parse("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), "Loan created: Amount $5,000.00",
                new DateTime(2024, 08, 01, 12, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("30000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), "Interest charged: $50.00",
                new DateTime(2024, 08, 02, 15, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("30000000-0000-0000-0000-000000000002") },
        new History(Guid.Parse("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), "Payment of $1,000.00 received",
                new DateTime(2024, 08, 04, 11, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("30000000-0000-0000-0000-000000000003") },
        new History(Guid.Parse("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), "Payment of $1,500.00 received",
                new DateTime(2024, 08, 06, 9, 30, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("30000000-0000-0000-0000-000000000004") },
        new History(Guid.Parse("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), "Late fee charged: $10.00",
                new DateTime(2024, 08, 08, 18, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("30000000-0000-0000-0000-000000000005") },

        // Michael Smith (2)
        new History(Guid.Parse("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04"), "Loan created: Amount $7,500.00",
                new DateTime(2024, 08, 03, 13, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("40000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04"), "Payment of $5,000.00 received",
                new DateTime(2024, 08, 07, 16, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("40000000-0000-0000-0000-000000000002") },

        // Laura Martinez (3)
        new History(Guid.Parse("5c334e60-81a2-42a0-89e2-0f80c4b1e405"), "Loan created: Amount $2,000.00",
                new DateTime(2024, 08, 02, 10, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("50000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("5c334e60-81a2-42a0-89e2-0f80c4b1e405"), "Payment of $1,000.00 received",
                new DateTime(2024, 08, 03, 13, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("50000000-0000-0000-0000-000000000002") },
        new History(Guid.Parse("5c334e60-81a2-42a0-89e2-0f80c4b1e405"), "Loan fully paid",
                new DateTime(2024, 08, 04, 15, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("50000000-0000-0000-0000-000000000003") },

        // Daniel Kim (4)
        new History(Guid.Parse("6a223421-38a9-43ff-b7b2-dab9b5e8e206"), "Loan created: Amount $12,000.00",
                new DateTime(2024, 08, 01, 14, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("60000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("6a223421-38a9-43ff-b7b2-dab9b5e8e206"), "Interest charged: $100.00",
                new DateTime(2024, 08, 03, 12, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("60000000-0000-0000-0000-000000000002") },
        new History(Guid.Parse("6a223421-38a9-43ff-b7b2-dab9b5e8e206"), "Payment of $4,000.00 received",
                new DateTime(2024, 08, 04, 17, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("60000000-0000-0000-0000-000000000003") },
        new History(Guid.Parse("6a223421-38a9-43ff-b7b2-dab9b5e8e206"), "Partial payment received",
                new DateTime(2024, 08, 06, 16, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("60000000-0000-0000-0000-000000000004") },

        // Emma Brown (2)
        new History(Guid.Parse("7e21f316-9475-4a3e-9c6d-61c5f1a3b207"), "Loan created: Amount $3,000.00",
                new DateTime(2024, 08, 02, 11, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("70000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("7e21f316-9475-4a3e-9c6d-61c5f1a3b207"), "Payment of $1,500.00 received",
                new DateTime(2024, 08, 05, 12, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("70000000-0000-0000-0000-000000000002") },

        // Lucas Williams (2)
        new History(Guid.Parse("8f42e9a1-3cdd-470b-8779-319ae346f508"), "Loan created: Amount $900.00",
                new DateTime(2024, 08, 02, 10, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("80000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("8f42e9a1-3cdd-470b-8779-319ae346f508"), "Loan fully paid",
                new DateTime(2024, 08, 04, 14, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("80000000-0000-0000-0000-000000000002") },

        // Olivia Davis (5)
        new History(Guid.Parse("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), "Loan created: Amount $6,500.00",
                new DateTime(2024, 08, 01, 15, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("90000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), "Interest charged: $70.00",
                new DateTime(2024, 08, 03, 10, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("90000000-0000-0000-0000-000000000002") },
        new History(Guid.Parse("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), "Payment of $2,000.00 received",
                new DateTime(2024, 08, 04, 15, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("90000000-0000-0000-0000-000000000003") },
        new History(Guid.Parse("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), "Payment of $1,500.00 received",
                new DateTime(2024, 08, 06, 9, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("90000000-0000-0000-0000-000000000004") },
        new History(Guid.Parse("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), "Partial payment received",
                new DateTime(2024, 08, 09, 18, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("90000000-0000-0000-0000-000000000005") },

        // Ethan Wilson (3)
        new History(Guid.Parse("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10"), "Loan created: Amount $4,000.00",
                new DateTime(2024, 08, 01, 14, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("10000001-0000-0000-0000-000000000001") },
        new History(Guid.Parse("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10"), "Payment of $2,000.00 received",
                new DateTime(2024, 08, 05, 12, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("10000001-0000-0000-0000-000000000002") },
        new History(Guid.Parse("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10"), "Loan fully paid",
                new DateTime(2024, 08, 07, 17, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("10000001-0000-0000-0000-000000000003") },

        // Sophia Moore (6)
        new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Loan created: Amount $10,000.00",
                new DateTime(2024, 08, 01, 10, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("11000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Interest charged: $120.00",
                new DateTime(2024, 08, 03, 12, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("11000000-0000-0000-0000-000000000002") },
        new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Payment of $2,500.00 received",
                new DateTime(2024, 08, 04, 14, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("11000000-0000-0000-0000-000000000003") },
        new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Late fee charged: $40.00",
                new DateTime(2024, 08, 05, 13, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("11000000-0000-0000-0000-000000000004") },
        new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Partial payment received",
                new DateTime(2024, 08, 07, 9, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("11000000-0000-0000-0000-000000000005") },
        new History(Guid.Parse("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), "Loan extension requested",
                new DateTime(2024, 08, 09, 15, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("11000000-0000-0000-0000-000000000006") },

        // Gabriel Taylor (2)
        new History(Guid.Parse("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12"), "Loan created: Amount $2,500.00",
                new DateTime(2024, 08, 03, 12, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("12000000-0000-0000-0000-000000000001") },
        new History(Guid.Parse("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12"), "Payment of $2,000.00 received",
                new DateTime(2024, 08, 06, 14, 0, 0, DateTimeKind.Utc))
            { Id = Guid.Parse("12000000-0000-0000-0000-000000000002") },
    };
}