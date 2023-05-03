using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yuxi.Andres.Test.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class YUXI_TEST_V03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenDate",
                schema: "YuxiTest",
                table: "LocationAggregate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseDate",
                schema: "YuxiTest",
                table: "LocationAggregate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OpenDate",
                schema: "YuxiTest",
                table: "LocationAggregate",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CloseDate",
                schema: "YuxiTest",
                table: "LocationAggregate",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
