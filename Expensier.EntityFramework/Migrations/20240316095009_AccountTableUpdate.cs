using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensier.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AccountTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_AccountHolderID",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "AccountHolderID",
                table: "Accounts",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountHolderID",
                table: "Accounts",
                newName: "IX_Accounts_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Accounts",
                newName: "AccountHolderID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserID",
                table: "Accounts",
                newName: "IX_Accounts_AccountHolderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_AccountHolderID",
                table: "Accounts",
                column: "AccountHolderID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
