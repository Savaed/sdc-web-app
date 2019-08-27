using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class minorpropertieschanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "2438f835-8eed-4ffd-9abf-50e6c9589c7d", new DateTime(2019, 8, 26, 16, 25, 54, 217, DateTimeKind.Utc).AddTicks(5120), new DateTime(2019, 8, 26, 18, 25, 54, 215, DateTimeKind.Local).AddTicks(8353), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "0339e903-e183-4ffd-aa03-92ee86f0b943", new DateTime(2019, 8, 26, 16, 25, 54, 217, DateTimeKind.Utc).AddTicks(7121), new DateTime(2019, 8, 26, 18, 25, 54, 217, DateTimeKind.Local).AddTicks(7109), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "4e7b8e39-cdea-404e-aa96-7c6e12c29f17", "Jack Sparrow", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(35), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "431f0c09-e50b-4327-817b-01394f580729", "Hektor Barbossa", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(1469), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "f1b7a9b1-1a0a-4ce5-a457-a6f040e33c0b", new DateTime(2019, 8, 26, 16, 25, 54, 219, DateTimeKind.Utc).AddTicks(4511), new DateTime(1996, 8, 26, 18, 25, 54, 219, DateTimeKind.Local).AddTicks(4521), "example@mail.com", false, false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "2e32b5f4-7c86-4b4c-8a81-6726740868cc", new DateTime(2019, 8, 26, 16, 25, 54, 219, DateTimeKind.Utc).AddTicks(5852), new DateTime(2015, 8, 26, 18, 25, 54, 219, DateTimeKind.Local).AddTicks(5860), " example2@mail.uk", true, false, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "d2206955-5039-43cc-a898-633f2ddf97d8", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(5929), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "4700b5ae-9f22-4ada-a2e5-9b1d598992b5", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(5931), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "022bfc17-952f-480e-82ff-be42fbe7f423", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(4228), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "01409cfd-6b73-45d0-bbec-ff2a76f52642", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(5910), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "6fe8d018-3ca8-4cfb-a078-b74bccb276a1", 17f, new DateTime(2019, 8, 26, 16, 25, 54, 219, DateTimeKind.Utc).AddTicks(1465), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "845c8b4d-1a2a-4760-801e-4d79053abeba", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(9325), 0, true, 30, new DateTime(2019, 9, 2, 18, 25, 54, 218, DateTimeKind.Local).AddTicks(9333), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "ce27497c-0120-4635-b8b1-3e8ddcccaead", new DateTime(2019, 8, 26, 16, 25, 54, 219, DateTimeKind.Utc).AddTicks(635), 0, true, 25, new DateTime(2019, 8, 28, 18, 25, 54, 219, DateTimeKind.Local).AddTicks(644), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "b0ef481e-fe72-4c62-b7db-5fe458dbf65f", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(8260), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "490de703-e268-40f4-a593-1d2c01b34b66", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(7548), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "2a7bfbe9-1e82-4d9c-89e1-675677608876", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(6719), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "a8d99c95-23a9-4c9c-838b-65eec9004904", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(7566), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "c45bab17-f72a-41b1-a1aa-4e62b380553d", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(2464), null, null, null, 0f, new DateTime(2019, 8, 26, 18, 25, 54, 218, DateTimeKind.Local).AddTicks(2454), null, "111e9b8d-47ad-4cbc-988f-d05714d971cb", null, new DateTime(2019, 9, 2, 18, 25, 54, 218, DateTimeKind.Local).AddTicks(2961) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "dd88d2f5-7384-456c-b071-15da62cfd016", new DateTime(2019, 8, 26, 16, 25, 54, 218, DateTimeKind.Utc).AddTicks(3435), null, null, null, 0f, new DateTime(2019, 8, 26, 18, 25, 54, 218, DateTimeKind.Local).AddTicks(3427), null, "23e836e8-064d-489a-869c-aece68efa52a", null, new DateTime(2019, 9, 16, 18, 25, 54, 218, DateTimeKind.Local).AddTicks(3449) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "0339e903-e183-4ffd-aa03-92ee86f0b943");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "2438f835-8eed-4ffd-9abf-50e6c9589c7d");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "431f0c09-e50b-4327-817b-01394f580729");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "4e7b8e39-cdea-404e-aa96-7c6e12c29f17");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "2e32b5f4-7c86-4b4c-8a81-6726740868cc");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "f1b7a9b1-1a0a-4ce5-a457-a6f040e33c0b");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "01409cfd-6b73-45d0-bbec-ff2a76f52642");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "022bfc17-952f-480e-82ff-be42fbe7f423");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "4700b5ae-9f22-4ada-a2e5-9b1d598992b5");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "d2206955-5039-43cc-a898-633f2ddf97d8");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "6fe8d018-3ca8-4cfb-a078-b74bccb276a1");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "845c8b4d-1a2a-4760-801e-4d79053abeba");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "ce27497c-0120-4635-b8b1-3e8ddcccaead");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "b0ef481e-fe72-4c62-b7db-5fe458dbf65f");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "2a7bfbe9-1e82-4d9c-89e1-675677608876");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "490de703-e268-40f4-a593-1d2c01b34b66");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "a8d99c95-23a9-4c9c-838b-65eec9004904");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "c45bab17-f72a-41b1-a1aa-4e62b380553d");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "dd88d2f5-7384-456c-b071-15da62cfd016");

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
    }
}
