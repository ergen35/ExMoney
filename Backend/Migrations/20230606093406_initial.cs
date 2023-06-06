using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMoney.Backend.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Symbol = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExMoneySettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommissionPercentage = table.Column<double>(type: "double", nullable: false),
                    EmailVerificationEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdentityVerificationEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PhoneVerificationEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExMoneySettings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExMoneyWallets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExMoneyWallets", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KycVerifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    IdDocumentRectoPic = table.Column<byte[]>(type: "longblob", nullable: true),
                    IdDocumentVersoPic = table.Column<byte[]>(type: "longblob", nullable: true),
                    UserPic = table.Column<byte[]>(type: "longblob", nullable: true),
                    VerificationResult = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KycVerifications", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentProcessors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecretKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SupportedCurrencies = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProcessors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    Rate = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseCurrencyId = table.Column<int>(type: "int", nullable: true),
                    ChangeCurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Currencies_BaseCurrencyId",
                        column: x => x.BaseCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Currencies_ChangeCurrencyId",
                        column: x => x.ChangeCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "FCFA", "XOF" },
                    { 2, "Naira", "NGN" }
                });

            migrationBuilder.InsertData(
                table: "ExMoneySettings",
                columns: new[] { "Id", "CommissionPercentage", "EmailVerificationEnabled", "IdentityVerificationEnabled", "PhoneVerificationEnabled" },
                values: new object[] { 1, 0.050000000000000003, false, false, false });

            migrationBuilder.InsertData(
                table: "ExMoneyWallets",
                columns: new[] { "Id", "Balance", "CurrencyId", "Name" },
                values: new object[,]
                {
                    { "001066c3-457b-4b3c-a942-b4ebca1afeb2", 26400.0, 2, "Réserve Naira" },
                    { "bab8e547-7f6e-4955-976f-80b9c4a2298e", 13674.0, 1, "Réserve CFA" }
                });

            migrationBuilder.InsertData(
                table: "PaymentProcessors",
                columns: new[] { "Id", "ApiKey", "Name", "SecretKey", "SupportedCurrencies" },
                values: new object[] { "8ce50226-1307-4de8-aff7-d839042dec53", "k", "KkiaPay", "", "xof" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BaseCurrencyId",
                table: "Transactions",
                column: "BaseCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ChangeCurrencyId",
                table: "Transactions",
                column: "ChangeCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExMoneySettings");

            migrationBuilder.DropTable(
                name: "ExMoneyWallets");

            migrationBuilder.DropTable(
                name: "KycVerifications");

            migrationBuilder.DropTable(
                name: "PaymentProcessors");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
