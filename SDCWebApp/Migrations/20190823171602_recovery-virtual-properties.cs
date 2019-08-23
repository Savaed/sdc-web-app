using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class recoveryvirtualproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "549fa6af-e583-4da8-b3c9-d1a39cb7af07");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "b9e77b9e-53cd-472a-9034-83557cd1fe9c");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "17161955-ad0d-4072-9726-91b6eea0cc6f");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "2110b068-b9b2-495f-a134-3a9e219addde");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "2ba66fdd-3264-484e-9aab-443a97e270fd");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "46bbd320-d9df-4bd8-b7f7-dcc138c95a92");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "141f5ed1-b962-4932-ab55-2e4d206b959a");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "897afdce-a0b7-413b-923c-27c4729f59c9");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "93ed0d63-0af2-4e8a-8c92-e995ef5a5fed");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "96954325-2794-4aa1-940b-1a83c046d605");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "82c2c985-d78c-493b-be4e-ecde1c0d27e4");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "2dcd4f70-5d2a-448b-8071-b80a88b4c1af");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "5362c4be-115d-4722-88ec-9a703f745d34");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "249b2e48-20f0-4e48-af44-72e40931c823");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "a8a871fb-dcc4-43b4-b94e-03bcf86222bd");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "b628d710-2ddf-41ba-a1b0-4e9f9f9437f9");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "e04d7b09-7065-43cf-88f2-78b58dee1d2c");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "71f30c14-a253-4b7c-91f1-4b8035f867ae");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "af29a489-032a-40bd-9686-4f1d64855b5d");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "d1cc5247-3734-494c-8a0b-67c2b5b2b470", new DateTime(2019, 8, 23, 17, 16, 1, 786, DateTimeKind.Utc).AddTicks(578), new DateTime(2019, 8, 23, 19, 16, 1, 784, DateTimeKind.Local).AddTicks(5316), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "6e297aa8-27c7-4a43-a84f-151f8b5a6b71", new DateTime(2019, 8, 23, 17, 16, 1, 786, DateTimeKind.Utc).AddTicks(2605), new DateTime(2019, 8, 23, 19, 16, 1, 786, DateTimeKind.Local).AddTicks(2589), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "d18c197d-7519-4b82-aaf7-60d28d06458c", "Jack Sparrow", new DateTime(2019, 8, 23, 17, 16, 1, 786, DateTimeKind.Utc).AddTicks(5296), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "bb9ac566-55c3-453b-85fb-aa4bf3891344", "Hektor Barbossa", new DateTime(2019, 8, 23, 17, 16, 1, 786, DateTimeKind.Utc).AddTicks(6634), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "19f12d3e-16c0-4088-8917-62034182db12", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(9825), new DateTime(1996, 8, 23, 19, 16, 1, 787, DateTimeKind.Local).AddTicks(9839), "example@mail.com", false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "90b78c28-33ee-4bd8-983c-d332a5c7874f", new DateTime(2019, 8, 23, 17, 16, 1, 788, DateTimeKind.Utc).AddTicks(1170), new DateTime(2015, 8, 23, 19, 16, 1, 788, DateTimeKind.Local).AddTicks(1179), " example2@mail.uk", true, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "e5988bb0-6cf5-4efb-b2cf-b2f8853779e8", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(1431), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "b429b16c-4651-4c02-80a6-0b2442b31e90", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(1435), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "fda5901c-a1e2-40bd-aeff-6f15bcf15569", new DateTime(2019, 8, 23, 17, 16, 1, 786, DateTimeKind.Utc).AddTicks(9733), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "3b851a43-9bfa-423a-94ff-3a3ad88a3854", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(1414), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "f222322c-b620-4e62-ae49-eabc3440a686", 17f, new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(6412), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "76863083-c495-491b-96f8-eb39f87bac96", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(4825), 0, true, 30, new DateTime(2019, 8, 30, 19, 16, 1, 787, DateTimeKind.Local).AddTicks(4834), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "f754d052-2193-4c85-ab20-aba3341b369b", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(5690), 0, true, 25, new DateTime(2019, 8, 25, 19, 16, 1, 787, DateTimeKind.Local).AddTicks(5699), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "c2d05de2-4215-4856-a032-683e18f11405", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(3764), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "5b10b6ce-678d-450c-ba80-4221d7db8439", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(3056), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "8b816fd7-41eb-4b01-8339-dd905066dc77", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(2224), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "74959c56-a049-4e80-8bab-0ee485e03fe1", new DateTime(2019, 8, 23, 17, 16, 1, 787, DateTimeKind.Utc).AddTicks(3071), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "06dcad8a-1b38-4968-bd75-0a64140672e2", new DateTime(2019, 8, 23, 17, 16, 1, 786, DateTimeKind.Utc).AddTicks(8047), null, null, null, 0f, new DateTime(2019, 8, 23, 19, 16, 1, 786, DateTimeKind.Local).AddTicks(8039), null, "c909b11f-6f81-4252-bda1-dd7b424957c9", null, new DateTime(2019, 8, 30, 19, 16, 1, 786, DateTimeKind.Local).AddTicks(8489) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "9a9a0282-d223-4451-89c3-ac32b1ec13e8", new DateTime(2019, 8, 23, 17, 16, 1, 786, DateTimeKind.Utc).AddTicks(8964), null, null, null, 0f, new DateTime(2019, 8, 23, 19, 16, 1, 786, DateTimeKind.Local).AddTicks(8957), null, "a8864179-1db5-487d-afca-2854b6d06795", null, new DateTime(2019, 9, 13, 19, 16, 1, 786, DateTimeKind.Local).AddTicks(8978) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "6e297aa8-27c7-4a43-a84f-151f8b5a6b71");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "d1cc5247-3734-494c-8a0b-67c2b5b2b470");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "bb9ac566-55c3-453b-85fb-aa4bf3891344");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "d18c197d-7519-4b82-aaf7-60d28d06458c");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "19f12d3e-16c0-4088-8917-62034182db12");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "90b78c28-33ee-4bd8-983c-d332a5c7874f");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "3b851a43-9bfa-423a-94ff-3a3ad88a3854");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "b429b16c-4651-4c02-80a6-0b2442b31e90");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "e5988bb0-6cf5-4efb-b2cf-b2f8853779e8");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "fda5901c-a1e2-40bd-aeff-6f15bcf15569");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "f222322c-b620-4e62-ae49-eabc3440a686");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "76863083-c495-491b-96f8-eb39f87bac96");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "f754d052-2193-4c85-ab20-aba3341b369b");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "c2d05de2-4215-4856-a032-683e18f11405");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "5b10b6ce-678d-450c-ba80-4221d7db8439");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "74959c56-a049-4e80-8bab-0ee485e03fe1");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "8b816fd7-41eb-4b01-8339-dd905066dc77");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "06dcad8a-1b38-4968-bd75-0a64140672e2");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "9a9a0282-d223-4451-89c3-ac32b1ec13e8");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "549fa6af-e583-4da8-b3c9-d1a39cb7af07", new DateTime(2019, 8, 21, 15, 35, 34, 110, DateTimeKind.Utc).AddTicks(2851), new DateTime(2019, 8, 21, 17, 35, 34, 108, DateTimeKind.Local).AddTicks(6648), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "b9e77b9e-53cd-472a-9034-83557cd1fe9c", new DateTime(2019, 8, 21, 15, 35, 34, 110, DateTimeKind.Utc).AddTicks(4895), new DateTime(2019, 8, 21, 17, 35, 34, 110, DateTimeKind.Local).AddTicks(4881), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "17161955-ad0d-4072-9726-91b6eea0cc6f", "Jack Sparrow", new DateTime(2019, 8, 21, 15, 35, 34, 110, DateTimeKind.Utc).AddTicks(7867), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "2110b068-b9b2-495f-a134-3a9e219addde", "Hektor Barbossa", new DateTime(2019, 8, 21, 15, 35, 34, 110, DateTimeKind.Utc).AddTicks(9234), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "2ba66fdd-3264-484e-9aab-443a97e270fd", new DateTime(2019, 8, 21, 15, 35, 34, 112, DateTimeKind.Utc).AddTicks(3947), new DateTime(1996, 8, 21, 17, 35, 34, 112, DateTimeKind.Local).AddTicks(3962), "example@mail.com", false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsDisabled", "UpdatedAt" },
                values: new object[] { "46bbd320-d9df-4bd8-b7f7-dcc138c95a92", new DateTime(2019, 8, 21, 15, 35, 34, 112, DateTimeKind.Utc).AddTicks(5302), new DateTime(2015, 8, 21, 17, 35, 34, 112, DateTimeKind.Local).AddTicks(5317), " example2@mail.uk", true, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "96954325-2794-4aa1-940b-1a83c046d605", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(4674), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "93ed0d63-0af2-4e8a-8c92-e995ef5a5fed", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(4677), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "141f5ed1-b962-4932-ab55-2e4d206b959a", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(2924), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "897afdce-a0b7-413b-923c-27c4729f59c9", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(4650), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "82c2c985-d78c-493b-be4e-ecde1c0d27e4", 17f, new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(9787), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "2dcd4f70-5d2a-448b-8071-b80a88b4c1af", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(8196), 0, true, 30, new DateTime(2019, 8, 28, 17, 35, 34, 111, DateTimeKind.Local).AddTicks(8205), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "5362c4be-115d-4722-88ec-9a703f745d34", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(9069), 0, true, 25, new DateTime(2019, 8, 23, 17, 35, 34, 111, DateTimeKind.Local).AddTicks(9083), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "249b2e48-20f0-4e48-af44-72e40931c823", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(7053), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "e04d7b09-7065-43cf-88f2-78b58dee1d2c", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(6334), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "a8a871fb-dcc4-43b4-b94e-03bcf86222bd", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(5482), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "b628d710-2ddf-41ba-a1b0-4e9f9f9437f9", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(6348), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "af29a489-032a-40bd-9686-4f1d64855b5d", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(509), null, null, null, 0f, new DateTime(2019, 8, 21, 17, 35, 34, 111, DateTimeKind.Local).AddTicks(500), null, "a711d28a-5d7b-4609-a77d-19dc5b2fd3dc", null, new DateTime(2019, 8, 28, 17, 35, 34, 111, DateTimeKind.Local).AddTicks(1238) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "71f30c14-a253-4b7c-91f1-4b8035f867ae", new DateTime(2019, 8, 21, 15, 35, 34, 111, DateTimeKind.Utc).AddTicks(2137), null, null, null, 0f, new DateTime(2019, 8, 21, 17, 35, 34, 111, DateTimeKind.Local).AddTicks(2130), null, "c73e1142-e0e3-41a3-b053-03c284ae1a9f", null, new DateTime(2019, 9, 11, 17, 35, 34, 111, DateTimeKind.Local).AddTicks(2151) });
        }
    }
}
