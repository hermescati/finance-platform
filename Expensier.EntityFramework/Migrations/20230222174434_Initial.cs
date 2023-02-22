using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensier.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(name: "First_Name", type: "TEXT", nullable: true),
                    LastName = table.Column<string>(name: "Last_Name", type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(name: "Password_Hash", type: "TEXT", nullable: true),
                    DateJoined = table.Column<DateTime>(name: "Date_Joined", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountHolderId = table.Column<int>(name: "Account_Holder_Id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_Account_Holder_Id",
                        column: x => x.AccountHolderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CryptoAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<int>(name: "Account_Id", type: "INTEGER", nullable: false),
                    CryptoSymbol = table.Column<string>(name: "Crypto_Symbol", type: "TEXT", nullable: false),
                    CryptoName = table.Column<string>(name: "Crypto_Name", type: "TEXT", nullable: true),
                    CryptoPrice = table.Column<double>(name: "Crypto_Price", type: "REAL", nullable: true),
                    CryptoChangesPercentage = table.Column<double>(name: "Crypto_ChangesPercentage", type: "REAL", nullable: true),
                    PurchasePrice = table.Column<double>(name: "Purchase_Price", type: "REAL", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryptoAssets_Accounts_Account_Id",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<int>(name: "Account_Id", type: "INTEGER", nullable: false),
                    CompanyName = table.Column<string>(name: "Company_Name", type: "TEXT", nullable: true),
                    SubscriptionPlan = table.Column<string>(name: "Subscription_Plan", type: "TEXT", nullable: true),
                    DueDate = table.Column<DateTime>(name: "Due_Date", type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    SubscriptionType = table.Column<string>(name: "Subscription_Type", type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Accounts_Account_Id",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<int>(name: "Account_Id", type: "INTEGER", nullable: false),
                    TransactionName = table.Column<string>(name: "Transaction_Name", type: "TEXT", nullable: true),
                    ProcessDate = table.Column<DateTime>(name: "Process_Date", type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    TransactionType = table.Column<string>(name: "Transaction_Type", type: "TEXT", nullable: true),
                    IsCredit = table.Column<bool>(name: "Is_Credit", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_Account_Id",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Account_Holder_Id",
                table: "Accounts",
                column: "Account_Holder_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAssets_Account_Id",
                table: "CryptoAssets",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Account_Id",
                table: "Subscriptions",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Account_Id",
                table: "Transactions",
                column: "Account_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoAssets");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
