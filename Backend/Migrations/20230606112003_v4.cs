using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMoney.Backend.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyEcxhangeBaseUrl",
                table: "ExMoneySettings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyExchangeApiKey",
                table: "ExMoneySettings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ExMoneySettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CurrencyEcxhangeBaseUrl", "CurrencyExchangeApiKey" },
                values: new object[] { "http://currencyapi.com", "STNcvlsyq6QpULgJhQYKqKqym6YI5MjrdPBalf5x" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyEcxhangeBaseUrl",
                table: "ExMoneySettings");

            migrationBuilder.DropColumn(
                name: "CurrencyExchangeApiKey",
                table: "ExMoneySettings");
        }
    }
}
