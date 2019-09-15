using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    User = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    IsChild = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    HasFamilyCard = table.Column<bool>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DiscountValueInPercentage = table.Column<int>(nullable: false),
                    GroupSizeForDiscount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    SightseeingDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    MaxGroupSize = table.Column<int>(nullable: false),
                    CurrentGroupSize = table.Column<int>(nullable: false),
                    IsAvailablePlace = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaxChildAge = table.Column<int>(nullable: false),
                    MaxAllowedGroupSize = table.Column<int>(nullable: false),
                    SightseeingDuration = table.Column<float>(nullable: false),
                    MaxTicketOrderInterval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    ExpiryIn = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitTariffs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitTariffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningDates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    OpeningHour = table.Column<TimeSpan>(nullable: false),
                    ClosingHour = table.Column<TimeSpan>(nullable: false),
                    DayOfWeek = table.Column<string>(nullable: false),
                    InfoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningDates_Info_InfoId",
                        column: x => x.InfoId,
                        principalTable: "Info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTariffs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsPerHour = table.Column<bool>(nullable: false),
                    IsPerPerson = table.Column<bool>(nullable: false),
                    DefaultPrice = table.Column<float>(nullable: false),
                    VisitTariffId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTariffs_VisitTariffs_VisitTariffId",
                        column: x => x.VisitTariffId,
                        principalTable: "VisitTariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    TicketUniqueId = table.Column<string>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ValidFor = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    OrderStamp = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    TariffId = table.Column<string>(nullable: true),
                    DiscountId = table.Column<string>(nullable: true),
                    GroupId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "TicketTariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "51b8da0a-70b0-4576-9e58-d6b16d9fbb5f", new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6793), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6793), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "d6c46a74-30d2-4ce1-a14d-3fcc223e262b", new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(8011), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(8011), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "d9355fbf-bbba-4a25-a19e-43abf3dad7a2", "Jack Sparrow", new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(8242), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "bc0c90a4-d49f-43cc-8289-f3ea3e826d24", "Hektor Barbossa", new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(9315), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "1", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(6579), new DateTime(1996, 9, 15, 17, 41, 1, 954, DateTimeKind.Local).AddTicks(6551), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "2", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(8070), new DateTime(2015, 9, 15, 17, 41, 1, 954, DateTimeKind.Local).AddTicks(8059), " example2@mail.uk", true, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "1", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(3431), "Discount for groups", 50, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "2", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(4400), "Discount for people with Family Card", 50, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "1", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(5416), 2, true, 30, new DateTime(2019, 9, 22, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "2", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(6322), 0, true, 30, new DateTime(2019, 9, 22, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Info",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "1", new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(2824), "TL;DR", 35, 5, 4, 2f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VisitTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "1", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(912), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "1", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(5375), "Monday", "1", new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "2", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6468), "Tuesday", "1", new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "3", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6479), "Wednesday", "1", new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "4", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6480), "Thursday", "1", new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "5", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6480), "Friday", "1", new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "6", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6481), "Saturday", "1", new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "7", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 15, 15, 41, 1, 952, DateTimeKind.Utc).AddTicks(6481), "Sunday", "1", new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "UpdatedAt", "VisitTariffId" },
                values: new object[] { "1", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(2204), 20f, "Ticket tariff 1", false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "UpdatedAt", "VisitTariffId" },
                values: new object[] { "2", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(3292), 20f, "Ticket tariff 2", false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "1", new DateTime(2019, 9, 15, 15, 41, 1, 954, DateTimeKind.Utc).AddTicks(9324), "1", "1", "1", "order-stamp-nr-1", 10f, new DateTime(2019, 9, 8, 17, 41, 1, 954, DateTimeKind.Local).AddTicks(9315), "1", "ticket-unique-id", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 22, 12, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "2", new DateTime(2019, 9, 15, 15, 41, 1, 955, DateTimeKind.Utc).AddTicks(571), "2", "2", "1", "order-stamp-nr-2", 10f, new DateTime(2019, 9, 8, 17, 41, 1, 955, DateTimeKind.Local).AddTicks(562), "1", "ticket-unique-id-nr-2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 22, 12, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningDates_InfoId",
                table: "OpeningDates",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerId",
                table: "Tickets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DiscountId",
                table: "Tickets",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_GroupId",
                table: "Tickets",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TariffId",
                table: "Tickets",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTariffs_VisitTariffId",
                table: "TicketTariffs",
                column: "VisitTariffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OpeningDates");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Info");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "TicketTariffs");

            migrationBuilder.DropTable(
                name: "VisitTariffs");
        }
    }
}
