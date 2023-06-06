using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMoney.Backend.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayInId",
                table: "Transactions",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PayOutId",
                table: "Transactions",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentOperations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PaymentId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentTransactionId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOperations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PayInId",
                table: "Transactions",
                column: "PayInId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PayOutId",
                table: "Transactions",
                column: "PayOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PaymentOperations_PayInId",
                table: "Transactions",
                column: "PayInId",
                principalTable: "PaymentOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PaymentOperations_PayOutId",
                table: "Transactions",
                column: "PayOutId",
                principalTable: "PaymentOperations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PaymentOperations_PayInId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PaymentOperations_PayOutId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "PaymentOperations");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PayInId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PayOutId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PayInId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PayOutId",
                table: "Transactions");
        }
    }
}
