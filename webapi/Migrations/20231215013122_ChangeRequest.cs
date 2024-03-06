using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class ChangeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_OwnerId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_OwnerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CannotDeliverReason",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserId",
                table: "Requests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_UserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CannotDeliverReason",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_OwnerId",
                table: "Requests",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_OwnerId",
                table: "Requests",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
