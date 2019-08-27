using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCWebApp.Migrations
{
    public partial class configuredsightseeingtariffdeletebehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTariffs_SightseeingTariffs_SightseeingTariffId",
                table: "TicketTariffs");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTariffs_SightseeingTariffs_SightseeingTariffId",
                table: "TicketTariffs",
                column: "SightseeingTariffId",
                principalTable: "SightseeingTariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTariffs_SightseeingTariffs_SightseeingTariffId",
                table: "TicketTariffs");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTariffs_SightseeingTariffs_SightseeingTariffId",
                table: "TicketTariffs",
                column: "SightseeingTariffId",
                principalTable: "SightseeingTariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
