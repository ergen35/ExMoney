using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMoney.Backend.Migrations
{
    public partial class transactionpropsupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rate",
                table: "Transactions",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "ChangeCurrency",
                table: "Transactions",
                newName: "ChangeCurrencyId");

            migrationBuilder.RenameColumn(
                name: "BaseCurrency",
                table: "Transactions",
                newName: "BaseCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Transactions",
                newName: "rate");

            migrationBuilder.RenameColumn(
                name: "ChangeCurrencyId",
                table: "Transactions",
                newName: "ChangeCurrency");

            migrationBuilder.RenameColumn(
                name: "BaseCurrencyId",
                table: "Transactions",
                newName: "BaseCurrency");
        }
    }
}
