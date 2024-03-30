using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkspaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterWorkspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspace_Users_CreatorId",
                table: "Workspace");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceMember_Users_UserId",
                table: "WorkspaceMember");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceMember_Workspace_WorkspaceId",
                table: "WorkspaceMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkspaceMember",
                table: "WorkspaceMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workspace",
                table: "Workspace");

            migrationBuilder.RenameTable(
                name: "WorkspaceMember",
                newName: "WorkspaceMembers");

            migrationBuilder.RenameTable(
                name: "Workspace",
                newName: "Workspaces");

            migrationBuilder.RenameIndex(
                name: "IX_WorkspaceMember_WorkspaceId",
                table: "WorkspaceMembers",
                newName: "IX_WorkspaceMembers_WorkspaceId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkspaceMember_UserId",
                table: "WorkspaceMembers",
                newName: "IX_WorkspaceMembers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Workspace_CreatorId",
                table: "Workspaces",
                newName: "IX_Workspaces_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkspaceMembers",
                table: "WorkspaceMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workspaces",
                table: "Workspaces",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceMembers_Users_UserId",
                table: "WorkspaceMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceMembers_Workspaces_WorkspaceId",
                table: "WorkspaceMembers",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_Users_CreatorId",
                table: "Workspaces",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceMembers_Users_UserId",
                table: "WorkspaceMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceMembers_Workspaces_WorkspaceId",
                table: "WorkspaceMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_Users_CreatorId",
                table: "Workspaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workspaces",
                table: "Workspaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkspaceMembers",
                table: "WorkspaceMembers");

            migrationBuilder.RenameTable(
                name: "Workspaces",
                newName: "Workspace");

            migrationBuilder.RenameTable(
                name: "WorkspaceMembers",
                newName: "WorkspaceMember");

            migrationBuilder.RenameIndex(
                name: "IX_Workspaces_CreatorId",
                table: "Workspace",
                newName: "IX_Workspace_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkspaceMembers_WorkspaceId",
                table: "WorkspaceMember",
                newName: "IX_WorkspaceMember_WorkspaceId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkspaceMembers_UserId",
                table: "WorkspaceMember",
                newName: "IX_WorkspaceMember_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workspace",
                table: "Workspace",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkspaceMember",
                table: "WorkspaceMember",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspace_Users_CreatorId",
                table: "Workspace",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceMember_Users_UserId",
                table: "WorkspaceMember",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceMember_Workspace_WorkspaceId",
                table: "WorkspaceMember",
                column: "WorkspaceId",
                principalTable: "Workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
