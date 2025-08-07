using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fundo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Histories",
                columns: new[] { "Id", "Created", "Description", "LoandId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $1,500.00", new Guid("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01") },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 3, 9, 30, 0, 0, DateTimeKind.Utc), "Payment of $800.00 received", new Guid("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01") },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2024, 8, 6, 14, 0, 0, 0, DateTimeKind.Utc), "Payment of $200.00 received", new Guid("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01") },
                    { new Guid("10000001-0000-0000-0000-000000000001"), new DateTime(2024, 8, 1, 14, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $4,000.00", new Guid("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10") },
                    { new Guid("10000001-0000-0000-0000-000000000002"), new DateTime(2024, 8, 5, 12, 0, 0, 0, DateTimeKind.Utc), "Payment of $2,000.00 received", new Guid("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10") },
                    { new Guid("10000001-0000-0000-0000-000000000003"), new DateTime(2024, 8, 7, 17, 0, 0, 0, DateTimeKind.Utc), "Loan fully paid", new Guid("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10") },
                    { new Guid("11000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $10,000.00", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("11000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Interest charged: $120.00", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("11000000-0000-0000-0000-000000000003"), new DateTime(2024, 8, 4, 14, 0, 0, 0, DateTimeKind.Utc), "Payment of $2,500.00 received", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("11000000-0000-0000-0000-000000000004"), new DateTime(2024, 8, 5, 13, 0, 0, 0, DateTimeKind.Utc), "Late fee charged: $40.00", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("11000000-0000-0000-0000-000000000005"), new DateTime(2024, 8, 7, 9, 0, 0, 0, DateTimeKind.Utc), "Partial payment received", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("11000000-0000-0000-0000-000000000006"), new DateTime(2024, 8, 9, 15, 0, 0, 0, DateTimeKind.Utc), "Loan extension requested", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("12000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $2,500.00", new Guid("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12") },
                    { new Guid("12000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 6, 14, 0, 0, 0, DateTimeKind.Utc), "Payment of $2,000.00 received", new Guid("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12") },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 1, 11, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $1,000.00", new Guid("2b3c934a-78d2-4b56-a5d4-55a83954ae02") },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 5, 13, 30, 0, 0, DateTimeKind.Utc), "Loan fully paid", new Guid("2b3c934a-78d2-4b56-a5d4-55a83954ae02") },
                    { new Guid("30000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $5,000.00", new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03") },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 2, 15, 0, 0, 0, DateTimeKind.Utc), "Interest charged: $50.00", new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03") },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new DateTime(2024, 8, 4, 11, 0, 0, 0, DateTimeKind.Utc), "Payment of $1,000.00 received", new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03") },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2024, 8, 6, 9, 30, 0, 0, DateTimeKind.Utc), "Payment of $1,500.00 received", new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03") },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new DateTime(2024, 8, 8, 18, 0, 0, 0, DateTimeKind.Utc), "Late fee charged: $10.00", new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03") },
                    { new Guid("40000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 3, 13, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $7,500.00", new Guid("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04") },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 7, 16, 0, 0, 0, DateTimeKind.Utc), "Payment of $5,000.00 received", new Guid("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04") },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $2,000.00", new Guid("5c334e60-81a2-42a0-89e2-0f80c4b1e405") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 3, 13, 0, 0, 0, DateTimeKind.Utc), "Payment of $1,000.00 received", new Guid("5c334e60-81a2-42a0-89e2-0f80c4b1e405") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new DateTime(2024, 8, 4, 15, 0, 0, 0, DateTimeKind.Utc), "Loan fully paid", new Guid("5c334e60-81a2-42a0-89e2-0f80c4b1e405") },
                    { new Guid("60000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 1, 14, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $12,000.00", new Guid("6a223421-38a9-43ff-b7b2-dab9b5e8e206") },
                    { new Guid("60000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Interest charged: $100.00", new Guid("6a223421-38a9-43ff-b7b2-dab9b5e8e206") },
                    { new Guid("60000000-0000-0000-0000-000000000003"), new DateTime(2024, 8, 4, 17, 0, 0, 0, DateTimeKind.Utc), "Payment of $4,000.00 received", new Guid("6a223421-38a9-43ff-b7b2-dab9b5e8e206") },
                    { new Guid("60000000-0000-0000-0000-000000000004"), new DateTime(2024, 8, 6, 16, 0, 0, 0, DateTimeKind.Utc), "Partial payment received", new Guid("6a223421-38a9-43ff-b7b2-dab9b5e8e206") },
                    { new Guid("70000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 2, 11, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $3,000.00", new Guid("7e21f316-9475-4a3e-9c6d-61c5f1a3b207") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 5, 12, 0, 0, 0, DateTimeKind.Utc), "Payment of $1,500.00 received", new Guid("7e21f316-9475-4a3e-9c6d-61c5f1a3b207") },
                    { new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $900.00", new Guid("8f42e9a1-3cdd-470b-8779-319ae346f508") },
                    { new Guid("80000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 4, 14, 0, 0, 0, DateTimeKind.Utc), "Loan fully paid", new Guid("8f42e9a1-3cdd-470b-8779-319ae346f508") },
                    { new Guid("90000000-0000-0000-0000-000000000001"), new DateTime(2024, 8, 1, 15, 0, 0, 0, DateTimeKind.Utc), "Loan created: Amount $6,500.00", new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909") },
                    { new Guid("90000000-0000-0000-0000-000000000002"), new DateTime(2024, 8, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Interest charged: $70.00", new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909") },
                    { new Guid("90000000-0000-0000-0000-000000000003"), new DateTime(2024, 8, 4, 15, 0, 0, 0, DateTimeKind.Utc), "Payment of $2,000.00 received", new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909") },
                    { new Guid("90000000-0000-0000-0000-000000000004"), new DateTime(2024, 8, 6, 9, 0, 0, 0, DateTimeKind.Utc), "Payment of $1,500.00 received", new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909") },
                    { new Guid("90000000-0000-0000-0000-000000000005"), new DateTime(2024, 8, 9, 18, 0, 0, 0, DateTimeKind.Utc), "Partial payment received", new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909") }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "Amount", "ApplicantName", "CurrentBalance", "Status" },
                values: new object[,]
                {
                    { new Guid("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10"), 4000m, "Ethan Wilson", 0m, 2 },
                    { new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11"), 10000m, "Sophia Moore", 10000m, 1 },
                    { new Guid("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12"), 2500m, "Gabriel Taylor", 500m, 1 },
                    { new Guid("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01"), 1500m, "Maria Silva", 500m, 1 },
                    { new Guid("2b3c934a-78d2-4b56-a5d4-55a83954ae02"), 1000m, "João Souza", 0m, 2 },
                    { new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03"), 5000m, "Alice Johnson", 5000m, 1 },
                    { new Guid("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04"), 7500m, "Michael Smith", 2500m, 1 },
                    { new Guid("5c334e60-81a2-42a0-89e2-0f80c4b1e405"), 2000m, "Laura Martinez", 0m, 2 },
                    { new Guid("6a223421-38a9-43ff-b7b2-dab9b5e8e206"), 12000m, "Daniel Kim", 12000m, 1 },
                    { new Guid("7e21f316-9475-4a3e-9c6d-61c5f1a3b207"), 3000m, "Emma Brown", 1500m, 1 },
                    { new Guid("8f42e9a1-3cdd-470b-8779-319ae346f508"), 900m, "Lucas Williams", 0m, 2 },
                    { new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909"), 6500m, "Olivia Davis", 3000m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
