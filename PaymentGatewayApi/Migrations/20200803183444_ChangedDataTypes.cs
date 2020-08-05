using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGatewayApi.Migrations
{
    public partial class ChangedDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CVV",
                table: "Cards",
                newName: "Cvv");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Cards",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "Cvv",
                table: "Cards",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cvv",
                table: "Cards",
                newName: "CVV");

            migrationBuilder.AlterColumn<int>(
                name: "CVV",
                table: "Cards",
                type: "int",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CardNumber",
                table: "Cards",
                type: "bigint",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);
        }
    }
}
