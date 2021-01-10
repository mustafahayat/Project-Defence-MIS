using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_MIS.Data.Migrations
{
    public partial class addCommitteeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EvaluationCommittees");

            migrationBuilder.AddColumn<int>(
                name: "CommitteeId",
                table: "EvaluationCommittees",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCommittees_CommitteeId",
                table: "EvaluationCommittees",
                column: "CommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationCommittees_Committees_CommitteeId",
                table: "EvaluationCommittees",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationCommittees_Committees_CommitteeId",
                table: "EvaluationCommittees");

            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationCommittees_CommitteeId",
                table: "EvaluationCommittees");

            migrationBuilder.DropColumn(
                name: "CommitteeId",
                table: "EvaluationCommittees");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EvaluationCommittees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
