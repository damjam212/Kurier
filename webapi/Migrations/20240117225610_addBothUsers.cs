using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class addBothUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email" },
                values: new object[] { 2, "dkakol01@gmail.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Role", "UserId", "Email" },
                values: new object[] { "normal", 2, "dkakol01@gmail.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Role", "UserId", "Email" },
                values: new object[] { "worker", 2, "dkakol01@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "normal", 2 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "worker", 2 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
