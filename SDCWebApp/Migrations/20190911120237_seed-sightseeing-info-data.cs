using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class seedsightseeinginfodata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "GeneralSightseeingInfo",
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
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "8a39810a-05ea-4d7a-b190-5a2ddb0b7d42");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "a2aece52-66c1-441d-ab99-ac0acb491fa8");

            migrationBuilder.DeleteData(
                table: "OpeningDates",
                keyColumn: "Id",
                keyValue: "b3ec0dcd-2b97-4468-838a-315eff942d75");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "GeneralSightseeingInfo",
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
        }
    }
}
