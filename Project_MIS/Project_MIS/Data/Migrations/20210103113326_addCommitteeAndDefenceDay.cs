using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_MIS.Data.Migrations
{
    public partial class addCommitteeAndDefenceDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectGroups",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EvaluationCommittees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    LecturerId = table.Column<int>(nullable: false),
                    LecturerTask = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCommittees_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefenceDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefenceDateTime = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ProjectGroupId = table.Column<int>(nullable: false),
                    ProjectState = table.Column<int>(nullable: false),
                    CommitteeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenceDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefenceDays_EvaluationCommittees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "EvaluationCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefenceDays_ProjectGroups_ProjectGroupId",
                        column: x => x.ProjectGroupId,
                        principalTable: "ProjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefenceDays_CommitteeId",
                table: "DefenceDays",
                column: "CommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenceDays_ProjectGroupId",
                table: "DefenceDays",
                column: "ProjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCommittees_LecturerId",
                table: "EvaluationCommittees",
                column: "LecturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefenceDays");

            migrationBuilder.DropTable(
                name: "EvaluationCommittees");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectGroups");
        }
    }
}
