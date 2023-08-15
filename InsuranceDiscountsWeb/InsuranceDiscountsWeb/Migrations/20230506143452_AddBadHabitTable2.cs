using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceDiscountsWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBadHabitTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBadHabits_BadHabit_BadHabitId",
                table: "UserBadHabits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadHabit",
                table: "BadHabit");

            migrationBuilder.RenameTable(
                name: "BadHabit",
                newName: "BadHabits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadHabits",
                table: "BadHabits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadHabits_BadHabits_BadHabitId",
                table: "UserBadHabits",
                column: "BadHabitId",
                principalTable: "BadHabits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBadHabits_BadHabits_BadHabitId",
                table: "UserBadHabits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadHabits",
                table: "BadHabits");

            migrationBuilder.RenameTable(
                name: "BadHabits",
                newName: "BadHabit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadHabit",
                table: "BadHabit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadHabits_BadHabit_BadHabitId",
                table: "UserBadHabits",
                column: "BadHabitId",
                principalTable: "BadHabit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
