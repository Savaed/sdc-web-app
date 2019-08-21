using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
