using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "normal", 1 },
                column: "Email",
                value: "kubakepka503@gmail.com");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "worker", 1 },
                column: "Email",
                value: "kubakepka503@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "kubakepka503@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "normal", 1 },
                column: "Email",
                value: "dkakol01@gmail.com");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "worker", 1 },
                column: "Email",
                value: "dkakol01@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "dkakol01@gmail.com");
        }
    }
}
