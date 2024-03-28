﻿// <auto-generated />
using System;
using Expensier.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Expensier.EntityFramework.Migrations
{
    [DbContext(typeof(ExpensierDbContext))]
    [Migration("20240327231122_AddedAssetID")]
    partial class AddedAssetID
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Expensier.Domain.Models.Account", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Expensier.Domain.Models.AssetTransaction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(9);

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(11);

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("REAL")
                        .HasColumnOrder(6);

                    b.Property<double>("QuantityOwned")
                        .HasColumnType("REAL")
                        .HasColumnOrder(7);

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("AssetTransactions");
                });

            modelBuilder.Entity("Expensier.Domain.Models.PortfolioReturn", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<Guid>("AccountHolderID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RecordedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("ReturnPercentage")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("AccountHolderID");

                    b.ToTable("PortfolioReturns");
                });

            modelBuilder.Entity("Expensier.Domain.Models.Subscription", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<double>("Amount")
                        .HasColumnType("REAL")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(7);

                    b.Property<string>("Frequency")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(2);

                    b.Property<string>("Plan")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(3);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(6);

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Expensier.Domain.Models.Transaction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<double>("Amount")
                        .HasColumnType("REAL")
                        .HasColumnOrder(4);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(3);

                    b.Property<bool>("IsCredit")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("ProcessedDate")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(6);

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Expensier.Domain.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(3);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(5);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(2);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(4);

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Expensier.Domain.Models.Account", b =>
                {
                    b.HasOne("Expensier.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Expensier.Domain.Models.AssetTransaction", b =>
                {
                    b.HasOne("Expensier.Domain.Models.Account", "User")
                        .WithMany("AssetList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Expensier.Domain.Models.Asset", "Asset", b1 =>
                        {
                            b1.Property<Guid>("AssetTransactionID")
                                .HasColumnType("TEXT");

                            b1.Property<double>("CurrentPrice")
                                .HasColumnType("REAL")
                                .HasColumnOrder(5);

                            b1.Property<string>("ID")
                                .HasColumnType("TEXT")
                                .HasColumnOrder(2);

                            b1.Property<string>("Image")
                                .HasColumnType("TEXT")
                                .HasColumnOrder(10);

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnOrder(4);

                            b1.Property<double?>("PercentageChange")
                                .HasColumnType("REAL")
                                .HasColumnOrder(8);

                            b1.Property<string>("Symbol")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnOrder(3);

                            b1.HasKey("AssetTransactionID");

                            b1.ToTable("AssetTransactions");

                            b1.WithOwner()
                                .HasForeignKey("AssetTransactionID");
                        });

                    b.Navigation("Asset")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Expensier.Domain.Models.PortfolioReturn", b =>
                {
                    b.HasOne("Expensier.Domain.Models.Account", "AccountHolder")
                        .WithMany("PortfolioReturn")
                        .HasForeignKey("AccountHolderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountHolder");
                });

            modelBuilder.Entity("Expensier.Domain.Models.Subscription", b =>
                {
                    b.HasOne("Expensier.Domain.Models.Account", "User")
                        .WithMany("SubscriptionList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Expensier.Domain.Models.Transaction", b =>
                {
                    b.HasOne("Expensier.Domain.Models.Account", "User")
                        .WithMany("TransactionList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Expensier.Domain.Models.Account", b =>
                {
                    b.Navigation("AssetList");

                    b.Navigation("PortfolioReturn");

                    b.Navigation("SubscriptionList");

                    b.Navigation("TransactionList");
                });
#pragma warning restore 612, 618
        }
    }
}