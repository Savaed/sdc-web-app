using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class changedvirtualpropertiestonormal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "6c8ab515-6d03-48e8-ae25-b9ce5a7d873f");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "7f9e380d-3d28-4c72-b7ee-9b40ff1e5599");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "8159b3d6-8733-4e53-9490-2df6955bec83");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "cd7551c7-1f7c-4d43-b3ba-d57ed5e838e0");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "3489effd-c52a-456b-85b4-5c8e3e647e31");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "f52d5135-114d-4ddc-8a77-865a816658f9");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "39af87a1-38df-41ae-85c3-be0b31c9af4a");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "4b9136b9-ef45-4bd7-be2e-1b2ae6e9a9e0");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "d625b84a-4c3a-4996-ba04-188b1472d3ca");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "e9f29487-5030-4d54-885c-3651f70a2fed");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "bcda0b66-b2e9-4f35-ae3b-8eeaff1093ed");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "aeb3f012-e14b-438d-a773-4b87422e7a5b");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "d0f12f1d-ebaf-4c19-8422-c5d18bf9fdaf");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "8ac862d6-4e97-400f-9346-31a9a5850f8c");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "0e9502a0-3fee-4d2d-aacf-6c491e4891ca");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "681881e2-478e-401c-96eb-52eb1b8746e7");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "ab5e930d-ea46-4a99-af31-0b71aa240ff2");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "4501860a-6993-4f3a-8258-d7a313d45824");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "5ad05fc7-615a-4490-8d52-a135700868f2");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "5cb24d53-5a92-472a-9526-8046d8be0a15", new DateTime(2019, 8, 21, 15, 8, 46, 423, DateTimeKind.Utc).AddTicks(7824), new DateTime(2019, 8, 21, 17, 8, 46, 422, DateTimeKind.Local).AddTicks(160), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "6d474231-f138-46f5-a559-a5082e6c2bf8", new DateTime(2019, 8, 21, 15, 8, 46, 424, DateTimeKind.Utc).AddTicks(77), new DateTime(2019, 8, 21, 17, 8, 46, 424, DateTimeKind.Local).AddTicks(64), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "6dd88cfc-a3d1-424d-8546-8a95d8ac1062", "Jack Sparrow", new DateTime(2019, 8, 21, 15, 8, 46, 424, DateTimeKind.Utc).AddTicks(3600), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "e32bb710-0e4c-47cd-843b-4eba79cfea7c", "Hektor Barbossa", new DateTime(2019, 8, 21, 15, 8, 46, 424, DateTimeKind.Utc).AddTicks(5250), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "2259082b-2e15-4f5d-8c94-b6b5142b983a", new DateTime(2019, 8, 21, 15, 8, 46, 426, DateTimeKind.Utc).AddTicks(9), new DateTime(1996, 8, 21, 17, 8, 46, 426, DateTimeKind.Local).AddTicks(19), "example@mail.com", false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "a3c0811c-0efe-4d4f-950b-dd856460af2c", new DateTime(2019, 8, 21, 15, 8, 46, 426, DateTimeKind.Utc).AddTicks(1318), new DateTime(2015, 8, 21, 17, 8, 46, 426, DateTimeKind.Local).AddTicks(1327), " example2@mail.uk", true, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "341e8add-dc35-42fb-8c8d-c7050ef82869", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(461), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "2c063c20-6f3b-421e-b0fd-9f29cb51d2bd", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(465), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "08fccf25-733a-4274-bac7-c701928b1dd3", new DateTime(2019, 8, 21, 15, 8, 46, 424, DateTimeKind.Utc).AddTicks(8652), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "faf07001-a101-41ab-9fcb-998479d9bd99", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(443), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "d2ac907f-72e8-4ca3-8b2e-0d3d2b72e1bb", 17f, new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(6459), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "ca92ef45-3bfb-44cf-a42f-1209e7ad6e97", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(4820), 0, true, 30, new DateTime(2019, 8, 28, 17, 8, 46, 425, DateTimeKind.Local).AddTicks(4829), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "96db5290-024e-477e-a697-ed36c081608c", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(5730), 0, true, 25, new DateTime(2019, 8, 23, 17, 8, 46, 425, DateTimeKind.Local).AddTicks(5738), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "f2ff7fb2-daf1-4f13-b5b6-ac25d86a56f3", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(3344), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "8e5906e5-1fc0-4c1e-8fe3-f362a6898083", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(2175), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "eda947e9-b4d5-4518-a09a-55b8ea1b157a", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(1299), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "47c925b0-5281-434d-b333-58114850406b", new DateTime(2019, 8, 21, 15, 8, 46, 425, DateTimeKind.Utc).AddTicks(2190), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "b4410829-9f4e-48ab-8bf2-e36806de2fd8", new DateTime(2019, 8, 21, 15, 8, 46, 424, DateTimeKind.Utc).AddTicks(6548), null, null, null, 0f, new DateTime(2019, 8, 21, 17, 8, 46, 424, DateTimeKind.Local).AddTicks(6540), null, "a55b4a60-7d91-4dc0-a76a-7599d0ef4a0f", null, new DateTime(2019, 8, 28, 17, 8, 46, 424, DateTimeKind.Local).AddTicks(7012) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "5ae3456f-91ad-4eaf-9987-3b43a2400cc5", new DateTime(2019, 8, 21, 15, 8, 46, 424, DateTimeKind.Utc).AddTicks(7509), null, null, null, 0f, new DateTime(2019, 8, 21, 17, 8, 46, 424, DateTimeKind.Local).AddTicks(7501), null, "b76d9676-440f-41f7-b4d2-434581fe5b8e", null, new DateTime(2019, 9, 11, 17, 8, 46, 424, DateTimeKind.Local).AddTicks(7532) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "5cb24d53-5a92-472a-9526-8046d8be0a15");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "6d474231-f138-46f5-a559-a5082e6c2bf8");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "6dd88cfc-a3d1-424d-8546-8a95d8ac1062");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "e32bb710-0e4c-47cd-843b-4eba79cfea7c");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "2259082b-2e15-4f5d-8c94-b6b5142b983a");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "a3c0811c-0efe-4d4f-950b-dd856460af2c");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "08fccf25-733a-4274-bac7-c701928b1dd3");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "2c063c20-6f3b-421e-b0fd-9f29cb51d2bd");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "341e8add-dc35-42fb-8c8d-c7050ef82869");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "faf07001-a101-41ab-9fcb-998479d9bd99");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "d2ac907f-72e8-4ca3-8b2e-0d3d2b72e1bb");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "96db5290-024e-477e-a697-ed36c081608c");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "ca92ef45-3bfb-44cf-a42f-1209e7ad6e97");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "f2ff7fb2-daf1-4f13-b5b6-ac25d86a56f3");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "47c925b0-5281-434d-b333-58114850406b");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "8e5906e5-1fc0-4c1e-8fe3-f362a6898083");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "eda947e9-b4d5-4518-a09a-55b8ea1b157a");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "5ae3456f-91ad-4eaf-9987-3b43a2400cc5");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "b4410829-9f4e-48ab-8bf2-e36806de2fd8");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "7f9e380d-3d28-4c72-b7ee-9b40ff1e5599", new DateTime(2019, 8, 21, 14, 45, 40, 182, DateTimeKind.Utc).AddTicks(5820), new DateTime(2019, 8, 21, 16, 45, 40, 180, DateTimeKind.Local).AddTicks(9940), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "6c8ab515-6d03-48e8-ae25-b9ce5a7d873f", new DateTime(2019, 8, 21, 14, 45, 40, 182, DateTimeKind.Utc).AddTicks(7935), new DateTime(2019, 8, 21, 16, 45, 40, 182, DateTimeKind.Local).AddTicks(7919), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "8159b3d6-8733-4e53-9490-2df6955bec83", "Jack Sparrow", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(1040), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "cd7551c7-1f7c-4d43-b3ba-d57ed5e838e0", "Hektor Barbossa", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(2444), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "f52d5135-114d-4ddc-8a77-865a816658f9", new DateTime(2019, 8, 21, 14, 45, 40, 184, DateTimeKind.Utc).AddTicks(5709), new DateTime(1996, 8, 21, 16, 45, 40, 184, DateTimeKind.Local).AddTicks(5719), "example@mail.com", false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "3489effd-c52a-456b-85b4-5c8e3e647e31", new DateTime(2019, 8, 21, 14, 45, 40, 184, DateTimeKind.Utc).AddTicks(7039), new DateTime(2015, 8, 21, 16, 45, 40, 184, DateTimeKind.Local).AddTicks(7048), " example2@mail.uk", true, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "d625b84a-4c3a-4996-ba04-188b1472d3ca", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(7239), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "39af87a1-38df-41ae-85c3-be0b31c9af4a", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(7242), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "e9f29487-5030-4d54-885c-3651f70a2fed", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(5412), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "4b9136b9-ef45-4bd7-be2e-1b2ae6e9a9e0", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(7219), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "bcda0b66-b2e9-4f35-ae3b-8eeaff1093ed", 17f, new DateTime(2019, 8, 21, 14, 45, 40, 184, DateTimeKind.Utc).AddTicks(2713), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "aeb3f012-e14b-438d-a773-4b87422e7a5b", new DateTime(2019, 8, 21, 14, 45, 40, 184, DateTimeKind.Utc).AddTicks(1033), 0, true, 30, new DateTime(2019, 8, 28, 16, 45, 40, 184, DateTimeKind.Local).AddTicks(1042), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "d0f12f1d-ebaf-4c19-8422-c5d18bf9fdaf", new DateTime(2019, 8, 21, 14, 45, 40, 184, DateTimeKind.Utc).AddTicks(1981), 0, true, 25, new DateTime(2019, 8, 23, 16, 45, 40, 184, DateTimeKind.Local).AddTicks(1990), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "8ac862d6-4e97-400f-9346-31a9a5850f8c", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(9845), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "0e9502a0-3fee-4d2d-aacf-6c491e4891ca", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(9016), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "681881e2-478e-401c-96eb-52eb1b8746e7", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(8119), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "ab5e930d-ea46-4a99-af31-0b71aa240ff2", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(9031), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "5ad05fc7-615a-4490-8d52-a135700868f2", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(3606), null, null, null, 0f, new DateTime(2019, 8, 21, 16, 45, 40, 183, DateTimeKind.Local).AddTicks(3598), null, "62d3e6db-49da-4a3a-b58f-7ebe87228aa2", null, new DateTime(2019, 8, 28, 16, 45, 40, 183, DateTimeKind.Local).AddTicks(4080) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "4501860a-6993-4f3a-8258-d7a313d45824", new DateTime(2019, 8, 21, 14, 45, 40, 183, DateTimeKind.Utc).AddTicks(4594), null, null, null, 0f, new DateTime(2019, 8, 21, 16, 45, 40, 183, DateTimeKind.Local).AddTicks(4587), null, "1bc5cff7-0bfb-49ab-afe4-d5be80fa1e32", null, new DateTime(2019, 9, 11, 16, 45, 40, 183, DateTimeKind.Local).AddTicks(4616) });
        }
    }
}
