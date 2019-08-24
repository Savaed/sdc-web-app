using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class removedunuseddataannotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "TicketUniqueId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddres",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChild",
                table: "Customers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "ActivityLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ActivityLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "44c77553-ad5d-4329-9cde-669aa7a88c2b", new DateTime(2019, 8, 24, 15, 4, 48, 792, DateTimeKind.Utc).AddTicks(4430), new DateTime(2019, 8, 24, 17, 4, 48, 790, DateTimeKind.Local).AddTicks(9096), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "e25a065d-b0c2-4c8a-a6d3-e1a657dfb3a3", new DateTime(2019, 8, 24, 15, 4, 48, 792, DateTimeKind.Utc).AddTicks(6433), new DateTime(2019, 8, 24, 17, 4, 48, 792, DateTimeKind.Local).AddTicks(6420), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "7f3d32bd-e799-4dfb-aed2-1c4f878d119e", "Jack Sparrow", new DateTime(2019, 8, 24, 15, 4, 48, 792, DateTimeKind.Utc).AddTicks(9170), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "f14af503-5313-4c10-b4ed-b298fac55059", "Hektor Barbossa", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(1318), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "d67c8d00-58bc-4276-9f46-cde1c51bae67", new DateTime(2019, 8, 24, 15, 4, 48, 794, DateTimeKind.Utc).AddTicks(5510), new DateTime(1996, 8, 24, 17, 4, 48, 794, DateTimeKind.Local).AddTicks(5521), "example@mail.com", false, false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "9e3ef2d0-22ed-4125-9271-ae7f4b391daa", new DateTime(2019, 8, 24, 15, 4, 48, 794, DateTimeKind.Utc).AddTicks(6880), new DateTime(2015, 8, 24, 17, 4, 48, 794, DateTimeKind.Local).AddTicks(6890), " example2@mail.uk", true, false, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "f6b139f1-d7e3-4c3a-b801-0a845884fbf6", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(6896), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "ccd3a29c-2cbc-4257-a081-11446a9c12a8", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(6900), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "5acc73ce-43da-446b-83d3-701fb45f4bb0", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(4450), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "336d792f-bb67-4e97-abda-bc534878daeb", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(6878), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "a65773f3-78c4-4dc4-99d6-e80c8ea8453f", 17f, new DateTime(2019, 8, 24, 15, 4, 48, 794, DateTimeKind.Utc).AddTicks(2353), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "397f9ca4-8b7f-4244-8310-a2e97f92698f", new DateTime(2019, 8, 24, 15, 4, 48, 794, DateTimeKind.Utc).AddTicks(745), 0, true, 30, new DateTime(2019, 8, 31, 17, 4, 48, 794, DateTimeKind.Local).AddTicks(754), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "cc603275-aad3-4176-9205-c45d8ef5b3f4", new DateTime(2019, 8, 24, 15, 4, 48, 794, DateTimeKind.Utc).AddTicks(1624), 0, true, 25, new DateTime(2019, 8, 26, 17, 4, 48, 794, DateTimeKind.Local).AddTicks(1632), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "6863320d-2088-4887-8237-47ba8f0b4871", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(9352), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "73a34b5e-24ac-4616-84e6-76ae82b98192", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(8612), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "1b078c42-e4b1-4fc5-8fbf-364c653168af", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(7746), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "eb483868-4396-46ac-bf0e-40fdf4c24af3", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(8631), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "98546bde-7b30-4180-8908-065dddadecc0", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(2518), null, null, null, 0f, new DateTime(2019, 8, 24, 17, 4, 48, 793, DateTimeKind.Local).AddTicks(2509), null, "0f25da8a-693b-4ab9-a775-588b6e97d3d5", null, new DateTime(2019, 8, 31, 17, 4, 48, 793, DateTimeKind.Local).AddTicks(2992) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "10d14727-9929-453d-b78b-1c5441fd1602", new DateTime(2019, 8, 24, 15, 4, 48, 793, DateTimeKind.Utc).AddTicks(3484), null, null, null, 0f, new DateTime(2019, 8, 24, 17, 4, 48, 793, DateTimeKind.Local).AddTicks(3477), null, "648a00e4-d4e1-4bbb-bb39-339d3e45d5ed", null, new DateTime(2019, 9, 14, 17, 4, 48, 793, DateTimeKind.Local).AddTicks(3498) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "44c77553-ad5d-4329-9cde-669aa7a88c2b");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "e25a065d-b0c2-4c8a-a6d3-e1a657dfb3a3");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "7f3d32bd-e799-4dfb-aed2-1c4f878d119e");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "f14af503-5313-4c10-b4ed-b298fac55059");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "9e3ef2d0-22ed-4125-9271-ae7f4b391daa");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "d67c8d00-58bc-4276-9f46-cde1c51bae67");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "336d792f-bb67-4e97-abda-bc534878daeb");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "5acc73ce-43da-446b-83d3-701fb45f4bb0");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "ccd3a29c-2cbc-4257-a081-11446a9c12a8");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "f6b139f1-d7e3-4c3a-b801-0a845884fbf6");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "a65773f3-78c4-4dc4-99d6-e80c8ea8453f");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "397f9ca4-8b7f-4244-8310-a2e97f92698f");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "cc603275-aad3-4176-9205-c45d8ef5b3f4");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "6863320d-2088-4887-8237-47ba8f0b4871");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "1b078c42-e4b1-4fc5-8fbf-364c653168af");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "73a34b5e-24ac-4616-84e6-76ae82b98192");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "eb483868-4396-46ac-bf0e-40fdf4c24af3");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "10d14727-9929-453d-b78b-1c5441fd1602");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "98546bde-7b30-4180-8908-065dddadecc0");

            migrationBuilder.DropColumn(
                name: "IsChild",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "TicketUniqueId",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddres",
                table: "Customers",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "ActivityLogs",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ActivityLogs",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
