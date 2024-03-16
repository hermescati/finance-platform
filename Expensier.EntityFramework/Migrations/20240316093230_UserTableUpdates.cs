using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensier.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UserTableUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedDate",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCredit",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Transactions",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Subscriptions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Frequency",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Subscriptions",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "PortfolioReturns",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "CryptoAssets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedDate",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCredit",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Transactions",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Subscriptions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Frequency",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Subscriptions",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "PortfolioReturns",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "CryptoAssets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 0);
        }
    }
}
