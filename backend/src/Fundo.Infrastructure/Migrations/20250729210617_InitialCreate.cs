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
                name: "Loans");
        }
    }
}
