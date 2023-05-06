using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceDiscountsWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBadHabitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDrinking",
                table: "UserBadHabits");

            migrationBuilder.DropColumn(
                name: "IsSmoking",
                table: "UserBadHabits");

            migrationBuilder.AddColumn<Guid>(
                name: "BadHabitId",
                table: "UserBadHabits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BadHabit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadHabit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBadHabits_BadHabitId",
                table: "UserBadHabits",
                column: "BadHabitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadHabits_BadHabit_BadHabitId",
                table: "UserBadHabits",
                column: "BadHabitId",
                principalTable: "BadHabit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBadHabits_BadHabit_BadHabitId",
                table: "UserBadHabits");

            migrationBuilder.DropTable(
                name: "BadHabit");

            migrationBuilder.DropIndex(
                name: "IX_UserBadHabits_BadHabitId",
                table: "UserBadHabits");

            migrationBuilder.DropColumn(
                name: "BadHabitId",
                table: "UserBadHabits");

            migrationBuilder.AddColumn<bool>(
                name: "IsDrinking",
                table: "UserBadHabits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSmoking",
                table: "UserBadHabits",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
