﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SDCWebApp.Data;

namespace SDCWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190912213504_change-valid-for-date-type")]
    partial class changevalidfordatetype
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
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

            modelBuilder.Entity("SDCWebApp.Models.ActivityLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Description");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("ActivityLogs");

                    b.HasData(
                        new
                        {
                            Id = "06014815-c95b-4992-ab05-aa4cb14f0593",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(3078),
                            Date = new DateTime(2019, 9, 12, 23, 35, 4, 27, DateTimeKind.Local).AddTicks(7932),
                            Description = "Attempt to steal the Dead Man's Chest",
                            Type = "LogIn",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            User = "jacks"
                        },
                        new
                        {
                            Id = "d79c5f47-5a02-41f1-b975-0f22e186d8fc",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(5106),
                            Date = new DateTime(2019, 9, 12, 23, 35, 4, 29, DateTimeKind.Local).AddTicks(5096),
                            Description = "Revolting on a black pearl",
                            Type = "LogOut",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            User = "hektorb"
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Article", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = "6f478851-52d1-447e-8424-6a78ef5adeb2",
                            Author = "Jack Sparrow",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(6881),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The bast pirate i'v every seen",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "3993f17c-d961-4452-9fb8-3af81aaca171",
                            Author = "Hektor Barbossa",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(8311),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The captain of Black Pearl",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("EmailAddress");

                    b.Property<bool>("HasFamilyCard");

                    b.Property<bool>("IsChild");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = "d93bb5d6-241a-4327-8a76-01789d6e5989",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(4415),
                            DateOfBirth = new DateTime(1996, 9, 12, 23, 35, 4, 32, DateTimeKind.Local).AddTicks(4426),
                            EmailAddress = "example@mail.com",
                            HasFamilyCard = false,
                            IsChild = false,
                            IsDisabled = false,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "cabd5d06-c92b-40d2-8a37-5715398c0232",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(5858),
                            DateOfBirth = new DateTime(2015, 9, 12, 23, 35, 4, 32, DateTimeKind.Local).AddTicks(5868),
                            EmailAddress = " example2@mail.uk",
                            HasFamilyCard = true,
                            IsChild = false,
                            IsDisabled = false,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Discount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("DiscountValueInPercentage");

                    b.Property<int?>("GroupSizeForDiscount");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            Id = "e6fb9652-a23f-4056-92d9-ce6ad63e8eb5",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(1197),
                            Description = "Discount for groups",
                            DiscountValueInPercentage = 15,
                            GroupSizeForDiscount = 20,
                            Type = "ForGroup",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "db78c049-4449-4ebd-b031-d82172ed91d1",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3104),
                            Description = "Discount for people with Family Card",
                            DiscountValueInPercentage = 15,
                            Type = "ForFamily",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "a3def270-db7e-4c65-bbd6-1fb5cd30096a",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3123),
                            Description = "Discount for disabled people.",
                            DiscountValueInPercentage = 50,
                            Type = "ForDisabled",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "4ad1fe6d-f7e3-4b52-9b38-fbede70a655b",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3131),
                            Description = "Discount only for kids under specific age.",
                            DiscountValueInPercentage = 100,
                            Type = "ForChild",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.GeneralSightseeingInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("MaxAllowedGroupSize");

                    b.Property<int>("MaxChildAge");

                    b.Property<int>("MaxTicketOrderInterval");

                    b.Property<float>("SightseeingDuration");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("GeneralSightseeingInfo");

                    b.HasData(
                        new
                        {
                            Id = "af003505-5043-4886-9230-f19cdbae0d15",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(1183),
                            Description = "TL;DR",
                            MaxAllowedGroupSize = 35,
                            MaxChildAge = 5,
                            MaxTicketOrderInterval = 4,
                            SightseeingDuration = 2f,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.OpeningHours", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("ClosingHour");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("DayOfWeek")
                        .IsRequired();

                    b.Property<string>("InfoId");

                    b.Property<TimeSpan>("OpeningHour");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("InfoId");

                    b.ToTable("OpeningDates");

                    b.HasData(
                        new
                        {
                            Id = "4cc41fa0-1b4c-4c23-a16e-6c23f3b051e4",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(3317),
                            DayOfWeek = "Monday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "4ef947af-8862-4bba-9b27-6bceaae9186f",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(6820),
                            DayOfWeek = "Tuesday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "855f7152-af13-430b-964d-b6ca92e1cc90",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7006),
                            DayOfWeek = "Wednesday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "cf81ea35-e134-401e-8ee6-f6cf5a82ea49",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7029),
                            DayOfWeek = "Thursday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "57b349a0-6db0-4647-9dda-6b71339ada7e",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7055),
                            DayOfWeek = "Friday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "7bedb60a-7825-4b89-ad30-a0ddede6a8b8",
                            ClosingHour = new TimeSpan(0, 16, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7075),
                            DayOfWeek = "Saturday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "f63045bf-5002-44eb-9303-e4e7460a613d",
                            ClosingHour = new TimeSpan(0, 16, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7091),
                            DayOfWeek = "Sunday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("ExpiryIn");

                    b.Property<string>("Token");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SDCWebApp.Models.SightseeingGroup", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CurrentGroupSize");

                    b.Property<bool>("IsAvailablePlace");

                    b.Property<int>("MaxGroupSize");

                    b.Property<DateTime>("SightseeingDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = "a4d0c83b-b3a7-4347-940b-d1f0d57acf78",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 31, DateTimeKind.Utc).AddTicks(9408),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 30,
                            SightseeingDate = new DateTime(2019, 9, 19, 12, 0, 0, 0, DateTimeKind.Local),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "21319f4f-3129-4ec4-97a4-ca40fea2b14e",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(450),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 25,
                            SightseeingDate = new DateTime(2019, 9, 14, 16, 0, 0, 0, DateTimeKind.Local),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.SightseeingTariff", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("SightseeingTariffs");

                    b.HasData(
                        new
                        {
                            Id = "cdc767bf-92fd-4a66-85cf-53c0f06b6bd6",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 31, DateTimeKind.Utc).AddTicks(7898),
                            Name = "BasicTickets",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CustomerId");

                    b.Property<string>("DiscountId");

                    b.Property<string>("GroupId");

                    b.Property<string>("OrderStamp");

                    b.Property<float>("Price");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("TariffId");

                    b.Property<string>("TicketUniqueId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<DateTime>("ValidFor")
                        .HasColumnType("datetime2(0)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("GroupId");

                    b.HasIndex("TariffId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = "32ce8324-2591-4b53-a711-4986ce0f89ef",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(9404),
                            OrderStamp = "efe3df1d-54f5-4ac1-8784-212bbeeb5e7c",
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 12, 23, 35, 4, 29, DateTimeKind.Local).AddTicks(9394),
                            TicketUniqueId = "dd7e7dad-cfc7-4787-890b-0383a29c772d",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "656229ee-f800-4388-aba3-aaf45bd633d5",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(328),
                            OrderStamp = "6c30c07e-5281-43ca-9b20-9686b80b654c",
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 12, 23, 35, 4, 30, DateTimeKind.Local).AddTicks(320),
                            TicketUniqueId = "6531006b-6eb3-46c2-b066-62f7a3e29731",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.TicketTariff", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<float>("DefaultPrice");

                    b.Property<string>("Description");

                    b.Property<bool>("IsPerHour");

                    b.Property<bool>("IsPerPerson");

                    b.Property<string>("SightseeingTariffId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("SightseeingTariffId");

                    b.ToTable("TicketTariffs");

                    b.HasData(
                        new
                        {
                            Id = "cea5395d-8e4d-4170-9cf8-89931b8098b3",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3967),
                            DefaultPrice = 28f,
                            Description = "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "49a5114a-b593-455d-a208-8c448620fcc8",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(4855),
                            DefaultPrice = 22f,
                            Description = "Centrum Dziedzictwa Szkła.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "b9ec4068-a047-4277-a0dd-1d3d8459e40b",
                            CreatedAt = new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(4870),
                            DefaultPrice = 10f,
                            Description = "Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SDCWebApp.Models.OpeningHours", b =>
                {
                    b.HasOne("SDCWebApp.Models.GeneralSightseeingInfo", "Info")
                        .WithMany("OpeningHours")
                        .HasForeignKey("InfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SDCWebApp.Models.Ticket", b =>
                {
                    b.HasOne("SDCWebApp.Models.Customer", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId");

                    b.HasOne("SDCWebApp.Models.Discount", "Discount")
                        .WithMany("Tickets")
                        .HasForeignKey("DiscountId");

                    b.HasOne("SDCWebApp.Models.SightseeingGroup", "Group")
                        .WithMany("Tickets")
                        .HasForeignKey("GroupId");

                    b.HasOne("SDCWebApp.Models.TicketTariff", "Tariff")
                        .WithMany("Tickets")
                        .HasForeignKey("TariffId");
                });

            modelBuilder.Entity("SDCWebApp.Models.TicketTariff", b =>
                {
                    b.HasOne("SDCWebApp.Models.SightseeingTariff", "SightseeingTariff")
                        .WithMany("TicketTariffs")
                        .HasForeignKey("SightseeingTariffId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
