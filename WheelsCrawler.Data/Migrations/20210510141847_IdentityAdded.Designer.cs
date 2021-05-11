﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Migrations
{
    [DbContext(typeof(WheelsCrawlerDbContext))]
    [Migration("20210510141847_IdentityAdded")]
    partial class IdentityAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Account.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Account.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Account.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarBrandId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarFuelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarGearboxId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("EngineСapacity")
                        .HasColumnType("REAL");

                    b.Property<int>("Kilometrage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PictureUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("Plate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RelatedQueryUrlId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.HasIndex("CarFuelId");

                    b.HasIndex("CarGearboxId");

                    b.HasIndex("CarModelId");

                    b.HasIndex("CarTypeId");

                    b.HasIndex("RelatedQueryUrlId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RiaName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("WheelsName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarBrands");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarFuel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RiaName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("WheelsName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarFuels");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarGearbox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RiaName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("WheelsName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarGearboxes");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarBrandId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RiaName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("WheelsName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RiaName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("WheelsName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Url", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UrlToScrape")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.Account.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Account.AppUserRole", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.Account.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WheelsCrawler.Data.Models.Account.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Car", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.CarBrand", "CarBrand")
                        .WithMany()
                        .HasForeignKey("CarBrandId");

                    b.HasOne("WheelsCrawler.Data.Models.CarFuel", "CarFuel")
                        .WithMany()
                        .HasForeignKey("CarFuelId");

                    b.HasOne("WheelsCrawler.Data.Models.CarGearbox", "CarGearbox")
                        .WithMany()
                        .HasForeignKey("CarGearboxId");

                    b.HasOne("WheelsCrawler.Data.Models.CarModel", "CarModel")
                        .WithMany()
                        .HasForeignKey("CarModelId");

                    b.HasOne("WheelsCrawler.Data.Models.CarType", "CarType")
                        .WithMany("Cars")
                        .HasForeignKey("CarTypeId");

                    b.HasOne("WheelsCrawler.Data.Models.Url", "RelatedQueryUrl")
                        .WithMany()
                        .HasForeignKey("RelatedQueryUrlId");

                    b.Navigation("CarBrand");

                    b.Navigation("CarFuel");

                    b.Navigation("CarGearbox");

                    b.Navigation("CarModel");

                    b.Navigation("CarType");

                    b.Navigation("RelatedQueryUrl");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarModel", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.CarBrand", "CarBrand")
                        .WithMany("CarModels")
                        .HasForeignKey("CarBrandId");

                    b.Navigation("CarBrand");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Url", b =>
                {
                    b.HasOne("WheelsCrawler.Data.Models.Account.User", null)
                        .WithMany("InterestedUrls")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Account.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.Account.User", b =>
                {
                    b.Navigation("InterestedUrls");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarBrand", b =>
                {
                    b.Navigation("CarModels");
                });

            modelBuilder.Entity("WheelsCrawler.Data.Models.CarType", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
