using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMoney.Backend.Migrations
{
    public partial class settingsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LatestF2NRate",
                table: "ExMoneySettings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LatestN2FRate",
                table: "ExMoneySettings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "ExMoneySettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LatestF2NRate", "LatestN2FRate" },
                values: new object[] { 1.3200000000000001, 0.84999999999999998 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestF2NRate",
                table: "ExMoneySettings");

            migrationBuilder.DropColumn(
                name: "LatestN2FRate",
                table: "ExMoneySettings");
        }
    }
}
