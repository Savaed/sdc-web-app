using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class addexpiryincolumntorefreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "ExpiryIn",
                table: "RefreshTokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "3584f443-33fc-45bd-a0a5-d1b972c93f98", new DateTime(2019, 9, 5, 14, 15, 44, 868, DateTimeKind.Utc).AddTicks(5071), new DateTime(2019, 9, 5, 16, 15, 44, 866, DateTimeKind.Local).AddTicks(8821), "Attempt to steal the Dead Man's Chest", "LogIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jacks" });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "Type", "UpdatedAt", "User" },
                values: new object[] { "d0399544-4e41-4070-991e-3107617e4524", new DateTime(2019, 9, 5, 14, 15, 44, 868, DateTimeKind.Utc).AddTicks(7033), new DateTime(2019, 9, 5, 16, 15, 44, 868, DateTimeKind.Local).AddTicks(7019), "Revolting on a black pearl", "LogOut", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hektorb" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "27d8fa7a-7d5f-4dcb-a51c-9d49eddf7784", "Jack Sparrow", new DateTime(2019, 9, 5, 14, 15, 44, 868, DateTimeKind.Utc).AddTicks(9998), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The bast pirate i'v every seen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CreatedAt", "Text", "Title", "UpdatedAt" },
                values: new object[] { "04650c0c-7c5e-405b-9a50-c99d90808d09", "Hektor Barbossa", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(1302), "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl", "The captain of Black Pearl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "61fcf266-c009-4228-ad0b-2fd3c79595ba", new DateTime(2019, 9, 5, 14, 15, 44, 872, DateTimeKind.Utc).AddTicks(1982), new DateTime(1996, 9, 5, 16, 15, 44, 872, DateTimeKind.Local).AddTicks(1992), "example@mail.com", false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "EmailAddress", "HasFamilyCard", "IsChild", "IsDisabled", "UpdatedAt" },
                values: new object[] { "14727643-9141-4474-bbd8-da9bc77c736b", new DateTime(2019, 9, 5, 14, 15, 44, 872, DateTimeKind.Utc).AddTicks(3314), new DateTime(2015, 9, 5, 16, 15, 44, 872, DateTimeKind.Local).AddTicks(3323), " example2@mail.uk", true, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "b439511d-d239-479a-944b-306c3bd5c690", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(5698), "Discount for disabled people.", 50, null, "ForDisabled", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "0ed93a76-da73-4b92-92ef-5cfd4ad73e54", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(5702), "Discount only for kids under specific age.", 100, null, "ForChild", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "9bb34393-981b-48cf-9686-52c19f674dd8", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(3992), "Discount for groups", 15, 20, "ForGroup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "Description", "DiscountValueInPercentage", "GroupSizeForDiscount", "Type", "UpdatedAt" },
                values: new object[] { "8ea64c3e-9030-4835-92fa-cdd373ba8711", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(5680), "Discount for people with Family Card", 15, null, "ForFamily", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "GeneralSightseeingInfo",
                columns: new[] { "Id", "ClosingHour", "CreatedAt", "Description", "MaxAllowedGroupSize", "MaxChildAge", "OpeningHour", "UpdatedAt" },
                values: new object[] { "5cb885fe-dd67-4747-8669-87087aa23e68", 17f, new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(8711), "TL;DR", 35, 5, 9f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "5cf29df4-7c37-4537-8182-20c47581dea8", new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(6990), 0, true, 30, new DateTime(2019, 9, 12, 16, 15, 44, 871, DateTimeKind.Local).AddTicks(7001), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "CurrentGroupSize", "IsAvailablePlace", "MaxGroupSize", "SightseeingDate", "UpdatedAt" },
                values: new object[] { "f75f9aa4-e2d4-4b2e-b2dc-bfcbfe72b2b0", new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(7949), 0, true, 25, new DateTime(2019, 9, 7, 16, 15, 44, 871, DateTimeKind.Local).AddTicks(7958), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SightseeingTariffs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "3e973817-585e-4cfc-acba-2cd1fa812bc3", new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(5650), "BasicTickets", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "5d47ad2a-24b8-485a-9331-04b5a16cc181", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(7421), 22f, "Centrum Dziedzictwa Szkła.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "a1e6d592-6b14-4d3b-b91a-d30645df1edd", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(6497), 28f, "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TicketTariffs",
                columns: new[] { "Id", "CreatedAt", "DefaultPrice", "Description", "IsPerHour", "IsPerPerson", "SightseeingTariffId", "UpdatedAt" },
                values: new object[] { "c017b24e-2330-4cbe-95cf-20a2388b992d", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(7442), 10f, "Piwnice Przedprożne.", false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "5d436cd2-d41e-4354-b576-8e0776f7bf95", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(2280), null, null, null, 0f, new DateTime(2019, 9, 5, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(2272), null, "a236c022-d193-4d65-974f-8249b24e6feb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 12, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(2731) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DiscountId", "GroupId", "Price", "PurchaseDate", "TariffId", "TicketUniqueId", "UpdatedAt", "ValidFor" },
                values: new object[] { "97cf000f-4c03-4b86-a526-0ed9364968bb", new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(3198), null, null, null, 0f, new DateTime(2019, 9, 5, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(3191), null, "93271c21-90c8-4b8c-89e9-43377b1d71f7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 26, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(3212) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "3584f443-33fc-45bd-a0a5-d1b972c93f98");

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: "d0399544-4e41-4070-991e-3107617e4524");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "04650c0c-7c5e-405b-9a50-c99d90808d09");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "27d8fa7a-7d5f-4dcb-a51c-9d49eddf7784");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "14727643-9141-4474-bbd8-da9bc77c736b");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "61fcf266-c009-4228-ad0b-2fd3c79595ba");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "0ed93a76-da73-4b92-92ef-5cfd4ad73e54");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "8ea64c3e-9030-4835-92fa-cdd373ba8711");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "9bb34393-981b-48cf-9686-52c19f674dd8");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: "b439511d-d239-479a-944b-306c3bd5c690");

            migrationBuilder.DeleteData(
                table: "GeneralSightseeingInfo",
                keyColumn: "Id",
                keyValue: "5cb885fe-dd67-4747-8669-87087aa23e68");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "5cf29df4-7c37-4537-8182-20c47581dea8");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "f75f9aa4-e2d4-4b2e-b2dc-bfcbfe72b2b0");

            migrationBuilder.DeleteData(
                table: "SightseeingTariffs",
                keyColumn: "Id",
                keyValue: "3e973817-585e-4cfc-acba-2cd1fa812bc3");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "5d47ad2a-24b8-485a-9331-04b5a16cc181");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "a1e6d592-6b14-4d3b-b91a-d30645df1edd");

            migrationBuilder.DeleteData(
                table: "TicketTariffs",
                keyColumn: "Id",
                keyValue: "c017b24e-2330-4cbe-95cf-20a2388b992d");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "5d436cd2-d41e-4354-b576-8e0776f7bf95");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: "97cf000f-4c03-4b86-a526-0ed9364968bb");

            migrationBuilder.DropColumn(
                name: "ExpiryIn",
                table: "RefreshTokens");

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
    }
}
