using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceDiscountsWeb.Migrations
{
    /// <inheritdoc />
    public partial class addColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPolicies_AspNetUsers_UserId1",
                table: "UserPolicies");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "UserPolicies",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPolicies_UserId1",
                table: "UserPolicies",
                newName: "IX_UserPolicies_AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "MaxDiscountPercentage",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthdayDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPolicies_AspNetUsers_AppUserId",
                table: "UserPolicies",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPolicies_AspNetUsers_AppUserId",
                table: "UserPolicies");

            migrationBuilder.DropColumn(
                name: "MaxDiscountPercentage",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BirthdayDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UserPolicies",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserPolicies_AppUserId",
                table: "UserPolicies",
                newName: "IX_UserPolicies_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPolicies_AspNetUsers_UserId1",
                table: "UserPolicies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
