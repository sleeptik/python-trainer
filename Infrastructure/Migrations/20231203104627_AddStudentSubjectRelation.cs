using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentSubjectRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    StudentUserId = table.Column<int>(type: "integer", nullable: false),
                    SubjectsToStudyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => new { x.StudentUserId, x.SubjectsToStudyId });
                    table.ForeignKey(
                        name: "FK_StudentSubject_Students_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subjects_SubjectsToStudyId",
                        column: x => x.SubjectsToStudyId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_SubjectsToStudyId",
                table: "StudentSubject",
                column: "SubjectsToStudyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSubject");
        }
    }
}
