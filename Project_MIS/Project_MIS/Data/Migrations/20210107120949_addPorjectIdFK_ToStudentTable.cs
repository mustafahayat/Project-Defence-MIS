using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_MIS.Data.Migrations
{
    public partial class addPorjectIdFK_ToStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DefenceDays_ProjectGroups_ProjectGroupId",
                table: "DefenceDays");

            migrationBuilder.DropTable(
                name: "ProjectGroups");

            migrationBuilder.DropIndex(
                name: "IX_DefenceDays_ProjectGroupId",
                table: "DefenceDays");

            migrationBuilder.DropColumn(
                name: "ProjectGroupId",
                table: "DefenceDays");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "DefenceDays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    LecturerId = table.Column<int>(nullable: false),
                    ProjectState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProjectId",
                table: "Students",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenceDays_ProjectId",
                table: "DefenceDays",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LecturerId",
                table: "Projects",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefenceDays_Projects_ProjectId",
                table: "DefenceDays",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Projects_ProjectId",
                table: "Students",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DefenceDays_Projects_ProjectId",
                table: "DefenceDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Projects_ProjectId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProjectId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_DefenceDays_ProjectId",
                table: "DefenceDays");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "DefenceDays");

            migrationBuilder.AddColumn<int>(
                name: "ProjectGroupId",
                table: "DefenceDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    ProjectState = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectGroups_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectGroups_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefenceDays_ProjectGroupId",
                table: "DefenceDays",
                column: "ProjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroups_LecturerId",
                table: "ProjectGroups",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroups_StudentId",
                table: "ProjectGroups",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefenceDays_ProjectGroups_ProjectGroupId",
                table: "DefenceDays",
                column: "ProjectGroupId",
                principalTable: "ProjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
