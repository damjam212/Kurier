using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class addOrderPicked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderPicked",
                columns: table => new
                {
                    OrderRequestId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPicked", x => new { x.OrderRequestId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderPicked_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPicked_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPicked_OrderId",
                table: "OrderPicked",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPicked_OrderId1",
                table: "OrderPicked",
                column: "OrderId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPicked");
        }
    }
}
