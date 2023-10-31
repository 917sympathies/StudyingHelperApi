using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyingHelperApi.Migrations
{
    /// <inheritdoc />
    public partial class @fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_workspaces_WorkspaceId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_workspaces_users_UserId",
                table: "workspaces");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "workspaces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkspaceId",
                table: "tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_workspaces_WorkspaceId",
                table: "tasks",
                column: "WorkspaceId",
                principalTable: "workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workspaces_users_UserId",
                table: "workspaces",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_workspaces_WorkspaceId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_workspaces_users_UserId",
                table: "workspaces");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "workspaces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WorkspaceId",
                table: "tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_workspaces_WorkspaceId",
                table: "tasks",
                column: "WorkspaceId",
                principalTable: "workspaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_workspaces_users_UserId",
                table: "workspaces",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
