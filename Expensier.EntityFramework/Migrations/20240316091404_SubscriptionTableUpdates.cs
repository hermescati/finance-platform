using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensier.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionTableUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Accounts_AccountHolderID",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlan",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "AccountHolderID",
                table: "Subscriptions",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_AccountHolderID",
                table: "Subscriptions",
                newName: "IX_Subscriptions_UserID");

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Subscriptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Plan",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Accounts_UserID",
                table: "Subscriptions",
                column: "UserID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Accounts_UserID",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Subscriptions",
                newName: "AccountHolderID");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_UserID",
                table: "Subscriptions",
                newName: "IX_Subscriptions_AccountHolderID");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Subscriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionPlan",
                table: "Subscriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionType",
                table: "Subscriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Accounts_AccountHolderID",
                table: "Subscriptions",
                column: "AccountHolderID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
