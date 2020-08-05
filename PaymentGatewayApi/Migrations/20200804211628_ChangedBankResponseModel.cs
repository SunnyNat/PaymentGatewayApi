using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGatewayApi.Migrations
{
    public partial class ChangedBankResponseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_BankResponses_BankResponseId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_BankResponseId",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "BankResponseId",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "BankResponses");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PaymentDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentDetails");

            migrationBuilder.AddColumn<int>(
                name: "BankResponseId",
                table: "PaymentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "BankResponses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_BankResponseId",
                table: "PaymentDetails",
                column: "BankResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_BankResponses_BankResponseId",
                table: "PaymentDetails",
                column: "BankResponseId",
                principalTable: "BankResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
