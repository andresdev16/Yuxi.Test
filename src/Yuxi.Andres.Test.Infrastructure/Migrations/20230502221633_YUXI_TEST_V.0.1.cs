using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yuxi.Andres.Test.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class YUXI_TEST_V01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "YuxiTest");

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "YuxiTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CloseDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location",
                schema: "YuxiTest");
        }
    }
}
