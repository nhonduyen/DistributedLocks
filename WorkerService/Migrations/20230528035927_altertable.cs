using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerService.Migrations
{
    /// <inheritdoc />
    public partial class altertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Counter",
                keyColumn: "Id",
                keyValue: new Guid("50b76361-d77c-42be-af81-6b40366f82a9"));

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CounterHistory",
                type: "varbinary(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.InsertData(
                table: "Counter",
                columns: new[] { "Id", "CreatedAt", "LastUpdatedTime", "Like", "UpdatedBy" },
                values: new object[] { new Guid("80f6d8b5-efce-4013-b940-0632b9fd297c"), new DateTime(2023, 5, 28, 3, 59, 27, 803, DateTimeKind.Utc).AddTicks(3550), new DateTime(2023, 5, 28, 3, 59, 27, 803, DateTimeKind.Utc).AddTicks(3550), 0, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Counter",
                keyColumn: "Id",
                keyValue: new Guid("80f6d8b5-efce-4013-b940-0632b9fd297c"));

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CounterHistory",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(18)",
                oldMaxLength: 18);

            migrationBuilder.InsertData(
                table: "Counter",
                columns: new[] { "Id", "CreatedAt", "LastUpdatedTime", "Like", "UpdatedBy" },
                values: new object[] { new Guid("50b76361-d77c-42be-af81-6b40366f82a9"), new DateTime(2023, 5, 28, 1, 58, 27, 735, DateTimeKind.Utc).AddTicks(1328), new DateTime(2023, 5, 28, 1, 58, 27, 735, DateTimeKind.Utc).AddTicks(1328), 0, "" });
        }
    }
}
