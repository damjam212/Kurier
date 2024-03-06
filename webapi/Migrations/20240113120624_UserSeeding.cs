using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class UserSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email" },
                values: new object[] { 1, "dkakol01@gmail.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Role", "UserId", "Email" },
                values: new object[] { "normal", 1, "dkakol01@gmail.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Role", "UserId", "Email" },
                values: new object[] { "worker", 1, "dkakol01@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "normal", 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "worker", 1 });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
