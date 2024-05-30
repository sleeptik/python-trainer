using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Trainer.Database.Migrations
{
    /// <inheritdoc />
    public partial class addAssignmentStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentStatusId",
                table: "Assignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssignmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignmentStatusId",
                table: "Assignments",
                column: "AssignmentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AssignmentStatuses_AssignmentStatusId",
                table: "Assignments",
                column: "AssignmentStatusId",
                principalTable: "AssignmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AssignmentStatuses_AssignmentStatusId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "AssignmentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssignmentStatusId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignmentStatusId",
                table: "Assignments");
        }
    }
}
