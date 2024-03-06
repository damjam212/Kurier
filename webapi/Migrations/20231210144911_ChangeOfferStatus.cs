using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class ChangeOfferStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Offers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "DeliverySteps",
                keyColumn: "StepId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "DeliverySteps",
                keyColumn: "StepId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "DateDelivered", "DateReceived", "OfferDate", "Status" },
                values: new object[] { new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4133), new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4133), new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4130), 1 });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "DateDelivered", "DateReceived", "OfferDate", "Status" },
                values: new object[] { new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4136), new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4136), new DateTime(2023, 12, 10, 14, 49, 11, 10, DateTimeKind.Utc).AddTicks(4135), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "DeliverySteps",
                keyColumn: "StepId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "DeliverySteps",
                keyColumn: "StepId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7802));

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "DateDelivered", "DateReceived", "OfferDate", "Status" },
                values: new object[] { new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7784), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7783), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7781), "Accepted" });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "DateDelivered", "DateReceived", "OfferDate", "Status" },
                values: new object[] { new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7787), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7786), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7785), "Pending" });
        }
    }
}
