using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensier.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AssetTableUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoAssets");

            migrationBuilder.CreateTable(
                name: "AssetTransactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Asset_Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Asset_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Asset_CurrentPrice = table.Column<double>(type: "REAL", nullable: false),
                    PurchasePrice = table.Column<double>(type: "REAL", nullable: false),
                    QuantityOwned = table.Column<double>(type: "REAL", nullable: false),
                    Asset_PercentageChange = table.Column<double>(type: "REAL", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Asset_Image = table.Column<string>(type: "TEXT", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssetTransactions_Accounts_UserID",
                        column: x => x.UserID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransactions_UserID",
                table: "AssetTransactions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetTransactions");

            migrationBuilder.CreateTable(
                name: "CryptoAssets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountHolderID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Coins = table.Column<double>(type: "REAL", nullable: false),
                    PurchasePrice = table.Column<double>(type: "REAL", nullable: false),
                    Asset_ChangesPercentage = table.Column<double>(type: "REAL", nullable: true),
                    Asset_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Asset_Price = table.Column<double>(type: "REAL", nullable: true),
                    Asset_Symbol = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAssets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CryptoAssets_Accounts_AccountHolderID",
                        column: x => x.AccountHolderID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAssets_AccountHolderID",
                table: "CryptoAssets",
                column: "AccountHolderID");
        }
    }
}
