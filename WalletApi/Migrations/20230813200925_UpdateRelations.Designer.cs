﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WalletApi.Data.DatabaseContexts;

#nullable disable

namespace WalletApi.Migrations
{
    [DbContext(typeof(WalletDbContext))]
    [Migration("20230813200925_UpdateRelations")]
    partial class UpdateRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WalletApi.Data.Entities.WalletAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<double>("Balance")
                        .HasColumnType("double precision")
                        .HasColumnName("Balance");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("DailyPoints")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DailyPoints");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("Name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("WalletAccount", (string)null);
                });

            modelBuilder.Entity("WalletApi.Data.Entities.WalletTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsPending")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("IsPending");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ModifiedDate");

                    b.Property<double>("Sum")
                        .HasColumnType("double precision")
                        .HasColumnName("Sum");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("Title");

                    b.Property<int>("Type")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("Type");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("WalletTransaction", (string)null);
                });

            modelBuilder.Entity("WalletApi.Data.Entities.WalletUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("WalletUser", (string)null);
                });

            modelBuilder.Entity("WalletApi.Data.Entities.WalletAccount", b =>
                {
                    b.HasOne("WalletApi.Data.Entities.WalletUser", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WalletApi.Data.Entities.WalletTransaction", b =>
                {
                    b.HasOne("WalletApi.Data.Entities.WalletAccount", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WalletApi.Data.Entities.WalletUser", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WalletApi.Data.Entities.WalletAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("WalletApi.Data.Entities.WalletUser", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
