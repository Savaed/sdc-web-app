using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SDCWebApp.Migrations
{
    public partial class addorderstamptotickettable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningDates_GeneralSightseeingInfo_InfoId",
                table: "OpeningDates");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "1c3511ac-257c-4ec0-a4c0-15bedd94929a");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "915154c2-f9f0-4a05-8c36-97ef6f7bc32f");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "3079afac-c8ab-4cfb-a20d-28bf7bbb3117");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "fdbe22d2-0a10-4c1a-86ef-f61b3c203c92");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "38754d7c-8a07-42b5-8361-87b7ccc5964f");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "b03dd3ca-bf0a-48e2-8315-cb74c8d7c0b5");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "282d8606-c99e-442c-bdb8-2b2034456aa6");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "57b30168-7962-4385-a159-ac600f202fae");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "5d9087d3-f88b-478d-8296-0cee76a6c129");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "80d3a5ca-a8db-4eb0-aeff-b9417869bb82");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "5d12cac1-1e48-4be2-a778-04176e233411");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "02c31704-6d5e-4c6a-9cc5-31a228faa14c");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "ee158de6-cc7d-41bd-837a-0939dbd1915e");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "14faa5e2-dd27-43c4-9abc-7225af47334e");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "4bc59dc6-d710-4400-b323-1443a10943f4");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "4f5218e6-4694-4c69-88ca-da0d83b52e6f");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "b435acd2-5486-4e55-b1d6-663536e6e2ec");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "c107fa9b-8159-44cc-8509-c38ce00e3db3");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "cb12626c-5fc5-4f5d-b403-a068d7240537");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "cb9450f0-e39d-4202-9ad1-bf4f5548acb1");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "5454f1d0-24b4-46d0-84ec-ce58ccd049b1");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "3432266a-6957-46ca-a512-f2384792a031");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "c666b507-9e37-4885-b459-750788e1889f");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "e9b58082-80f2-46ff-8ed5-5a97a2f198dd");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "9c1940cb-9108-4176-bba9-d8c5e82590cb");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "c8045805-6091-46fd-968c-e959d0d461ec");

            migrationBuilder.AddColumn<string>(
                name: "OrderStamp",
                table: "Tickets",
                nullable: true);

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
                table: "GeneralSightseeingInfo",
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

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningDates_GeneralSightseeingInfo_InfoId",
                table: "OpeningDates",
                column: "InfoId",
                principalTable: "GeneralSightseeingInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningDates_GeneralSightseeingInfo_InfoId",
                table: "OpeningDates");

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
                table: "GeneralSightseeingInfo",
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

            migrationBuilder.DropColumn(
                name: "OrderStamp",
                table: "Tickets");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "915154c2-f9f0-4a05-8c36-97ef6f7bc32f", new DateTime(2019, 9, 11, 12, 2, 36, 772, DateTimeKind.Utc).AddTicks(6647), new DateTime(2019, 9, 11, 14, 2, 36, 771, DateTimeKind.Local).AddTicks(2647), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "1c3511ac-257c-4ec0-a4c0-15bedd94929a", new DateTime(2019, 9, 11, 12, 2, 36, 772, DateTimeKind.Utc).AddTicks(8364), new DateTime(2019, 9, 11, 14, 2, 36, 772, DateTimeKind.Local).AddTicks(8354), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "3079afac-c8ab-4cfb-a20d-28bf7bbb3117", "Jack Sparrow", new DateTime(2019, 9, 11, 12, 2, 36, 772, DateTimeKind.Utc).AddTicks(9879), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "fdbe22d2-0a10-4c1a-86ef-f61b3c203c92", "Hektor Barbossa", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(1277), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "b03dd3ca-bf0a-48e2-8315-cb74c8d7c0b5", new DateTime(2019, 9, 11, 12, 2, 36, 776, DateTimeKind.Utc).AddTicks(2299), new DateTime(1996, 9, 11, 14, 2, 36, 776, DateTimeKind.Local).AddTicks(2309), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "38754d7c-8a07-42b5-8361-87b7ccc5964f", new DateTime(2019, 9, 11, 12, 2, 36, 776, DateTimeKind.Utc).AddTicks(3676), new DateTime(2015, 9, 11, 14, 2, 36, 776, DateTimeKind.Local).AddTicks(3691), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "80d3a5ca-a8db-4eb0-aeff-b9417869bb82", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(3993), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "57b30168-7962-4385-a159-ac600f202fae", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(5742), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "282d8606-c99e-442c-bdb8-2b2034456aa6", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(5761), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "5d9087d3-f88b-478d-8296-0cee76a6c129", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(5764), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "MaxTicketOrderInterval", "SightseeingDuration", "UpdatedAt" },
                values: new object[] { "5d12cac1-1e48-4be2-a778-04176e233411", new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(9231), "TL;DR", 35, 5, 4, 2f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "02c31704-6d5e-4c6a-9cc5-31a228faa14c", new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(8480), 0, true, 25, new DateTime(2019, 9, 13, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "ee158de6-cc7d-41bd-837a-0939dbd1915e", new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(7499), 0, true, 30, new DateTime(2019, 9, 18, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "4bc59dc6-d710-4400-b323-1443a10943f4", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1598), "Sunday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "c107fa9b-8159-44cc-8509-c38ce00e3db3", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1584), "Saturday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "4f5218e6-4694-4c69-88ca-da0d83b52e6f", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1359), "Tuesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "cb12626c-5fc5-4f5d-b403-a068d7240537", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1540), "Thursday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "14faa5e2-dd27-43c4-9abc-7225af47334e", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1520), "Wednesday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "cb9450f0-e39d-4202-9ad1-bf4f5548acb1", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 11, 12, 2, 36, 770, DateTimeKind.Utc).AddTicks(7924), "Monday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OpeningDates",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "DayOfWeek", "InfoId", "OpeningHour", "UpdatedAt" },
                values: new object[] { "b435acd2-5486-4e55-b1d6-663536e6e2ec", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1566), "Friday", null, new TimeSpan(0, 10, 0, 0, 0), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "5454f1d0-24b4-46d0-84ec-ce58ccd049b1", new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(6097), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "e9b58082-80f2-46ff-8ed5-5a97a2f198dd", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(7403), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "3432266a-6957-46ca-a512-f2384792a031", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(6547), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "c666b507-9e37-4885-b459-750788e1889f", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(7417), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "9c1940cb-9108-4176-bba9-d8c5e82590cb", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(2265), null, null, null, 0f, new DateTime(2019, 9, 11, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(2257), null, "d1ba6690-e4cf-45dd-a523-c0408df6bbab", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 18, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(2723) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "c8045805-6091-46fd-968c-e959d0d461ec", new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(3205), null, null, null, 0f, new DateTime(2019, 9, 11, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(3197), null, "88a0248d-f1da-4724-8df0-fc4ae1277e19", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 2, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(3220) });

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningDates_GeneralSightseeingInfo_InfoId",
                table: "OpeningDates",
                column: "InfoId",
                principalTable: "GeneralSightseeingInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
