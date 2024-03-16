using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensier.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTableUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountHolderID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountHolderID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ProcessDate",
                table: "Transactions",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "AccountHolderID",
                table: "Transactions",
                newName: "ProcessedDate");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_UserID",
                table: "Transactions",
                column: "UserID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_UserID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Transactions",
                newName: "ProcessDate");

            migrationBuilder.RenameColumn(
                name: "ProcessedDate",
                table: "Transactions",
                newName: "AccountHolderID");

            migrationBuilder.AddColumn<string>(
                name: "TransactionName",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountHolderID",
                table: "Transactions",
                column: "AccountHolderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountHolderID",
                table: "Transactions",
                column: "AccountHolderID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
