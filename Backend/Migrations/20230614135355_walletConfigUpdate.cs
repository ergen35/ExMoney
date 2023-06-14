using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMoney.Backend.Migrations
{
    public partial class walletConfigUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ExMoneyWallets",
                columns: new[] { "Id", "Balance", "CurrencyId", "Name", "OwnerId" },
                values: new object[] { "b4bca84f-0520-4c99-97d1-433d4df7d802", 0.0, 2, "NGN Wallet", "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af" });

            migrationBuilder.InsertData(
                table: "ExMoneyWallets",
                columns: new[] { "Id", "Balance", "CurrencyId", "Name", "OwnerId" },
                values: new object[] { "f07ce690-336e-45dd-aee7-11dac71b37e4", 27000.0, 1, "XOF Wallet", "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "BirthDate", "Country", "CreationDate", "Email", "EmailVerified", "FirstName", "LastName", "Phone", "PhoneVerified", "Points", "Sex" },
                values: new object[] { "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af", "Porto-Novo, Bénin", new DateTime(2000, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bénin", new DateTime(2023, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "wassi@gmail.com", true, "wassi", "harif", "+22990210790", true, 45, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExMoneyWallets",
                keyColumn: "Id",
                keyValue: "b4bca84f-0520-4c99-97d1-433d4df7d802");

            migrationBuilder.DeleteData(
                table: "ExMoneyWallets",
                keyColumn: "Id",
                keyValue: "f07ce690-336e-45dd-aee7-11dac71b37e4");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "User");

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "User",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
