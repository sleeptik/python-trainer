using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Difficulties_DifficultyId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "UserRanks");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.RenameColumn(
                name: "DifficultyId",
                table: "Exercises",
                newName: "RankId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_DifficultyId",
                table: "Exercises",
                newName: "IX_Exercises_RankId");

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LowerBound = table.Column<float>(type: "real", nullable: false),
                    UpperBound = table.Column<float>(type: "real", nullable: false),
                    FallThreshold = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CurrentRankId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Students_Ranks_CurrentRankId",
                        column: x => x.CurrentRankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false),
                    IsPassed = table.Column<bool>(type: "boolean", nullable: true),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => new { x.StudentId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_Assignments_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ExerciseId",
                table: "Assignments",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CurrentRankId",
                table: "Students",
                column: "CurrentRankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Ranks_RankId",
                table: "Exercises",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Ranks_RankId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.RenameColumn(
                name: "RankId",
                table: "Exercises",
                newName: "DifficultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_RankId",
                table: "Exercises",
                newName: "IX_Exercises_DifficultyId");

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false),
                    Finished = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsPassed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => new { x.UserId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_Histories_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRanks",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AssignedDifficultyId = table.Column<int>(type: "integer", nullable: false),
                    Metric = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRanks", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserRanks_Difficulties_AssignedDifficultyId",
                        column: x => x.AssignedDifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRanks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ExerciseId",
                table: "Histories",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRanks_AssignedDifficultyId",
                table: "UserRanks",
                column: "AssignedDifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Difficulties_DifficultyId",
                table: "Exercises",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
