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
    [Migration("20190812140929_initial-create")]
    partial class initialcreate
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

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("ActivityLogs");

                    b.HasData(
                        new
                        {
                            Id = "80cacb5b-aa99-44aa-8f96-409b2b3ff55d",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 973, DateTimeKind.Utc).AddTicks(7676),
                            Date = new DateTime(2019, 8, 12, 16, 9, 28, 972, DateTimeKind.Local).AddTicks(2095),
                            Description = "Attempt to steal the Dead Man's Chest",
                            Type = "LogIn",
                            User = "jacks"
                        },
                        new
                        {
                            Id = "7491bf87-6337-491d-90ac-aad7e2c6f868",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 973, DateTimeKind.Utc).AddTicks(9724),
                            Date = new DateTime(2019, 8, 12, 16, 9, 28, 973, DateTimeKind.Local).AddTicks(9711),
                            Description = "Revolting on a black pearl",
                            Type = "LogOut",
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
                            Id = "ef1a25f4-8118-4f4b-bc22-7dd07bffccb2",
                            Author = "Jack Sparrow",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(2365),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The bast pirate i'v every seen"
                        },
                        new
                        {
                            Id = "a10b933e-f4f0-4b64-b090-ff56ceb11572",
                            Author = "Hektor Barbossa",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(3848),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The captain of Black Pearl"
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

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("EmailAddres")
                        .HasMaxLength(30);

                    b.Property<bool>("HasFamilyCard");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = "b2703e11-4c73-4f46-990c-e7d12822db0c",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(7214),
                            DateOfBirth = new DateTime(1996, 8, 12, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(7224),
                            EmailAddres = "example@mail.com",
                            HasFamilyCard = false,
                            IsDisabled = false
                        },
                        new
                        {
                            Id = "e59fb471-9e65-4aa0-91c3-d89a04a41a96",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(8591),
                            DateOfBirth = new DateTime(2015, 8, 12, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(8600),
                            EmailAddres = " example2@mail.uk",
                            HasFamilyCard = true,
                            IsDisabled = false
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
                            Id = "6f1d4082-b187-4ce0-b7c9-0756fb4c0ddd",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(6986),
                            Description = "Discount for groups",
                            DiscountValueInPercentage = 15,
                            GroupSizeForDiscount = 20,
                            Type = "ForGroup"
                        },
                        new
                        {
                            Id = "75ed6464-d709-4e39-ac8b-0033b9ef7502",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(8760),
                            Description = "Discount for people with Family Card",
                            DiscountValueInPercentage = 15,
                            Type = "ForFamily"
                        },
                        new
                        {
                            Id = "e02aff62-b27a-4ed8-bd63-90c0532e1c82",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(8778),
                            Description = "Discount for disabled people.",
                            DiscountValueInPercentage = 50,
                            Type = "ForDisabled"
                        },
                        new
                        {
                            Id = "f08af5ac-c199-4bbb-ae55-c3db5a251956",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(8787),
                            Description = "Discount only for kids under specific age.",
                            DiscountValueInPercentage = 100,
                            Type = "ForChild"
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.GeneralSightseeingInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("ClosingHour");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("MaxAllowedGroupSize");

                    b.Property<int>("MaxChildAge");

                    b.Property<float>("OpeningHour");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("GeneralSightseeingInfo");

                    b.HasData(
                        new
                        {
                            Id = "098e18fe-eb3e-4993-8609-31681141ba78",
                            ClosingHour = 17f,
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(3968),
                            Description = "TL;DR",
                            MaxAllowedGroupSize = 35,
                            MaxChildAge = 5,
                            OpeningHour = 9f
                        });
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
                            Id = "8b5d3f0a-ea7e-484a-8078-3df4c60c26b9",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(2353),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 30,
                            SightseeingDate = new DateTime(2019, 8, 19, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(2362)
                        },
                        new
                        {
                            Id = "6f1e412c-aa2d-4c00-b5ea-8eb8a66694bd",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(3259),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 25,
                            SightseeingDate = new DateTime(2019, 8, 14, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(3268)
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
                            Id = "f2ab5cbd-280c-4e82-9ea5-60e3e6a690f8",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(1219),
                            Name = "BasicTickets"
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

                    b.Property<float>("Price");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("TariffId");

                    b.Property<string>("TicketUniqueId")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<DateTime>("ValidFor")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("GroupId");

                    b.HasIndex("TariffId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = "8a322294-4fe7-416a-b6e7-b76e787af1b6",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(5172),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 8, 12, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(5163),
                            TicketUniqueId = "c598bf66-9911-473a-ac4d-926d63fcde97",
                            ValidFor = new DateTime(2019, 8, 19, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(5632)
                        },
                        new
                        {
                            Id = "cd58fa69-fc64-443a-9f7c-e10069b9222a",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(6133),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 8, 12, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(6125),
                            TicketUniqueId = "26508b81-dfc7-4e77-be1b-b8c6e7308108",
                            ValidFor = new DateTime(2019, 9, 2, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(6192)
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
                            Id = "f519f87a-8a12-4d9a-9d52-691a4f78a745",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(9606),
                            DefaultPrice = 28f,
                            Description = "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true
                        },
                        new
                        {
                            Id = "990c1977-72c4-4310-92c2-5b08a1d2b302",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(490),
                            DefaultPrice = 22f,
                            Description = "Centrum Dziedzictwa Szkła.",
                            IsPerHour = false,
                            IsPerPerson = true
                        },
                        new
                        {
                            Id = "27707966-5b7f-44d8-a7e8-1849d843e609",
                            CreatedAt = new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(504),
                            DefaultPrice = 10f,
                            Description = "Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true
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
                        .HasForeignKey("SightseeingTariffId");
                });
#pragma warning restore 612, 618
        }
    }
}
