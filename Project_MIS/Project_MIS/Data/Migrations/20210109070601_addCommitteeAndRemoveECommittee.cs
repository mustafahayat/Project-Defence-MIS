using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_MIS.Data.Migrations
{
    public partial class addCommitteeAndRemoveECommittee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectResults_EvaluationCommittees_ECommitteeId",
                table: "ProjectResults");

            migrationBuilder.DropIndex(
                name: "IX_ProjectResults_ECommitteeId",
                table: "ProjectResults");

            migrationBuilder.DropColumn(
                name: "ECommitteeId",
                table: "ProjectResults");

            migrationBuilder.AddColumn<int>(
                name: "CommitteeId",
                table: "ProjectResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResults_CommitteeId",
                table: "ProjectResults",
                column: "CommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectResults_Committees_CommitteeId",
                table: "ProjectResults",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectResults_Committees_CommitteeId",
                table: "ProjectResults");

            migrationBuilder.DropIndex(
                name: "IX_ProjectResults_CommitteeId",
                table: "ProjectResults");

            migrationBuilder.DropColumn(
                name: "CommitteeId",
                table: "ProjectResults");

            migrationBuilder.AddColumn<int>(
                name: "ECommitteeId",
                table: "ProjectResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResults_ECommitteeId",
                table: "ProjectResults",
                column: "ECommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectResults_EvaluationCommittees_ECommitteeId",
                table: "ProjectResults",
                column: "ECommitteeId",
                principalTable: "EvaluationCommittees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
