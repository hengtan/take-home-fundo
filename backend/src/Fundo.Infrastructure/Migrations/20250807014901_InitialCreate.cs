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
                    { new Guid("081d1de5-eb89-4d5f-bffc-ab502021f4c6"), new DateTime(2025, 8, 6, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2690), "Loan fully paid", new Guid("5c334e60-81a2-42a0-89e2-0f80c4b1e405") },
                    { new Guid("1b52e0e5-6700-4fac-8eea-5c5f7d120f7f"), new DateTime(2025, 7, 26, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2670), "Loan created: Amount $1,000.00", new Guid("2b3c934a-78d2-4b56-a5d4-55a83954ae02") },
                    { new Guid("276d8e01-6745-4585-bdfa-6c0c05db9724"), new DateTime(2025, 8, 4, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2670), "Loan fully paid", new Guid("2b3c934a-78d2-4b56-a5d4-55a83954ae02") },
                    { new Guid("3132c1c3-8208-4910-8088-a1abc8754c8a"), new DateTime(2025, 7, 25, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2690), "Loan created: Amount $2,000.00", new Guid("5c334e60-81a2-42a0-89e2-0f80c4b1e405") },
                    { new Guid("33ca418f-a02f-49e1-8bfe-aebcec982ee6"), new DateTime(2025, 8, 5, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2710), "Payment of $1,500.00 received", new Guid("7e21f316-9475-4a3e-9c6d-61c5f1a3b207") },
                    { new Guid("35c66de0-e58f-4274-8603-b470c50e821b"), new DateTime(2025, 8, 1, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2710), "Loan created: Amount $3,000.00", new Guid("7e21f316-9475-4a3e-9c6d-61c5f1a3b207") },
                    { new Guid("3809de77-1778-4a1f-9a49-970e41e7b41d"), new DateTime(2025, 7, 18, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2680), "Loan created: Amount $5,000.00", new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03") },
                    { new Guid("3d756b5e-2017-4aa5-8363-af0ad151e985"), new DateTime(2025, 7, 23, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2680), "Loan created: Amount $7,500.00", new Guid("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04") },
                    { new Guid("452457a2-f3c6-4592-9e4a-ece7386e8c31"), new DateTime(2025, 7, 27, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2740), "Loan created: Amount $2,500.00", new Guid("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12") },
                    { new Guid("4fce586f-1024-4fbe-a2ef-151d9302ec2d"), new DateTime(2025, 7, 23, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2700), "Interest charged: $100.00", new Guid("6a223421-38a9-43ff-b7b2-dab9b5e8e206") },
                    { new Guid("5717a56a-2ab9-4c32-8736-e024830b051c"), new DateTime(2025, 8, 5, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2690), "Payment of $5,000.00 received", new Guid("4db4e8ae-d0ff-49fd-ae13-bb3e9f4b3a04") },
                    { new Guid("57a139a7-5b4d-43a8-8440-0c3b992576c8"), new DateTime(2025, 7, 28, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(1440), "Loan created: Amount $1,500.00", new Guid("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01") },
                    { new Guid("5c8a2094-98c2-46c8-9dc4-a888ff095310"), new DateTime(2025, 8, 5, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2720), "Loan fully paid", new Guid("8f42e9a1-3cdd-470b-8779-319ae346f508") },
                    { new Guid("5d36c62a-7f07-48d2-b180-c726b890aece"), new DateTime(2025, 8, 2, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2650), "Payment of $1,000.00 received", new Guid("1a58dcd6-4562-4e1e-9aa9-02129a2f1c01") },
                    { new Guid("60488e8a-69e4-4b9e-8dd3-9d67d29811b5"), new DateTime(2025, 8, 2, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2720), "Payment of $3,500.00 received", new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909") },
                    { new Guid("9ea32480-1753-4b01-baf4-9e4748ce5022"), new DateTime(2025, 7, 24, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2720), "Loan created: Amount $6,500.00", new Guid("9c01fd52-780c-4e84-9ef1-5a3e3c0a5909") },
                    { new Guid("a5dd33d8-16d4-4278-b6d9-7d7c57058f84"), new DateTime(2025, 7, 28, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2680), "Interest charged: $50.00", new Guid("3c1a2d9f-d8f1-4f45-8b90-49a2780b1f03") },
                    { new Guid("b407aca7-d81d-4d02-b184-2a03fd60998c"), new DateTime(2025, 7, 30, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2710), "Loan created: Amount $900.00", new Guid("8f42e9a1-3cdd-470b-8779-319ae346f508") },
                    { new Guid("b80218af-fb48-4823-bd1f-14a64039f80a"), new DateTime(2025, 7, 17, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2740), "Loan created: Amount $10,000.00", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("d2566068-2d4c-4e28-a683-c280a9f011f9"), new DateTime(2025, 8, 6, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2730), "Loan fully paid", new Guid("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10") },
                    { new Guid("d5bd9efc-d845-4525-9f79-930038121ff6"), new DateTime(2025, 7, 31, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2740), "Interest charged: $120.00", new Guid("11aaf61e-4de7-4607-b9fd-dbc81d15cb11") },
                    { new Guid("df249249-f1ff-4aac-a3f4-0b7aec659ac5"), new DateTime(2025, 7, 8, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2700), "Loan created: Amount $12,000.00", new Guid("6a223421-38a9-43ff-b7b2-dab9b5e8e206") },
                    { new Guid("ed813d06-d247-431a-9a4f-50dbbe8acd6a"), new DateTime(2025, 8, 4, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2750), "Payment of $2,000.00 received", new Guid("12bbc84e-5ad8-4c6a-b637-e2cc087c5d12") },
                    { new Guid("feb2efe6-2c0c-48f1-af5b-f901c443c5b8"), new DateTime(2025, 7, 28, 1, 49, 0, 815, DateTimeKind.Utc).AddTicks(2730), "Loan created: Amount $4,000.00", new Guid("10ffbc7e-212f-4fc7-a3ee-9437e66e7e10") }
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
