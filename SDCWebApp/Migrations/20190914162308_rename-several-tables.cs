using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SDCWebApp.Migrations
{
    public partial class renameseveraltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningDates_VisitInfo_InfoId",
                table: "OpeningDates");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTariffs_SightseeingTariffs_SightseeingTariffId",
                table: "TicketTariffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitInfo",
                table: "VisitInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SightseeingTariffs",
                table: "SightseeingTariffs");

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

            migrationBuilder.DeleteData(
                table: "VisitInfo",
                keyColumn: "Id",
                keyValue: "c75b36c6-d514-494e-acc4-36ec306d002e");

            migrationBuilder.RenameTable(
                name: "VisitInfo",
                newName: "Info");

            migrationBuilder.RenameTable(
                name: "SightseeingTariffs",
                newName: "VisitTariffs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Info",
                table: "Info",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitTariffs",
                table: "VisitTariffs",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "897f4b56-3bcc-4b1d-8421-3f1b17def13a", new DateTime(2019, 9, 14, 16, 23, 8, 75, DateTimeKind.Utc).AddTicks(7263), new DateTime(2019, 9, 14, 18, 23, 8, 74, DateTimeKind.Local).AddTicks(2517), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "dfc8e7dd-b48a-4c44-a5b7-d3415a8182b9", new DateTime(2019, 9, 14, 16, 23, 8, 75, DateTimeKind.Utc).AddTicks(8988), new DateTime(2019, 9, 14, 18, 23, 8, 75, DateTimeKind.Local).AddTicks(8978), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "8de37511-3483-4ff4-8ffc-eeeb2f66ddfc", "Jack Sparrow", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(840), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "57a4d188-6267-4cd0-b091-065224c004aa", "Hektor Barbossa", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(2151), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "15006f10-89ed-4343-9463-d90cc54b64b9", new DateTime(2019, 9, 14, 16, 23, 8, 78, DateTimeKind.Utc).AddTicks(7018), new DateTime(1996, 9, 14, 18, 23, 8, 78, DateTimeKind.Local).AddTicks(7034), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "4dd4c79a-8d00-4868-9533-9d78037174d3", new DateTime(2019, 9, 14, 16, 23, 8, 78, DateTimeKind.Utc).AddTicks(8394), new DateTime(2015, 9, 14, 18, 23, 8, 78, DateTimeKind.Local).AddTicks(8402), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "84aff430-be8b-4dbe-b0ed-d29198342697", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(4844), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "05ba34a4-48fa-4557-bfa0-4ad8914a44b6", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(6567), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "a6100271-db96-454b-904f-30231b980831", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(6585), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "9522e2b4-6220-4651-ab4c-a2b1be107022", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(6588), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "0aeff4f8-3e8e-46ed-878c-1f308b4fe1c8", new DateTime(2019, 9, 14, 16, 23, 8, 78, DateTimeKind.Utc).AddTicks(3194), 0, true, 25, new DateTime(2019, 9, 16, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "79026a57-bbec-4169-9e36-0dc3f0468c62", new DateTime(2019, 9, 14, 16, 23, 8, 78, DateTimeKind.Utc).AddTicks(2219), 0, true, 30, new DateTime(2019, 9, 21, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Info",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "c51d6390-d99d-49a0-9084-060be11c86d9", new DateTime(2019, 9, 14, 16, 23, 8, 78, DateTimeKind.Utc).AddTicks(3967), "TL;DR", 35, 5, 4, 2f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "40da378e-6eeb-4ade-b6c8-a04e2b681abc", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 14, 16, 23, 8, 74, DateTimeKind.Utc).AddTicks(1745), "Sunday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "22fa371a-40ca-4996-b9c1-04338d063c83", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 14, 16, 23, 8, 74, DateTimeKind.Utc).AddTicks(1730), "Saturday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "ac77dfb0-72a8-4aaa-b59f-ce11eefa1199", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 14, 16, 23, 8, 74, DateTimeKind.Utc).AddTicks(1667), "Wednesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "25fb41b6-6ed8-414f-a26f-b66644a12645", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 14, 16, 23, 8, 74, DateTimeKind.Utc).AddTicks(1688), "Thursday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "e9030453-1a33-4c98-b47e-c4630f2c1e78", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 14, 16, 23, 8, 74, DateTimeKind.Utc).AddTicks(1485), "Tuesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "91b8adb0-513e-42d5-bf05-f8538c4047b8", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 14, 16, 23, 8, 73, DateTimeKind.Utc).AddTicks(7670), "Monday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "614d404a-3ea7-4e20-a3c3-50052b439218", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 14, 16, 23, 8, 74, DateTimeKind.Utc).AddTicks(1702), "Friday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "ce5a5f87-44ba-4a7d-b525-37ca06ff7573", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(7400), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "d98ebf7b-0609-4ca8-8b2a-40f1555a3bd2", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(8245), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "65bc1aa8-183b-43ad-b6d6-da720db09f32", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(8260), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "ec030629-1aa3-4989-aab5-a9e48fcbf4f2", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(3146), null, null, null, "b7104577-496c-4352-845a-0c949b877786", 0f, new DateTime(2019, 9, 14, 18, 23, 8, 76, DateTimeKind.Local).AddTicks(3136), null, "f75406c5-f8ad-4dfe-9c59-2464c676e93e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "OrderStamp", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "6b42aee4-0d9a-400f-859d-cd1eb4fbde67", new DateTime(2019, 9, 14, 16, 23, 8, 76, DateTimeKind.Utc).AddTicks(4008), null, null, null, "1131e655-16aa-4bd7-85cb-59380a0dfc85", 0f, new DateTime(2019, 9, 14, 18, 23, 8, 76, DateTimeKind.Local).AddTicks(4001), null, "5d71c814-ecc3-4888-9c50-891472aa62ef", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VisitTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "0c6bf34a-7dd0-4ac5-9cb9-5ec848cb4b95", new DateTime(2019, 9, 14, 16, 23, 8, 78, DateTimeKind.Utc).AddTicks(847), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningDates_Info_InfoId",
                table: "OpeningDates",
                column: "InfoId",
                principalTable: "Info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTariffs_VisitTariffs_SightseeingTariffId",
                table: "TicketTariffs",
                column: "SightseeingTariffId",
                principalTable: "VisitTariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningDates_Info_InfoId",
                table: "OpeningDates");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTariffs_VisitTariffs_SightseeingTariffId",
                table: "TicketTariffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitTariffs",
                table: "VisitTariffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Info",
                table: "Info");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "897f4b56-3bcc-4b1d-8421-3f1b17def13a");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "dfc8e7dd-b48a-4c44-a5b7-d3415a8182b9");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "57a4d188-6267-4cd0-b091-065224c004aa");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "8de37511-3483-4ff4-8ffc-eeeb2f66ddfc");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "15006f10-89ed-4343-9463-d90cc54b64b9");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "4dd4c79a-8d00-4868-9533-9d78037174d3");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "05ba34a4-48fa-4557-bfa0-4ad8914a44b6");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "84aff430-be8b-4dbe-b0ed-d29198342697");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "9522e2b4-6220-4651-ab4c-a2b1be107022");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "a6100271-db96-454b-904f-30231b980831");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "0aeff4f8-3e8e-46ed-878c-1f308b4fe1c8");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "79026a57-bbec-4169-9e36-0dc3f0468c62");

            migrationBuilder.DeleteData(
                table: "Info",
                keyColumn: "Id",
                keyValue: "c51d6390-d99d-49a0-9084-060be11c86d9");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "22fa371a-40ca-4996-b9c1-04338d063c83");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "25fb41b6-6ed8-414f-a26f-b66644a12645");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "40da378e-6eeb-4ade-b6c8-a04e2b681abc");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "614d404a-3ea7-4e20-a3c3-50052b439218");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "91b8adb0-513e-42d5-bf05-f8538c4047b8");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "ac77dfb0-72a8-4aaa-b59f-ce11eefa1199");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "e9030453-1a33-4c98-b47e-c4630f2c1e78");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "65bc1aa8-183b-43ad-b6d6-da720db09f32");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "ce5a5f87-44ba-4a7d-b525-37ca06ff7573");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "d98ebf7b-0609-4ca8-8b2a-40f1555a3bd2");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "6b42aee4-0d9a-400f-859d-cd1eb4fbde67");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "ec030629-1aa3-4989-aab5-a9e48fcbf4f2");

            migrationBuilder.DeleteData(
                table: "VisitTariffs",
                keyColumn: "Id",
                keyValue: "0c6bf34a-7dd0-4ac5-9cb9-5ec848cb4b95");

            migrationBuilder.RenameTable(
                name: "VisitTariffs",
                newName: "SightseeingTariffs");

            migrationBuilder.RenameTable(
                name: "Info",
                newName: "VisitInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SightseeingTariffs",
                table: "SightseeingTariffs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitInfo",
                table: "VisitInfo",
                column: "Id");

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
                values: new object[] { "35b2e46a-9c3d-423a-a747-f97b1b8c6a3f", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8474), "Wednesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "36af60a6-b607-4bd8-b072-29414d18ee4a", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8494), "Thursday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "5d0da1dc-f87a-473a-8a99-d7c2e6cdf775", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 13, 12, 16, 36, 659, DateTimeKind.Utc).AddTicks(8320), "Tuesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
                values: new object[] { "11dad3fd-37dc-4b2b-824c-1588cad17601", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(2716), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "de7882b4-7365-483b-aea0-a6de0e32fd22", new DateTime(2019, 9, 13, 12, 16, 36, 662, DateTimeKind.Utc).AddTicks(3548), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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

            migrationBuilder.InsertData(
                table: "VisitInfo",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "c75b36c6-d514-494e-acc4-36ec306d002e", new DateTime(2019, 9, 13, 12, 16, 36, 663, DateTimeKind.Utc).AddTicks(8880), "TL;DR", 35, 5, 4, 2f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningDates_VisitInfo_InfoId",
                table: "OpeningDates",
                column: "InfoId",
                principalTable: "VisitInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTariffs_SightseeingTariffs_SightseeingTariffId",
                table: "TicketTariffs",
                column: "SightseeingTariffId",
                principalTable: "SightseeingTariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
