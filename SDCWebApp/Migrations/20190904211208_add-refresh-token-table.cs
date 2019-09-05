using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class addrefreshtokentable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "1ad4457c-15ad-4523-adb5-92bb6834f31b");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "fcef1bc8-9ccf-4f52-b39b-19e5b1b5d756");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "c2f498e0-5dad-4ce1-a4af-efca2d18495c");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "d9e170ac-55c0-4e50-9030-bd2f97a50338");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "7a4a65b2-0843-4f7e-b0c5-2dab297f363a");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "c636bfd5-f80c-42a0-a549-c009b384891c");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "03b04bfd-0c0f-4a17-9fe6-d60c2adf0859");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "56459153-6ef9-49a9-b7ad-6aa7ca1df9ea");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "77f3769b-0d66-4c7f-a734-925ed3541697");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "a6578180-9da9-4385-86e3-1f9a96039da4");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "6e90c73d-e21a-43cf-90fc-b0959b42e022");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "0aad11bf-f5a2-483d-9eba-d354e0cb3d3d");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "f9a43dcd-7f16-4fdc-a958-de72411dc6ec");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "cdf4562c-9809-4d53-9e8b-6761e2a4a84a");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "0ecd2e09-6915-43e9-9990-49f95f456040");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "6761a403-767b-44da-94dc-e40403dae44e");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "c56cabd7-bad6-46bc-8fd2-220f44febecb");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "6bbc1de0-9bd3-4cbd-a073-3f7c2210075e");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "7e4f6d23-b400-4fae-a3f7-662e8220264c");

            migrationBuilder.RenameColumn(
                name: "EmailAddres",
                table: "Customers",
                newName: "EmailAddress");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "d847c03e-0d34-4f2f-90ba-57c0d3804895", new DateTime(2019, 9, 4, 21, 12, 7, 651, DateTimeKind.Utc).AddTicks(7819), new DateTime(2019, 9, 4, 23, 12, 7, 650, DateTimeKind.Local).AddTicks(931), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "bfd81277-f058-4f4c-8be4-23593ae74aa6", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(96), new DateTime(2019, 9, 4, 23, 12, 7, 652, DateTimeKind.Local).AddTicks(81), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "88d77ac1-3ea5-480d-b9bd-53ea136a57bf", "Jack Sparrow", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(2943), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "7572a3fc-1013-4266-ac70-7e4b4b6ed52b", "Hektor Barbossa", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(4262), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "9635d8ee-77fb-42d0-a540-7ab2b13f788f", new DateTime(2019, 9, 4, 21, 12, 7, 655, DateTimeKind.Utc).AddTicks(6697), new DateTime(1996, 9, 4, 23, 12, 7, 655, DateTimeKind.Local).AddTicks(6708), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "f9a1cb76-5e02-4080-ab15-22ace523e7e1", new DateTime(2019, 9, 4, 21, 12, 7, 655, DateTimeKind.Utc).AddTicks(8065), new DateTime(2015, 9, 4, 23, 12, 7, 655, DateTimeKind.Local).AddTicks(8075), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "293193eb-66c7-4861-b065-6b52ad356ce9", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(9389), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "15ad0247-2913-472f-9a07-252a3e2adb89", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(9392), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "fb11ebc4-5993-4085-96ca-bd357e30b47d", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(7389), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "15864a68-da0d-4f5c-a4ef-f509d2cb55e4", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(9241), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "33256280-fa2a-4712-9365-08ed680f493e", 17f, new DateTime(2019, 9, 4, 21, 12, 7, 655, DateTimeKind.Utc).AddTicks(3674), "TL;DR", 35, 5, 9f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "a10f917d-1c24-4a1d-ba14-ddf4ad454efe", new DateTime(2019, 9, 4, 21, 12, 7, 655, DateTimeKind.Utc).AddTicks(1945), 0, true, 30, new DateTime(2019, 9, 11, 23, 12, 7, 655, DateTimeKind.Local).AddTicks(1956), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "07aa8ea0-86f3-4ab3-9458-cf58badbcf13", new DateTime(2019, 9, 4, 21, 12, 7, 655, DateTimeKind.Utc).AddTicks(2917), 0, true, 25, new DateTime(2019, 9, 6, 23, 12, 7, 655, DateTimeKind.Local).AddTicks(2932), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "cf1cafb3-7493-4252-adae-4c91c56f93a5", new DateTime(2019, 9, 4, 21, 12, 7, 655, DateTimeKind.Utc).AddTicks(582), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "fcd9a2fe-a65f-4381-bfd8-410b912765bd", new DateTime(2019, 9, 4, 21, 12, 7, 653, DateTimeKind.Utc).AddTicks(1048), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "e5f2033f-7477-49c7-be97-1f3b9cc1de84", new DateTime(2019, 9, 4, 21, 12, 7, 653, DateTimeKind.Utc).AddTicks(208), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "c738cfff-6dc7-40af-a312-f551c7205659", new DateTime(2019, 9, 4, 21, 12, 7, 653, DateTimeKind.Utc).AddTicks(1065), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "71017e93-c0f6-4473-bd22-714f563bf56e", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(5369), null, null, null, 0f, new DateTime(2019, 9, 4, 23, 12, 7, 652, DateTimeKind.Local).AddTicks(5360), null, "dff38be2-9753-4c5b-9a0f-7d2a2b2c40da", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 11, 23, 12, 7, 652, DateTimeKind.Local).AddTicks(5823) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "28da433a-0577-4a69-8b29-c2f567fc4ae7", new DateTime(2019, 9, 4, 21, 12, 7, 652, DateTimeKind.Utc).AddTicks(6453), null, null, null, 0f, new DateTime(2019, 9, 4, 23, 12, 7, 652, DateTimeKind.Local).AddTicks(6446), null, "0502bbb3-7aa8-4ee9-b6e0-96028ea32a09", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 25, 23, 12, 7, 652, DateTimeKind.Local).AddTicks(6470) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "bfd81277-f058-4f4c-8be4-23593ae74aa6");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "d847c03e-0d34-4f2f-90ba-57c0d3804895");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "7572a3fc-1013-4266-ac70-7e4b4b6ed52b");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "88d77ac1-3ea5-480d-b9bd-53ea136a57bf");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "9635d8ee-77fb-42d0-a540-7ab2b13f788f");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "f9a1cb76-5e02-4080-ab15-22ace523e7e1");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "15864a68-da0d-4f5c-a4ef-f509d2cb55e4");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "15ad0247-2913-472f-9a07-252a3e2adb89");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "293193eb-66c7-4861-b065-6b52ad356ce9");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "fb11ebc4-5993-4085-96ca-bd357e30b47d");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "33256280-fa2a-4712-9365-08ed680f493e");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "07aa8ea0-86f3-4ab3-9458-cf58badbcf13");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "a10f917d-1c24-4a1d-ba14-ddf4ad454efe");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "cf1cafb3-7493-4252-adae-4c91c56f93a5");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "c738cfff-6dc7-40af-a312-f551c7205659");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "e5f2033f-7477-49c7-be97-1f3b9cc1de84");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "fcd9a2fe-a65f-4381-bfd8-410b912765bd");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "28da433a-0577-4a69-8b29-c2f567fc4ae7");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "71017e93-c0f6-4473-bd22-714f563bf56e");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Customers",
                newName: "EmailAddres");

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "fcef1bc8-9ccf-4f52-b39b-19e5b1b5d756", new DateTime(2019, 8, 27, 17, 22, 52, 512, DateTimeKind.Utc).AddTicks(7003), new DateTime(2019, 8, 27, 19, 22, 52, 511, DateTimeKind.Local).AddTicks(1075), "Attempt to steal the Dead Man's Chest", "LogIn", null, "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "1ad4457c-15ad-4523-adb5-92bb6834f31b", new DateTime(2019, 8, 27, 17, 22, 52, 512, DateTimeKind.Utc).AddTicks(9013), new DateTime(2019, 8, 27, 19, 22, 52, 512, DateTimeKind.Local).AddTicks(9000), "Revolting on a black pearl", "LogOut", null, "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "c2f498e0-5dad-4ce1-a4af-efca2d18495c", "Jack Sparrow", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(1648), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "d9e170ac-55c0-4e50-9030-bd2f97a50338", "Hektor Barbossa", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(2948), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "7a4a65b2-0843-4f7e-b0c5-2dab297f363a", new DateTime(2019, 8, 27, 17, 22, 52, 516, DateTimeKind.Utc).AddTicks(3430), new DateTime(1996, 8, 27, 19, 22, 52, 516, DateTimeKind.Local).AddTicks(3440), "example@mail.com", false, false, false, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddres", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "c636bfd5-f80c-42a0-a549-c009b384891c", new DateTime(2019, 8, 27, 17, 22, 52, 516, DateTimeKind.Utc).AddTicks(5583), new DateTime(2015, 8, 27, 19, 22, 52, 516, DateTimeKind.Local).AddTicks(5598), " example2@mail.uk", true, false, false, null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "77f3769b-0d66-4c7f-a734-925ed3541697", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(7355), "Discount for disabled people.", 50, null, "ForDisabled", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "a6578180-9da9-4385-86e3-1f9a96039da4", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(7358), "Discount only for kids under specific age.", 100, null, "ForChild", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "56459153-6ef9-49a9-b7ad-6aa7ca1df9ea", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(5663), "Discount for groups", 15, 20, "ForGroup", null });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "03b04bfd-0c0f-4a17-9fe6-d60c2adf0859", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(7337), "Discount for people with Family Card", 15, null, "ForFamily", null });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "6e90c73d-e21a-43cf-90fc-b0959b42e022", 17f, new DateTime(2019, 8, 27, 17, 22, 52, 516, DateTimeKind.Utc).AddTicks(354), "TL;DR", 35, 5, 9f, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "f9a43dcd-7f16-4fdc-a958-de72411dc6ec", new DateTime(2019, 8, 27, 17, 22, 52, 515, DateTimeKind.Utc).AddTicks(8593), 0, true, 30, new DateTime(2019, 9, 3, 19, 22, 52, 515, DateTimeKind.Local).AddTicks(8604), null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "0aad11bf-f5a2-483d-9eba-d354e0cb3d3d", new DateTime(2019, 8, 27, 17, 22, 52, 515, DateTimeKind.Utc).AddTicks(9603), 0, true, 25, new DateTime(2019, 8, 29, 19, 22, 52, 515, DateTimeKind.Local).AddTicks(9612), null });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "cdf4562c-9809-4d53-9e8b-6761e2a4a84a", new DateTime(2019, 8, 27, 17, 22, 52, 515, DateTimeKind.Utc).AddTicks(7272), "BasicTickets", null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "c56cabd7-bad6-46bc-8fd2-220f44febecb", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(9019), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "6761a403-767b-44da-94dc-e40403dae44e", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(8144), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "0ecd2e09-6915-43e9-9990-49f95f456040", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(9035), 10f, "Piwnice Przedprożne.", false, true, null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "7e4f6d23-b400-4fae-a3f7-662e8220264c", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(3963), null, null, null, 0f, new DateTime(2019, 8, 27, 19, 22, 52, 513, DateTimeKind.Local).AddTicks(3955), null, "d7e93bbf-f5e6-49c5-ab0d-d63f5fd59691", null, new DateTime(2019, 9, 3, 19, 22, 52, 513, DateTimeKind.Local).AddTicks(4419) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "6bbc1de0-9bd3-4cbd-a073-3f7c2210075e", new DateTime(2019, 8, 27, 17, 22, 52, 513, DateTimeKind.Utc).AddTicks(4892), null, null, null, 0f, new DateTime(2019, 8, 27, 19, 22, 52, 513, DateTimeKind.Local).AddTicks(4885), null, "2d7c2a7b-775d-4157-8198-dac1fcd79249", null, new DateTime(2019, 9, 17, 19, 22, 52, 513, DateTimeKind.Local).AddTicks(4907) });
        }
    }
}
