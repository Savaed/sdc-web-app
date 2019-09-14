using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SDCWebApp.Migrations
{
    public partial class redesignsightseeinginfotable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "ca6c3233-20bb-4d9b-86d7-898c502a3ff4");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "fa7b3054-c94d-4b1c-a987-9b2d2c7abbc8");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "c9f8aeff-cc94-4a7b-a8c2-55cb46d0363f");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "e28e05bd-221b-4f53-9df0-d236f0403557");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "47e39995-0b89-481b-a420-a648a22ea893");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "7199faf7-7678-4979-8976-efee55418248");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "0f900b49-1482-472c-af36-add758689d4e");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "740335e9-1b77-4399-b348-17c965b2cc11");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "8d533126-4609-47b5-885b-fcb3b85b296f");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "f87af06f-7949-4e63-a5cc-3cba058a18ad");

            migrationBuilder.DeleteData(
                table: "VisitInfo",
                keyColumn: "Id",
                keyValue: "23ded832-4d2e-4972-b5d2-d3fa673703f1");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "31636c00-1c99-4d56-8ef0-9b3b48fc2a54");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "be76f1e2-d99a-4f47-ac1c-39dd7ee7c396");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "10881837-df57-4498-9b88-8031997b2fca");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "13a90443-c641-46e4-aa9e-26eaa7371fbc");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "3bcd9759-18c6-4009-92d1-532c905f65c4");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "c72506a3-7f22-49ba-951d-b82c525f26f6");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "b98ab531-3b23-4956-9704-535b7efb35a7");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "d484384e-7467-4e4d-b605-673f00ad7b31");

            migrationBuilder.DropColumn(
                name: "ClosingHour",
                table: "VisitInfo");

            migrationBuilder.DropColumn(
                name: "OpeningHour",
                table: "VisitInfo");

            migrationBuilder.AddColumn<float>(
                name: "SightseeingDuration",
                table: "VisitInfo",
                nullable: false,
                defaultValue: 0f);

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
                        name: "FK_OpeningDates_VisitInfo_InfoId",
                        column: x => x.InfoId,
                        principalTable: "VisitInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "94fef728-1750-41f9-8605-4e8b66d8b0e7", new DateTime(2019, 9, 11, 10, 22, 32, 777, DateTimeKind.Utc).AddTicks(9526), new DateTime(2019, 9, 11, 12, 22, 32, 776, DateTimeKind.Local).AddTicks(5531), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "e067927c-f7df-4dd8-aa67-d35bd258b591", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(1272), new DateTime(2019, 9, 11, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(1263), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "551956a8-567c-44d1-a23c-e42604a01cab", "Jack Sparrow", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(3211), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "10826dff-1c1e-4912-9639-36cccb518df6", "Hektor Barbossa", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(4661), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "96085bd5-44d3-4418-88b6-89e2b88d4233", new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(6064), new DateTime(1996, 9, 11, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(6074), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "70a909a9-8c64-4825-a476-e927a1a231b5", new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(7420), new DateTime(2015, 9, 11, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(7430), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "f3911046-0b36-4cd6-a507-455e58ebcdb8", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(7309), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "0ce45b1d-1823-44be-a9c2-9ce809cc6f5b", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9040), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "1b649fe0-40cf-43a0-90da-059e4f553a03", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9057), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "96db8a74-22e7-4ea3-90c0-5977de9d6b4a", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9060), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VisitInfo",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "9b2b0143-df2d-408b-af1c-f61540b6d942", new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(3440), "TL;DR", 35, 5, 4, 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "e7aaa447-eb16-402b-ad1d-52debabae0ce", new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(1040), 0, true, 30, new DateTime(2019, 9, 18, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(1056), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "b17082e3-5878-4a16-957c-b5ada035dd67", new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(2375), 0, true, 25, new DateTime(2019, 9, 13, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(2385), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "8a39810a-05ea-4d7a-b190-5a2ddb0b7d42", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 11, 10, 22, 32, 776, DateTimeKind.Utc).AddTicks(608), "Sunday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "a2aece52-66c1-441d-ab99-ac0acb491fa8", new TimeSpan(0, 10, 0, 0, 0), new DateTime(2019, 9, 11, 10, 22, 32, 776, DateTimeKind.Utc).AddTicks(4093), "Saturday", null, new TimeSpan(0, 16, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "b3ec0dcd-2b97-4468-838a-315eff942d75", new TimeSpan(0, 10, 0, 0, 0), new DateTime(2019, 9, 11, 10, 22, 32, 776, DateTimeKind.Utc).AddTicks(4843), "Monday", null, new TimeSpan(0, 18, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "c37359d1-b232-4a21-b3f2-106690109a5a", new DateTime(2019, 9, 11, 10, 22, 32, 780, DateTimeKind.Utc).AddTicks(9632), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "c8ab0404-b65a-4019-b4aa-63066e4f6da0", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9845), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "9b8d4f66-fde8-41a9-8778-0c33e7f53f01", new DateTime(2019, 9, 11, 10, 22, 32, 779, DateTimeKind.Utc).AddTicks(687), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "a3b6ac26-36fc-4fcf-b1bf-d6489b0dcc5b", new DateTime(2019, 9, 11, 10, 22, 32, 779, DateTimeKind.Utc).AddTicks(702), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "2da03878-b7e7-49e7-a111-c1275384c454", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(5586), null, null, null, 0f, new DateTime(2019, 9, 11, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(5578), null, "c278362d-c0fc-4ba8-bfb9-d6fc33f40072", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 18, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(6036) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "4bd9cd2e-e48f-431e-bf92-03ccdd513c9a", new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(6516), null, null, null, 0f, new DateTime(2019, 9, 11, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(6510), null, "e6b8755c-49e2-4faf-9e14-2f7987df2c68", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 2, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(6531) });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningDates_InfoId",
                table: "OpeningDates",
                column: "InfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningDates");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "94fef728-1750-41f9-8605-4e8b66d8b0e7");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "e067927c-f7df-4dd8-aa67-d35bd258b591");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "10826dff-1c1e-4912-9639-36cccb518df6");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "551956a8-567c-44d1-a23c-e42604a01cab");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "70a909a9-8c64-4825-a476-e927a1a231b5");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "96085bd5-44d3-4418-88b6-89e2b88d4233");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "0ce45b1d-1823-44be-a9c2-9ce809cc6f5b");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "1b649fe0-40cf-43a0-90da-059e4f553a03");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "96db8a74-22e7-4ea3-90c0-5977de9d6b4a");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "f3911046-0b36-4cd6-a507-455e58ebcdb8");

            migrationBuilder.DeleteData(
                table: "VisitInfo",
                keyColumn: "Id",
                keyValue: "9b2b0143-df2d-408b-af1c-f61540b6d942");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "b17082e3-5878-4a16-957c-b5ada035dd67");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "e7aaa447-eb16-402b-ad1d-52debabae0ce");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "c37359d1-b232-4a21-b3f2-106690109a5a");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "9b8d4f66-fde8-41a9-8778-0c33e7f53f01");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "a3b6ac26-36fc-4fcf-b1bf-d6489b0dcc5b");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "c8ab0404-b65a-4019-b4aa-63066e4f6da0");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "2da03878-b7e7-49e7-a111-c1275384c454");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "4bd9cd2e-e48f-431e-bf92-03ccdd513c9a");

            migrationBuilder.DropColumn(
                name: "SightseeingDuration",
                table: "VisitInfo");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ClosingHour",
                table: "VisitInfo",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningHour",
                table: "VisitInfo",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "fa7b3054-c94d-4b1c-a987-9b2d2c7abbc8", new DateTime(2019, 9, 7, 19, 14, 28, 256, DateTimeKind.Utc).AddTicks(5430), new DateTime(2019, 9, 7, 21, 14, 28, 254, DateTimeKind.Local).AddTicks(8445), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "ca6c3233-20bb-4d9b-86d7-898c502a3ff4", new DateTime(2019, 9, 7, 19, 14, 28, 256, DateTimeKind.Utc).AddTicks(7969), new DateTime(2019, 9, 7, 21, 14, 28, 256, DateTimeKind.Local).AddTicks(7953), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "e28e05bd-221b-4f53-9df0-d236f0403557", "Jack Sparrow", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(1767), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "c9f8aeff-cc94-4a7b-a8c2-55cb46d0363f", "Hektor Barbossa", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(3576), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "47e39995-0b89-481b-a420-a648a22ea893", new DateTime(2019, 9, 7, 19, 14, 28, 261, DateTimeKind.Utc).AddTicks(5744), new DateTime(1996, 9, 7, 21, 14, 28, 261, DateTimeKind.Local).AddTicks(5755), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "7199faf7-7678-4979-8976-efee55418248", new DateTime(2019, 9, 7, 19, 14, 28, 261, DateTimeKind.Utc).AddTicks(7608), new DateTime(2015, 9, 7, 21, 14, 28, 261, DateTimeKind.Local).AddTicks(7618), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "740335e9-1b77-4399-b348-17c965b2cc11", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(9533), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "8d533126-4609-47b5-885b-fcb3b85b296f", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(9537), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "0f900b49-1482-472c-af36-add758689d4e", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(7493), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "f87af06f-7949-4e63-a5cc-3cba058a18ad", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(9515), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VisitInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "OpeningHour", "UpdatedAt" },
                values: new object[] { "23ded832-4d2e-4972-b5d2-d3fa673703f1", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 7, 19, 14, 28, 261, DateTimeKind.Utc).AddTicks(809), "TL;DR", 35, 5, 4, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "31636c00-1c99-4d56-8ef0-9b3b48fc2a54", new DateTime(2019, 9, 7, 19, 14, 28, 260, DateTimeKind.Utc).AddTicks(8698), 0, true, 30, new DateTime(2019, 9, 14, 21, 14, 28, 260, DateTimeKind.Local).AddTicks(8711), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "be76f1e2-d99a-4f47-ac1c-39dd7ee7c396", new DateTime(2019, 9, 7, 19, 14, 28, 260, DateTimeKind.Utc).AddTicks(9743), 0, true, 25, new DateTime(2019, 9, 9, 21, 14, 28, 260, DateTimeKind.Local).AddTicks(9753), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "10881837-df57-4498-9b88-8031997b2fca", new DateTime(2019, 9, 7, 19, 14, 28, 260, DateTimeKind.Utc).AddTicks(6803), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "3bcd9759-18c6-4009-92d1-532c905f65c4", new DateTime(2019, 9, 7, 19, 14, 28, 258, DateTimeKind.Utc).AddTicks(1601), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "c72506a3-7f22-49ba-951d-b82c525f26f6", new DateTime(2019, 9, 7, 19, 14, 28, 258, DateTimeKind.Utc).AddTicks(739), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "13a90443-c641-46e4-aa9e-26eaa7371fbc", new DateTime(2019, 9, 7, 19, 14, 28, 258, DateTimeKind.Utc).AddTicks(1666), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "b98ab531-3b23-4956-9704-535b7efb35a7", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(5124), null, null, null, 0f, new DateTime(2019, 9, 7, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(5115), null, "387759b2-b8ca-4012-840a-7174efcef96a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 14, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(5725) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "d484384e-7467-4e4d-b605-673f00ad7b31", new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(6220), null, null, null, 0f, new DateTime(2019, 9, 7, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(6213), null, "e1b844a8-6b7a-4c3e-83e2-4ce8c4fc85bd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 28, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(6247) });
        }
    }
}
