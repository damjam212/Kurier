using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CannotDeliverReason",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "John Doe" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Jane Smith" });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "RequestId", "DeliveryAtWeekend", "DestinationAddress", "Height", "Length", "OwnerId", "Priority", "SourceAddress", "Weight", "Width" },
                values: new object[] { 1, true, "Destination 1", "15", "10", 1, "High", "Source 1", "20", "5" });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "RequestId", "DeliveryAtWeekend", "DestinationAddress", "Height", "Length", "OwnerId", "Priority", "SourceAddress", "Weight", "Width" },
                values: new object[] { 2, false, "Destination 2", "12", "8", 2, "Low", "Source 2", "15", "6" });

            migrationBuilder.InsertData(
                table: "DeliverySteps",
                columns: new[] { "StepId", "CourierName", "Date", "RequestId", "StepDescription" },
                values: new object[,]
                {
                    { 1, "Courier 1", new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7800), 1, "Received" },
                    { 2, "Courier 2", new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7802), 2, "Delivered" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "OfferId", "CannotDeliverReason", "CourierName", "DateDelivered", "DateReceived", "OfferDate", "Price", "RequestId", "Status", "ValidityPeriod" },
                values: new object[,]
                {
                    { 1, null, "Courier 1", new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7784), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7783), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7781), 50.00m, 1, "Accepted", 7 },
                    { 2, "Address not found", "Courier 2", new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7787), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7786), new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7785), 30.00m, 2, "Pending", 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliverySteps",
                keyColumn: "StepId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliverySteps",
                keyColumn: "StepId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "RequestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "RequestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "CannotDeliverReason",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
