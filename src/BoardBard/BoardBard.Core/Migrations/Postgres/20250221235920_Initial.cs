using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoardBard.Core.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "LabelTypes",
                schema: "Core",
                columns: table => new
                {
                    LabelTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ColorHex = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTypes", x => x.LabelTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TaskBoards",
                schema: "Core",
                columns: table => new
                {
                    TaskBoardId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoardName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Starred = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskBoards", x => x.TaskBoardId);
                });

            migrationBuilder.CreateTable(
                name: "TaskCards",
                schema: "Core",
                columns: table => new
                {
                    TaskCardId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskBoardId = table.Column<int>(type: "integer", nullable: false),
                    CardName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    Collapsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCards", x => x.TaskCardId);
                    table.ForeignKey(
                        name: "FK_TaskCards_TaskBoards_TaskBoardId",
                        column: x => x.TaskBoardId,
                        principalSchema: "Core",
                        principalTable: "TaskBoards",
                        principalColumn: "TaskBoardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "Core",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskCardId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(40000)", maxLength: 40000, nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskCards_TaskCardId",
                        column: x => x.TaskCardId,
                        principalSchema: "Core",
                        principalTable: "TaskCards",
                        principalColumn: "TaskCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskActivities",
                schema: "Core",
                columns: table => new
                {
                    TaskActivityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskItemId = table.Column<int>(type: "integer", nullable: false),
                    Detail = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskActivities", x => x.TaskActivityId);
                    table.ForeignKey(
                        name: "FK_TaskActivities_Tasks_TaskItemId",
                        column: x => x.TaskItemId,
                        principalSchema: "Core",
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskLabelLinks",
                schema: "Core",
                columns: table => new
                {
                    TaskLabelLinkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    LabelTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLabelLinks", x => x.TaskLabelLinkId);
                    table.ForeignKey(
                        name: "FK_TaskLabelLinks_LabelTypes_LabelTypeId",
                        column: x => x.LabelTypeId,
                        principalSchema: "Core",
                        principalTable: "LabelTypes",
                        principalColumn: "LabelTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskLabelLinks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "Core",
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskActivities_TaskItemId",
                schema: "Core",
                table: "TaskActivities",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCards_TaskBoardId",
                schema: "Core",
                table: "TaskCards",
                column: "TaskBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLabelLinks_LabelTypeId",
                schema: "Core",
                table: "TaskLabelLinks",
                column: "LabelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLabelLinks_TaskId",
                schema: "Core",
                table: "TaskLabelLinks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskCardId",
                schema: "Core",
                table: "Tasks",
                column: "TaskCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskActivities",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "TaskLabelLinks",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "LabelTypes",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "TaskCards",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "TaskBoards",
                schema: "Core");
        }
    }
}
