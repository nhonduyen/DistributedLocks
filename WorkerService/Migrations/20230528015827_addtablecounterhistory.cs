using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerService.Migrations
{
    /// <inheritdoc />
    public partial class addtablecounterhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Counter",
                keyColumn: "Id",
                keyValue: new Guid("4cc69f92-015b-493f-9d14-59a99971e3f2"));

            migrationBuilder.CreateTable(
                name: "CounterHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterHistory", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Counter",
                columns: new[] { "Id", "CreatedAt", "LastUpdatedTime", "Like", "UpdatedBy" },
                values: new object[] { new Guid("50b76361-d77c-42be-af81-6b40366f82a9"), new DateTime(2023, 5, 28, 1, 58, 27, 735, DateTimeKind.Utc).AddTicks(1328), new DateTime(2023, 5, 28, 1, 58, 27, 735, DateTimeKind.Utc).AddTicks(1328), 0, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounterHistory");

            migrationBuilder.DeleteData(
                table: "Counter",
                keyColumn: "Id",
                keyValue: new Guid("50b76361-d77c-42be-af81-6b40366f82a9"));

            migrationBuilder.InsertData(
                table: "Counter",
                columns: new[] { "Id", "CreatedAt", "LastUpdatedTime", "Like", "UpdatedBy" },
                values: new object[] { new Guid("4cc69f92-015b-493f-9d14-59a99971e3f2"), new DateTime(2023, 5, 28, 1, 54, 41, 684, DateTimeKind.Utc).AddTicks(4449), new DateTime(2023, 5, 28, 1, 54, 41, 684, DateTimeKind.Utc).AddTicks(4449), 0, "" });
        }
    }
}
