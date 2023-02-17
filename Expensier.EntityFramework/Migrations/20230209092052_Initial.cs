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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(name: "First_Name", type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(name: "Last_Name", type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateJoined = table.Column<DateTime>(name: "Date_Joined", type: "datetime2", nullable: false),
                    ProfilePicture = table.Column<string>(name: "Profile_Picture", type: "nvarchar(max)", nullable: true, defaultValue: "/Styles/Images/default_profile_picture.png")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountHolderId = table.Column<int>(name: "Account_Holder_Id", type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(name: "Account_Id", type: "int", nullable: false),
                    CryptoSymbol = table.Column<string>(name: "Crypto_Symbol", type: "nvarchar(max)", nullable: false),
                    CryptoName = table.Column<string>(name: "Crypto_Name", type: "nvarchar(max)", nullable: true),
                    CryptoPrice = table.Column<double>(name: "Crypto_Price", type: "float", nullable: true),
                    CryptoChangesPercentage = table.Column<double>(name: "Crypto_ChangesPercentage", type: "float", nullable: true),
                    PurchasePrice = table.Column<double>(name: "Purchase_Price", type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(name: "Account_Id", type: "int", nullable: false),
                    CompanyName = table.Column<string>(name: "Company_Name", type: "nvarchar(max)", nullable: true),
                    SubscriptionPlan = table.Column<string>(name: "Subscription_Plan", type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(name: "Due_Date", type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    SubscriptionType = table.Column<string>(name: "Subscription_Type", type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(name: "Account_Id", type: "int", nullable: false),
                    TransactionName = table.Column<string>(name: "Transaction_Name", type: "nvarchar(max)", nullable: true),
                    ProcessDate = table.Column<DateTime>(name: "Process_Date", type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionType = table.Column<string>(name: "Transaction_Type", type: "nvarchar(max)", nullable: true),
                    IsCredit = table.Column<bool>(name: "Is_Credit", type: "bit", nullable: false)
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
