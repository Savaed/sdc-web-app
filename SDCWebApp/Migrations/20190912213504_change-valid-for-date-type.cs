using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SDCWebApp.Migrations
{
    public partial class changevalidfordatetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "c9607e07-72c4-433d-9110-4b86a04b5045");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "e86e27a8-a872-45c5-a24c-5e4fb016e773");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "52fbb816-db08-400f-b83e-7b66d2695499");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "fe9d7790-fcc5-4886-8c46-c97043c54e47");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "ab4f01df-6b51-4bb3-aeaa-396c5c5c344b");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "f566d545-8398-4711-9382-755cb7628f31");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "32e72c12-eb90-45af-a89d-db8bf9264704");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "592c7016-a06e-41b8-8915-4116123613cf");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "5ba207c7-c70f-4f55-82c8-f391f4888256");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "91e38b65-a0db-4908-89f2-406461a00d25");

            migrationBuilder.DeleteData(
                table: "VisitInfo",
                keyColumn: "Id",
                keyValue: "c55b5340-b93e-4587-90e8-73d9b0a9551c");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "523dc3d9-6c38-404a-9387-d63aa0d0292d");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "c9f1cce9-5b2d-4fbe-9133-ce4b431a64df");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "462d42d8-ef35-405c-9971-e9fcd52c9f41");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "51dd0622-441d-4c89-b78a-33ff6642bccb");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "961a0acb-f3be-4ea8-ba90-96c7399a6774");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "a6a099a2-f50c-4f12-89d5-9d96e276eed1");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "b51745a2-b079-448a-bfcf-df57b8e9ac0d");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "dcffc971-9bbd-4e28-8126-c72b3e35ecd0");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "f4983307-88bf-45ee-b341-e8b86829d8e2");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "138d9a22-bc2e-47af-925f-d677bff5dfb5");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "3d30bc21-74a6-451a-a049-3a82bda9f881");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "89a2e913-a666-4daa-80db-d7fa21b33386");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "c930a405-1f1c-4233-ad50-1075d21e01f4");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "1bcc5bcc-1cf8-4a2c-83ec-a7e5516d3116");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "55c0e91f-091c-495c-b0cd-974687c53a48");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFor",
                table: "Tickets",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "06014815-c95b-4992-ab05-aa4cb14f0593", new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(3078), new DateTime(2019, 9, 12, 23, 35, 4, 27, DateTimeKind.Local).AddTicks(7932), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "d79c5f47-5a02-41f1-b975-0f22e186d8fc", new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(5106), new DateTime(2019, 9, 12, 23, 35, 4, 29, DateTimeKind.Local).AddTicks(5096), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "6f478851-52d1-447e-8424-6a78ef5adeb2", "Jack Sparrow", new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(6881), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "3993f17c-d961-4452-9fb8-3af81aaca171", "Hektor Barbossa", new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(8311), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "d93bb5d6-241a-4327-8a76-01789d6e5989", new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(4415), new DateTime(1996, 9, 12, 23, 35, 4, 32, DateTimeKind.Local).AddTicks(4426), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "cabd5d06-c92b-40d2-8a37-5715398c0232", new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(5858), new DateTime(2015, 9, 12, 23, 35, 4, 32, DateTimeKind.Local).AddTicks(5868), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "e6fb9652-a23f-4056-92d9-ce6ad63e8eb5", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(1197), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "db78c049-4449-4ebd-b031-d82172ed91d1", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3104), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "a3def270-db7e-4c65-bbd6-1fb5cd30096a", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3123), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "4ad1fe6d-f7e3-4b52-9b38-fbede70a655b", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3131), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VisitInfo",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "af003505-5043-4886-9230-f19cdbae0d15", new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(1183), "TL;DR", 35, 5, 4, 2f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "21319f4f-3129-4ec4-97a4-ca40fea2b14e", new DateTime(2019, 9, 12, 21, 35, 4, 32, DateTimeKind.Utc).AddTicks(450), 0, true, 25, new DateTime(2019, 9, 14, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "a4d0c83b-b3a7-4347-940b-d1f0d57acf78", new DateTime(2019, 9, 12, 21, 35, 4, 31, DateTimeKind.Utc).AddTicks(9408), 0, true, 30, new DateTime(2019, 9, 19, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "f63045bf-5002-44eb-9303-e4e7460a613d", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7091), "Sunday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "7bedb60a-7825-4b89-ad30-a0ddede6a8b8", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7075), "Saturday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "4ef947af-8862-4bba-9b27-6bceaae9186f", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(6820), "Tuesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "cf81ea35-e134-401e-8ee6-f6cf5a82ea49", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7029), "Thursday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "855f7152-af13-430b-964d-b6ca92e1cc90", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7006), "Wednesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "4cc41fa0-1b4c-4c23-a16e-6c23f3b051e4", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(3317), "Monday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "57b349a0-6db0-4647-9dda-6b71339ada7e", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 21, 35, 4, 27, DateTimeKind.Utc).AddTicks(7055), "Friday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "cdc767bf-92fd-4a66-85cf-53c0f06b6bd6", new DateTime(2019, 9, 12, 21, 35, 4, 31, DateTimeKind.Utc).AddTicks(7898), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "49a5114a-b593-455d-a208-8c448620fcc8", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(4855), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "cea5395d-8e4d-4170-9cf8-89931b8098b3", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(3967), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "b9ec4068-a047-4277-a0dd-1d3d8459e40b", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(4870), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "32ce8324-2591-4b53-a711-4986ce0f89ef", new DateTime(2019, 9, 12, 21, 35, 4, 29, DateTimeKind.Utc).AddTicks(9404), null, null, null, "efe3df1d-54f5-4ac1-8784-212bbeeb5e7c", 0f, new DateTime(2019, 9, 12, 23, 35, 4, 29, DateTimeKind.Local).AddTicks(9394), null, "dd7e7dad-cfc7-4787-890b-0383a29c772d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "656229ee-f800-4388-aba3-aaf45bd633d5", new DateTime(2019, 9, 12, 21, 35, 4, 30, DateTimeKind.Utc).AddTicks(328), null, null, null, "6c30c07e-5281-43ca-9b20-9686b80b654c", 0f, new DateTime(2019, 9, 12, 23, 35, 4, 30, DateTimeKind.Local).AddTicks(320), null, "6531006b-6eb3-46c2-b066-62f7a3e29731", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "06014815-c95b-4992-ab05-aa4cb14f0593");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "d79c5f47-5a02-41f1-b975-0f22e186d8fc");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "3993f17c-d961-4452-9fb8-3af81aaca171");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "6f478851-52d1-447e-8424-6a78ef5adeb2");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "cabd5d06-c92b-40d2-8a37-5715398c0232");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "d93bb5d6-241a-4327-8a76-01789d6e5989");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "4ad1fe6d-f7e3-4b52-9b38-fbede70a655b");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "a3def270-db7e-4c65-bbd6-1fb5cd30096a");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "db78c049-4449-4ebd-b031-d82172ed91d1");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "e6fb9652-a23f-4056-92d9-ce6ad63e8eb5");

            migrationBuilder.DeleteData(
                table: "VisitInfo",
                keyColumn: "Id",
                keyValue: "af003505-5043-4886-9230-f19cdbae0d15");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "21319f4f-3129-4ec4-97a4-ca40fea2b14e");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "a4d0c83b-b3a7-4347-940b-d1f0d57acf78");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "4cc41fa0-1b4c-4c23-a16e-6c23f3b051e4");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "4ef947af-8862-4bba-9b27-6bceaae9186f");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "57b349a0-6db0-4647-9dda-6b71339ada7e");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "7bedb60a-7825-4b89-ad30-a0ddede6a8b8");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "855f7152-af13-430b-964d-b6ca92e1cc90");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "cf81ea35-e134-401e-8ee6-f6cf5a82ea49");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "f63045bf-5002-44eb-9303-e4e7460a613d");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "cdc767bf-92fd-4a66-85cf-53c0f06b6bd6");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "49a5114a-b593-455d-a208-8c448620fcc8");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "b9ec4068-a047-4277-a0dd-1d3d8459e40b");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "cea5395d-8e4d-4170-9cf8-89931b8098b3");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "32ce8324-2591-4b53-a711-4986ce0f89ef");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "656229ee-f800-4388-aba3-aaf45bd633d5");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFor",
                table: "Tickets",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "e86e27a8-a872-45c5-a24c-5e4fb016e773", new DateTime(2019, 9, 12, 9, 54, 56, 174, DateTimeKind.Utc).AddTicks(1127), new DateTime(2019, 9, 12, 11, 54, 56, 172, DateTimeKind.Local).AddTicks(6208), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "c9607e07-72c4-433d-9110-4b86a04b5045", new DateTime(2019, 9, 12, 9, 54, 56, 174, DateTimeKind.Utc).AddTicks(2819), new DateTime(2019, 9, 12, 11, 54, 56, 174, DateTimeKind.Local).AddTicks(2809), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "fe9d7790-fcc5-4886-8c46-c97043c54e47", "Jack Sparrow", new DateTime(2019, 9, 12, 9, 54, 56, 174, DateTimeKind.Utc).AddTicks(4322), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "52fbb816-db08-400f-b83e-7b66d2695499", "Hektor Barbossa", new DateTime(2019, 9, 12, 9, 54, 56, 174, DateTimeKind.Utc).AddTicks(5701), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "ab4f01df-6b51-4bb3-aeaa-396c5c5c344b", new DateTime(2019, 9, 12, 9, 54, 56, 177, DateTimeKind.Utc).AddTicks(638), new DateTime(1996, 9, 12, 11, 54, 56, 177, DateTimeKind.Local).AddTicks(650), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "f566d545-8398-4711-9382-755cb7628f31", new DateTime(2019, 9, 12, 9, 54, 56, 177, DateTimeKind.Utc).AddTicks(2050), new DateTime(2015, 9, 12, 11, 54, 56, 177, DateTimeKind.Local).AddTicks(2059), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "32e72c12-eb90-45af-a89d-db8bf9264704", new DateTime(2019, 9, 12, 9, 54, 56, 174, DateTimeKind.Utc).AddTicks(8802), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "91e38b65-a0db-4908-89f2-406461a00d25", new DateTime(2019, 9, 12, 9, 54, 56, 175, DateTimeKind.Utc).AddTicks(498), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "5ba207c7-c70f-4f55-82c8-f391f4888256", new DateTime(2019, 9, 12, 9, 54, 56, 175, DateTimeKind.Utc).AddTicks(516), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "592c7016-a06e-41b8-8915-4116123613cf", new DateTime(2019, 9, 12, 9, 54, 56, 175, DateTimeKind.Utc).AddTicks(524), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VisitInfo",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "c55b5340-b93e-4587-90e8-73d9b0a9551c", new DateTime(2019, 9, 12, 9, 54, 56, 176, DateTimeKind.Utc).AddTicks(7419), "TL;DR", 35, 5, 4, 2f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "523dc3d9-6c38-404a-9387-d63aa0d0292d", new DateTime(2019, 9, 12, 9, 54, 56, 176, DateTimeKind.Utc).AddTicks(6712), 0, true, 25, new DateTime(2019, 9, 14, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "c9f1cce9-5b2d-4fbe-9133-ce4b431a64df", new DateTime(2019, 9, 12, 9, 54, 56, 176, DateTimeKind.Utc).AddTicks(5704), 0, true, 30, new DateTime(2019, 9, 19, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "f4983307-88bf-45ee-b341-e8b86829d8e2", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 12, 9, 54, 56, 172, DateTimeKind.Utc).AddTicks(5494), "Sunday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "a6a099a2-f50c-4f12-89d5-9d96e276eed1", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 12, 9, 54, 56, 172, DateTimeKind.Utc).AddTicks(5480), "Saturday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "b51745a2-b079-448a-bfcf-df57b8e9ac0d", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 9, 54, 56, 172, DateTimeKind.Utc).AddTicks(5207), "Tuesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "961a0acb-f3be-4ea8-ba90-96c7399a6774", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 9, 54, 56, 172, DateTimeKind.Utc).AddTicks(5439), "Thursday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "51dd0622-441d-4c89-b78a-33ff6642bccb", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 9, 54, 56, 172, DateTimeKind.Utc).AddTicks(5419), "Wednesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "dcffc971-9bbd-4e28-8126-c72b3e35ecd0", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 9, 54, 56, 172, DateTimeKind.Utc).AddTicks(1854), "Monday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "462d42d8-ef35-405c-9971-e9fcd52c9f41", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 12, 9, 54, 56, 172, DateTimeKind.Utc).AddTicks(5462), "Friday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "138d9a22-bc2e-47af-925f-d677bff5dfb5", new DateTime(2019, 9, 12, 9, 54, 56, 176, DateTimeKind.Utc).AddTicks(4135), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "89a2e913-a666-4daa-80db-d7fa21b33386", new DateTime(2019, 9, 12, 9, 54, 56, 175, DateTimeKind.Utc).AddTicks(2229), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "3d30bc21-74a6-451a-a049-3a82bda9f881", new DateTime(2019, 9, 12, 9, 54, 56, 175, DateTimeKind.Utc).AddTicks(1385), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "c930a405-1f1c-4233-ad50-1075d21e01f4", new DateTime(2019, 9, 12, 9, 54, 56, 175, DateTimeKind.Utc).AddTicks(2243), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "55c0e91f-091c-495c-b0cd-974687c53a48", new DateTime(2019, 9, 12, 9, 54, 56, 174, DateTimeKind.Utc).AddTicks(6646), null, null, null, "be70eaa4-76e5-4a58-ad56-18a98393912b", 0f, new DateTime(2019, 9, 12, 11, 54, 56, 174, DateTimeKind.Local).AddTicks(6637), null, "f391c745-30f2-4d8c-92b0-6d835d8c9452", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 19, 11, 54, 56, 174, DateTimeKind.Local).AddTicks(7105) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "1bcc5bcc-1cf8-4a2c-83ec-a7e5516d3116", new DateTime(2019, 9, 12, 9, 54, 56, 174, DateTimeKind.Utc).AddTicks(7995), null, null, null, "6aa222e0-0242-42e1-93b2-32aa99a71dc8", 0f, new DateTime(2019, 9, 12, 11, 54, 56, 174, DateTimeKind.Local).AddTicks(7986), null, "94febddc-7aa8-4ef1-ad9e-150ad5b12e9e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 3, 11, 54, 56, 174, DateTimeKind.Local).AddTicks(8011) });
        }
    }
}
