using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class initalcreate : Migration
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
                values: new object[] { "17fb7a6a-956f-4fd6-a469-6d55f072b39b", new DateTime(2019, 8, 12, 17, 7, 17, 650, DateTimeKind.Utc).AddTicks(7365), new DateTime(2019, 8, 12, 19, 7, 17, 648, DateTimeKind.Local).AddTicks(9448), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "18a0d2a9-d05f-46e2-8d44-1ea691b6da5c", new DateTime(2019, 8, 12, 17, 7, 17, 650, DateTimeKind.Utc).AddTicks(9780), new DateTime(2019, 8, 12, 19, 7, 17, 650, DateTimeKind.Local).AddTicks(9764), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "43802ebc-dd05-4183-9a62-92610ea426de", "Jack Sparrow", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(2823), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "3df4e52e-471d-45a7-b709-b16a6a5d6ea4", "Hektor Barbossa", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(4161), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "3ade1019-2157-4259-91eb-319343e9447e", new DateTime(2019, 8, 12, 17, 7, 17, 652, DateTimeKind.Utc).AddTicks(9529), new DateTime(1996, 8, 12, 19, 7, 17, 652, DateTimeKind.Local).AddTicks(9540), "example@mail.com", false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "662efe49-dfe3-4073-ab70-1f700532d311", new DateTime(2019, 8, 12, 17, 7, 17, 653, DateTimeKind.Utc).AddTicks(785), new DateTime(2015, 8, 12, 19, 7, 17, 653, DateTimeKind.Local).AddTicks(793), " example2@mail.uk", true, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "5c4a5b84-d67e-48c5-87b8-4b873ff60eb2", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(9029), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "137e452d-aa32-43d6-b4a8-c10b4ea03d5d", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(9038), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "04247bfb-f459-4489-9375-8f87aa7c3ecb", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(6925), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "e3e33c0a-0148-4d67-8fed-a7dc802979c9", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(8837), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "693eabb0-6a69-4424-8301-c01a10b1f467", 17f, new DateTime(2019, 8, 12, 17, 7, 17, 652, DateTimeKind.Utc).AddTicks(6067), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "f4d0f259-c9fb-4988-a7d3-eacf9747e3a1", new DateTime(2019, 8, 12, 17, 7, 17, 652, DateTimeKind.Utc).AddTicks(4337), 0, true, 30, new DateTime(2019, 8, 19, 19, 7, 17, 652, DateTimeKind.Local).AddTicks(4346), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "3d6c3c27-dbaa-4b84-880f-0e422cb1de58", new DateTime(2019, 8, 12, 17, 7, 17, 652, DateTimeKind.Utc).AddTicks(5360), 0, true, 25, new DateTime(2019, 8, 14, 19, 7, 17, 652, DateTimeKind.Local).AddTicks(5369), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "4a629635-c23f-47dc-9181-825aec4b320c", new DateTime(2019, 8, 12, 17, 7, 17, 652, DateTimeKind.Utc).AddTicks(2397), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "1b242ddb-c7ae-455c-a6d6-a7ff40ce8776", new DateTime(2019, 8, 12, 17, 7, 17, 652, DateTimeKind.Utc).AddTicks(1262), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "2ee14071-5b87-4c86-8b84-bfd7eb3ebe97", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(9996), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "9b974eb7-4922-4438-8709-129db5e353f2", new DateTime(2019, 8, 12, 17, 7, 17, 652, DateTimeKind.Utc).AddTicks(1277), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "cf75b711-5f93-4847-8713-99fa955f93c9", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(5231), null, null, null, 0f, new DateTime(2019, 8, 12, 19, 7, 17, 651, DateTimeKind.Local).AddTicks(5222), null, "879a7abe-9330-43f4-97ee-53a9b4371256", null, new DateTime(2019, 8, 19, 19, 7, 17, 651, DateTimeKind.Local).AddTicks(5671) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "a9302528-4257-4abf-b817-5069b43017d8", new DateTime(2019, 8, 12, 17, 7, 17, 651, DateTimeKind.Utc).AddTicks(6148), null, null, null, 0f, new DateTime(2019, 8, 12, 19, 7, 17, 651, DateTimeKind.Local).AddTicks(6141), null, "d8f4a395-25b4-46da-9d48-d922ea05adbf", null, new DateTime(2019, 9, 2, 19, 7, 17, 651, DateTimeKind.Local).AddTicks(6160) });

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
