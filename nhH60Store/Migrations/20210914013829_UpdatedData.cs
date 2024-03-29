﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace nhH60Store.Migrations
{
    public partial class UpdatedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "Customer",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "CreditCard", "Province" },
                values: new object[] { "4309827387307397", "QC" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "CreditCard", "Province" },
                values: new object[] { "4309827387307397", "QC" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "CreditCard", "Province" },
                values: new object[] { "4309827387307397", "QC" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "Customer",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "CreditCard", "Province" },
                values: new object[] { null, " " });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "CreditCard", "Province" },
                values: new object[] { null, " " });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "CreditCard", "Province" },
                values: new object[] { null, " " });
        }
    }
}
