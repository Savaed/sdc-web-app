using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class initialcreate : Migration
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
                    User = table.Column<string>(maxLength: 20, nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true)
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
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    HasFamilyCard = table.Column<bool>(nullable: false),
                    EmailAddres = table.Column<string>(maxLength: 30, nullable: true)
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
                name: "GeneralSightseeingInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaxChildAge = table.Column<int>(nullable: false),
                    MaxAllowedGroupSize = table.Column<int>(nullable: false),
                    OpeningHour = table.Column<float>(nullable: false),
                    ClosingHour = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSightseeingInfo", x => x.Id);
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
                name: "SightseeingTariffs",
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
                    table.PrimaryKey("PK_SightseeingTariffs", x => x.Id);
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
                    SightseeingTariffId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTariffs_SightseeingTariffs_SightseeingTariffId",
                        column: x => x.SightseeingTariffId,
                        principalTable: "SightseeingTariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    TicketUniqueId = table.Column<string>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ValidFor = table.Column<DateTime>(type: "date", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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
                values: new object[] { "80cacb5b-aa99-44aa-8f96-409b2b3ff55d", new DateTime(2019, 8, 12, 14, 9, 28, 973, DateTimeKind.Utc).AddTicks(7676), new DateTime(2019, 8, 12, 16, 9, 28, 972, DateTimeKind.Local).AddTicks(2095), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "7491bf87-6337-491d-90ac-aad7e2c6f868", new DateTime(2019, 8, 12, 14, 9, 28, 973, DateTimeKind.Utc).AddTicks(9724), new DateTime(2019, 8, 12, 16, 9, 28, 973, DateTimeKind.Local).AddTicks(9711), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "ef1a25f4-8118-4f4b-bc22-7dd07bffccb2", "Jack Sparrow", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(2365), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "a10b933e-f4f0-4b64-b090-ff56ceb11572", "Hektor Barbossa", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(3848), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "b2703e11-4c73-4f46-990c-e7d12822db0c", new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(7214), new DateTime(1996, 8, 12, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(7224), "example@mail.com", false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "e59fb471-9e65-4aa0-91c3-d89a04a41a96", new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(8591), new DateTime(2015, 8, 12, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(8600), " example2@mail.uk", true, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "e02aff62-b27a-4ed8-bd63-90c0532e1c82", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(8778), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "f08af5ac-c199-4bbb-ae55-c3db5a251956", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(8787), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "6f1d4082-b187-4ce0-b7c9-0756fb4c0ddd", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(6986), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "75ed6464-d709-4e39-ac8b-0033b9ef7502", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(8760), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "098e18fe-eb3e-4993-8609-31681141ba78", 17f, new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(3968), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "8b5d3f0a-ea7e-484a-8078-3df4c60c26b9", new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(2353), 0, true, 30, new DateTime(2019, 8, 19, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(2362), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "6f1e412c-aa2d-4c00-b5ea-8eb8a66694bd", new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(3259), 0, true, 25, new DateTime(2019, 8, 14, 16, 9, 28, 975, DateTimeKind.Local).AddTicks(3268), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "f2ab5cbd-280c-4e82-9ea5-60e3e6a690f8", new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(1219), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "990c1977-72c4-4310-92c2-5b08a1d2b302", new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(490), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "f519f87a-8a12-4d9a-9d52-691a4f78a745", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(9606), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "27707966-5b7f-44d8-a7e8-1849d843e609", new DateTime(2019, 8, 12, 14, 9, 28, 975, DateTimeKind.Utc).AddTicks(504), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "8a322294-4fe7-416a-b6e7-b76e787af1b6", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(5172), null, null, null, 0f, new DateTime(2019, 8, 12, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(5163), null, "c598bf66-9911-473a-ac4d-926d63fcde97", null, new DateTime(2019, 8, 19, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(5632) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "cd58fa69-fc64-443a-9f7c-e10069b9222a", new DateTime(2019, 8, 12, 14, 9, 28, 974, DateTimeKind.Utc).AddTicks(6133), null, null, null, 0f, new DateTime(2019, 8, 12, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(6125), null, "26508b81-dfc7-4e77-be1b-b8c6e7308108", null, new DateTime(2019, 9, 2, 16, 9, 28, 974, DateTimeKind.Local).AddTicks(6192) });

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
                name: "IX_TicketTariffs_SightseeingTariffId",
                table: "TicketTariffs",
                column: "SightseeingTariffId");
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
                name: "GeneralSightseeingInfo");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "TicketTariffs");

            migrationBuilder.DropTable(
                name: "SightseeingTariffs");
        }
    }
}
