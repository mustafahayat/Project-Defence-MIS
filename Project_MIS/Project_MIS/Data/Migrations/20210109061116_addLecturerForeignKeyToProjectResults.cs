using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_MIS.Data.Migrations
{
    public partial class addLecturerForeignKeyToProjectResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefenceDays");

            migrationBuilder.CreateTable(
                name: "ProjectResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefenceDateTime = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectState = table.Column<int>(nullable: false),
                    ECommitteeId = table.Column<int>(nullable: false),
                    LecturerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectResults_EvaluationCommittees_ECommitteeId",
                        column: x => x.ECommitteeId,
                        principalTable: "EvaluationCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectResults_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProjectResults_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResults_ECommitteeId",
                table: "ProjectResults",
                column: "ECommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResults_LecturerId",
                table: "ProjectResults",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResults_ProjectId",
                table: "ProjectResults",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectResults");

            migrationBuilder.CreateTable(
                name: "DefenceDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommitteeId = table.Column<int>(type: "int", nullable: false),
                    DefenceDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectState = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_DefenceDays_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefenceDays_CommitteeId",
                table: "DefenceDays",
                column: "CommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenceDays_ProjectId",
                table: "DefenceDays",
                column: "ProjectId");
        }
    }
}
