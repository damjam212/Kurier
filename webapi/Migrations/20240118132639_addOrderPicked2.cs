using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class addOrderPicked2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPicked_Orders_OrderId",
                table: "OrderPicked");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPicked_Orders_OrderId1",
                table: "OrderPicked");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPicked",
                table: "OrderPicked");

            migrationBuilder.DropIndex(
                name: "IX_OrderPicked_OrderId",
                table: "OrderPicked");

            migrationBuilder.RenameColumn(
                name: "OrderId1",
                table: "OrderPicked",
                newName: "OrderId2");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPicked_OrderId1",
                table: "OrderPicked",
                newName: "IX_OrderPicked_OrderId2");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "OrderPicked",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OrderRequestId",
                table: "OrderPicked",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderPicked",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OrderKey",
                table: "OrderPicked",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPicked",
                table: "OrderPicked",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPicked_OrderKey",
                table: "OrderPicked",
                column: "OrderKey");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPicked_Orders_OrderId2",
                table: "OrderPicked",
                column: "OrderId2",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPicked_Orders_OrderKey",
                table: "OrderPicked",
                column: "OrderKey",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPicked_Orders_OrderId2",
                table: "OrderPicked");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPicked_Orders_OrderKey",
                table: "OrderPicked");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPicked",
                table: "OrderPicked");

            migrationBuilder.DropIndex(
                name: "IX_OrderPicked_OrderKey",
                table: "OrderPicked");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderPicked");

            migrationBuilder.DropColumn(
                name: "OrderKey",
                table: "OrderPicked");

            migrationBuilder.RenameColumn(
                name: "OrderId2",
                table: "OrderPicked",
                newName: "OrderId1");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPicked_OrderId2",
                table: "OrderPicked",
                newName: "IX_OrderPicked_OrderId1");

            migrationBuilder.AlterColumn<int>(
                name: "OrderRequestId",
                table: "OrderPicked",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderPicked",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPicked",
                table: "OrderPicked",
                columns: new[] { "OrderRequestId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPicked_OrderId",
                table: "OrderPicked",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPicked_Orders_OrderId",
                table: "OrderPicked",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPicked_Orders_OrderId1",
                table: "OrderPicked",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
