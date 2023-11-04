﻿// <auto-generated />
using System;
using Login.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Login.Migrations
{
    [DbContext(typeof(LoginContext))]
    [Migration("20231104082300_sheet")]
    partial class sheet
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("Login.Models.User.FileSheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FileId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FileSheets");
                });

            modelBuilder.Entity("Login.Models.User.Sheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FileSheetId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SheetId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SheetName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FileSheetId");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("Login.Models.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Login.Models.User.UserLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "IsDeleted")
                        .IsUnique();

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Login.Models.User.FileSheet", b =>
                {
                    b.HasOne("Login.Models.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Login.Models.User.Sheet", b =>
                {
                    b.HasOne("Login.Models.User.FileSheet", null)
                        .WithMany("SheetNames")
                        .HasForeignKey("FileSheetId");
                });

            modelBuilder.Entity("Login.Models.User.UserLogin", b =>
                {
                    b.HasOne("Login.Models.User.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Login.Models.User.FileSheet", b =>
                {
                    b.Navigation("SheetNames");
                });

            modelBuilder.Entity("Login.Models.User.User", b =>
                {
                    b.Navigation("UserLogins");
                });
#pragma warning restore 612, 618
        }
    }
}