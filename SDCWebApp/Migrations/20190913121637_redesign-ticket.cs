using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SDCWebApp.Migrations
{
    public partial class redesignticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Groups_GroupId",
                table: "Tickets");

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

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "7f5f7c4a-cda6-43b3-8ad4-aafb02b59ac3", new DateTime(2019, 9, 13, 12, 16, 36, 661, DateTimeKind.Utc).AddTicks(3244), new DateTime(2019, 9, 13, 14, 16, 36, 659, DateTimeKind.Local).AddTicks(9250), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "f6bee357-9b12-43eb-9dd0-4e24af90ef7c", new DateTime(2019, 9, 13, 12, 16, 36, 661, DateTimeKind.Utc).AddTicks(4926), new DateTime(2019, 9, 13, 14, 16, 36, 661, DateTimeKind.Local).AddTicks(4917), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "667aefba-2b74-42a7-a1b4-e09e2a382e59", "Jack Sparrow", new DateTime(2019, 9, 13, 12, 16, 36, 661, DateTimeKind.Utc).AddTicks(6388), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "8f0608d3-636e-46bc-aab4-970fb3e8c89f", "Hektor Barbossa", new DateTime(2019, 9, 13, 12, 16, 36, 661, DateTimeKind.Utc).AddTicks(7680), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "9abf8635-b89f-4c17-8a1d-937e8b1a36c7", new DateTime(2019, 9, 13, 12, 16, 36, 664, DateTimeKind.Utc).AddTicks(2313), new DateTime(1996, 9, 13, 14, 16, 36, 664, DateTimeKind.Local).AddTicks(2324), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "0a46ebd3-6ed4-4d64-b0f7-9cf2541750b3", new DateTime(2019, 9, 13, 12, 16, 36, 664, DateTimeKind.Utc).AddTicks(3667), new DateTime(2015, 9, 13, 14, 16, 36, 664, DateTimeKind.Local).AddTicks(3677), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "ff0d5462-43c2-4203-8d4e-c2058ed2f52d", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(227), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "b6bab1be-1191-4c01-8faf-73155e466ee8", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(1946), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "0a9b5087-d18f-48fd-a8c0-e43442d1600e", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(1964), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "ce01f53a-93c7-42d0-82b5-2ac213e6ea53", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(1967), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VisitInfo",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "c75b36c6-d514-494e-acc4-36ec306d002e", new DateTime(2019, 9, 13, 12, 16, 36, 663, DateTimeKind.Utc).AddTicks(8880), "TL;DR", 35, 5, 4, 2f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "b2b7cc65-0261-4f8e-bb87-cf3610503b4b", new DateTime(2019, 9, 13, 12, 16, 36, 663, DateTimeKind.Utc).AddTicks(8186), 0, true, 25, new DateTime(2019, 9, 15, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "ac866cfd-311f-48ca-afb0-55b17e7ef87f", new DateTime(2019, 9, 13, 12, 16, 36, 663, DateTimeKind.Utc).AddTicks(7214), 0, true, 30, new DateTime(2019, 9, 20, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "2d1ead4e-57ff-4cb7-9889-01ca528ee302", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8540), "Sunday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "e8cccff4-d6b8-4b2a-8a7e-d0a5e9edb497", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8525), "Saturday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "5d0da1dc-f87a-473a-8a99-d7c2e6cdf775", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8320), "Tuesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "36af60a6-b607-4bd8-b072-29414d18ee4a", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8494), "Thursday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "35b2e46a-9c3d-423a-a747-f97b1b8c6a3f", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8474), "Wednesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "1ae9838b-5d45-4767-b7f6-83533f79587a", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(4979), "Monday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "9741ff98-7898-4700-8ad5-fde26f264d4a", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8508), "Friday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "fdcb2ad8-a506-4ede-8933-df71f1e8d81c", new DateTime(2019, 9, 13, 12, 16, 36, 663, DateTimeKind.Utc).AddTicks(5868), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "de7882b4-7365-483b-aea0-a6de0e32fd22", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(3548), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "11dad3fd-37dc-4b2b-824c-1588cad17601", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(2716), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "7f3aaee2-9963-4f97-9631-13e514b3e036", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(3562), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "531d5cc9-3070-4f2c-95bd-c47a0dce1639", new DateTime(2019, 9, 13, 12, 16, 36, 661, DateTimeKind.Utc).AddTicks(8597), null, null, null, "5b2bee55-6854-4c48-9613-380fc5552a51", 0f, new DateTime(2019, 9, 13, 14, 16, 36, 661, DateTimeKind.Local).AddTicks(8589), null, "29baecc7-d26d-48eb-9cbf-96135adf42bf", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "a8e286cb-c1c3-48f6-9153-83d330bf084d", new DateTime(2019, 9, 13, 12, 16, 36, 661, DateTimeKind.Utc).AddTicks(9440), null, null, null, "a97aa442-ac3e-467e-87bc-00d281924b20", 0f, new DateTime(2019, 9, 13, 14, 16, 36, 661, DateTimeKind.Local).AddTicks(9433), null, "dace0157-96c4-4d09-b526-482331c8d2de", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Groups_GroupId",
                table: "Tickets",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Groups_GroupId",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "7f5f7c4a-cda6-43b3-8ad4-aafb02b59ac3");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "f6bee357-9b12-43eb-9dd0-4e24af90ef7c");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "667aefba-2b74-42a7-a1b4-e09e2a382e59");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "8f0608d3-636e-46bc-aab4-970fb3e8c89f");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "0a46ebd3-6ed4-4d64-b0f7-9cf2541750b3");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "9abf8635-b89f-4c17-8a1d-937e8b1a36c7");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "0a9b5087-d18f-48fd-a8c0-e43442d1600e");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "b6bab1be-1191-4c01-8faf-73155e466ee8");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "ce01f53a-93c7-42d0-82b5-2ac213e6ea53");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "ff0d5462-43c2-4203-8d4e-c2058ed2f52d");

            migrationBuilder.DeleteData(
                table: "VisitInfo",
                keyColumn: "Id",
                keyValue: "c75b36c6-d514-494e-acc4-36ec306d002e");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "ac866cfd-311f-48ca-afb0-55b17e7ef87f");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "b2b7cc65-0261-4f8e-bb87-cf3610503b4b");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "1ae9838b-5d45-4767-b7f6-83533f79587a");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "2d1ead4e-57ff-4cb7-9889-01ca528ee302");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "35b2e46a-9c3d-423a-a747-f97b1b8c6a3f");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "36af60a6-b607-4bd8-b072-29414d18ee4a");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "5d0da1dc-f87a-473a-8a99-d7c2e6cdf775");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "9741ff98-7898-4700-8ad5-fde26f264d4a");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "e8cccff4-d6b8-4b2a-8a7e-d0a5e9edb497");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "fdcb2ad8-a506-4ede-8933-df71f1e8d81c");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "11dad3fd-37dc-4b2b-824c-1588cad17601");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "7f3aaee2-9963-4f97-9631-13e514b3e036");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "de7882b4-7365-483b-aea0-a6de0e32fd22");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "531d5cc9-3070-4f2c-95bd-c47a0dce1639");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "a8e286cb-c1c3-48f6-9153-83d330bf084d");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Groups_GroupId",
                table: "Tickets",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
