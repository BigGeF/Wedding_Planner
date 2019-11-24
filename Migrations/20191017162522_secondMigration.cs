using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planner_Plan_PlanId",
                table: "Planner");

            migrationBuilder.DropForeignKey(
                name: "FK_Planner_Users_UserId",
                table: "Planner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planner",
                table: "Planner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plan",
                table: "Plan");

            migrationBuilder.RenameTable(
                name: "Planner",
                newName: "Planners");

            migrationBuilder.RenameTable(
                name: "Plan",
                newName: "Plans");

            migrationBuilder.RenameIndex(
                name: "IX_Planner_UserId",
                table: "Planners",
                newName: "IX_Planners_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Planner_PlanId",
                table: "Planners",
                newName: "IX_Planners_PlanId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Plans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planners",
                table: "Planners",
                column: "PlannerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plans",
                table: "Plans",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planners_Plans_PlanId",
                table: "Planners",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Planners_Users_UserId",
                table: "Planners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planners_Plans_PlanId",
                table: "Planners");

            migrationBuilder.DropForeignKey(
                name: "FK_Planners_Users_UserId",
                table: "Planners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plans",
                table: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planners",
                table: "Planners");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Plans");

            migrationBuilder.RenameTable(
                name: "Plans",
                newName: "Plan");

            migrationBuilder.RenameTable(
                name: "Planners",
                newName: "Planner");

            migrationBuilder.RenameIndex(
                name: "IX_Planners_UserId",
                table: "Planner",
                newName: "IX_Planner_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Planners_PlanId",
                table: "Planner",
                newName: "IX_Planner_PlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plan",
                table: "Plan",
                column: "PlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planner",
                table: "Planner",
                column: "PlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planner_Plan_PlanId",
                table: "Planner",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Planner_Users_UserId",
                table: "Planner",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
