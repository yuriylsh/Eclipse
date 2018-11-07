﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Yuriy.Web.Migrations
{
    public partial class SeedingUserAndNotificationTypeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "New Comment" },
                    { 2, "Campaign Status Changed " },
                    { 3, "New Report Available" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, "William", "Brown", "WilliamPassword" },
                    { 2, "Kyle", "Burnham", "KylePassword" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotificationType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
