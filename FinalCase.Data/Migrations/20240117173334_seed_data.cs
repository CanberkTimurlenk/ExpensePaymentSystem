using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ApplicationUsers",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "DateOfBirth", "Email", "Firstname", "Iban", "InsertDate", "InsertUserId", "IsActive", "LastActivityDate", "Lastname", "Password", "Role", "UpdateDate", "UpdateUserId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b.doe@example.com", "John", "sampleIban", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8283), 1, true, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8270), "Doe", "samplePassword", "employee", null, null, "sampleUsername" },
                    { 2, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c.doe@example.com", "ffffffffff", "sampleIban", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8466), 2, true, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8465), "eeeeeeeeeeeeee", "samplePassword", "employee", null, null, "abbb" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Description", "InsertDate", "InsertUserId", "Name", "UpdateDate", "UpdateUserId" },
                values: new object[,]
                {
                    { 1, "Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8630), 1, "b", null, null },
                    { 2, "Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8649), 2, "c", null, null },
                    { 3, "Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8664), 1, "d", null, null }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Description", "InsertDate", "InsertUserId", "Name", "UpdateDate", "UpdateUserId" },
                values: new object[,]
                {
                    { 1, "Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8687), 1, "b", null, null },
                    { 2, "Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8707), 2, "a", null, null },
                    { 3, "Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8724), 1, "x   ", null, null }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "AdminDescription", "Amount", "CategoryId", "CreatorEmployeeId", "Date", "EmployeeDescription", "InsertDate", "InsertUserId", "Location", "PaymentMethodId", "ReviewerAdminId", "Status", "UpdateDate", "UpdateUserId" },
                values: new object[,]
                {
                    { 1, "Admin Description", 100m, 1, 1, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8498), "Employee Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8503), 1, "Location", 1, null, 1, null, null },
                    { 2, "Admin Description", 100m, 1, 2, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8527), "Employee Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8528), 2, "Location", 1, null, 1, null, null },
                    { 3, "Admin Description", 100m, 1, 1, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8547), "Employee Description", new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8549), 1, "Location", 1, null, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Description", "ExpenseId", "InsertDate", "InsertUserId", "Name", "UpdateDate", "UpdateUserId", "Url" },
                values: new object[,]
                {
                    { 1, "Description", 1, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8572), 1, "Name", null, null, "Url" },
                    { 2, "Description", 1, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8590), 2, "Name", null, null, "Url" },
                    { 3, "Description", 1, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8606), 1, "a", null, null, "Url" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "EmployeeId", "ExpenseId", "Amount", "Date", "Description", "PaymentMethodId", "PaymentMethodName", "ReceiverIban", "ReceiverName" },
                values: new object[,]
                {
                    { 1, 1, 100m, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8789), "Description", 1, "PaymentMethodName", "ReceiverIban", "ReceiverName" },
                    { 1, 2, 200m, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8812), "Description", 1, "PaymentMethodName", "ReceiverIban", "ReceiverName" },
                    { 1, 3, 300m, new DateTime(2024, 1, 17, 20, 33, 33, 537, DateTimeKind.Local).AddTicks(8833), "Description", 1, "PaymentMethodName", "ReceiverIban", "ReceiverName" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumns: new[] { "EmployeeId", "ExpenseId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumns: new[] { "EmployeeId", "ExpenseId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumns: new[] { "EmployeeId", "ExpenseId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ApplicationUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
