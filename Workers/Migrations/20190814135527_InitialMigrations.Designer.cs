﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workers.Models;

namespace Workers.Migrations
{
    [DbContext(typeof(AuthenticationContext))]
    [Migration("20190812152826_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Models.GlobalSetting", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("NameCompany");

                b.Property<int>("VacationDays");

                b.HasKey("Id");

                b.ToTable("GlobalSettings");
            });

            modelBuilder.Entity("DataAccessLayer.Models.HistoryAddingDays", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("CheckAddDays");

                b.Property<int>("NumberAddDays");

                b.Property<int>("Year");

                b.HasKey("Id");

                b.ToTable("HistoryAddDays");
            });

            modelBuilder.Entity("DataAccessLayer.Models.Person", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("Days");

                b.Property<string>("LastName");

                b.Property<string>("Name");

                b.Property<Guid?>("TeamId");

                b.Property<int>("Year");

                b.HasKey("Id");

                b.HasIndex("TeamId");

                b.ToTable("People");
            });

            modelBuilder.Entity("DataAccessLayer.Models.Team", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("MinNumberWorkers");

                b.Property<string>("TeamName");

                b.HasKey("Id");

                b.ToTable("Teams");
            });

            modelBuilder.Entity("DataAccessLayer.Models.User", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Email");

                b.Property<string>("Password");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("DataAccessLayer.Models.Vacation", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("Days");

                b.Property<DateTime>("FirstDate");

                b.Property<bool>("IndexDate");

                b.Property<Guid>("Peopleid");

                b.Property<DateTime>("SecontDate");

                b.Property<string>("TeamName");

                b.HasKey("Id");

                b.HasIndex("Peopleid");

                b.ToTable("Vacations");
            });

            modelBuilder.Entity("DataAccessLayer.Models.Weekend", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("EndDate");

                b.Property<string>("Name");

                b.Property<DateTime>("startDate");

                b.HasKey("Id");

                b.ToTable("HolyDays");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Name")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<string>("RoleId")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<string>("UserId")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider");

                b.Property<string>("ProviderKey");

                b.Property<string>("ProviderDisplayName");

                b.Property<string>("UserId")
                    .IsRequired();

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId");

                b.Property<string>("RoleId");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId");

                b.Property<string>("LoginProvider");

                b.Property<string>("Name");

                b.Property<string>("Value");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens");
            });

            modelBuilder.Entity("Workers.Models.UserAuthentication", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("AccessFailedCount");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Email")
                    .HasMaxLength(256);

                b.Property<bool>("EmailConfirmed");

                b.Property<bool>("LockoutEnabled");

                b.Property<DateTimeOffset?>("LockoutEnd");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256);

                b.Property<string>("PasswordHash");

                b.Property<string>("PhoneNumber");

                b.Property<bool>("PhoneNumberConfirmed");

                b.Property<string>("SecurityStamp");

                b.Property<bool>("TwoFactorEnabled");

                b.Property<string>("UserName")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers");
            });

            modelBuilder.Entity("DataAccessLayer.Models.Person", b =>
            {
                b.HasOne("DataAccessLayer.Models.Team", "Team")
                    .WithMany("Workers")
                    .HasForeignKey("TeamId");
            });

            modelBuilder.Entity("DataAccessLayer.Models.Vacation", b =>
            {
                b.HasOne("DataAccessLayer.Models.Person", "People")
                    .WithMany("HolyDays")
                    .HasForeignKey("Peopleid")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("Workers.Models.UserAuthentication")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("Workers.Models.UserAuthentication")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("Workers.Models.UserAuthentication")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("Workers.Models.UserAuthentication")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}