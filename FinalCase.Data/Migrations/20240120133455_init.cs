using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AdminDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatorEmployeeId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    ReviewerAdminId = table.Column<int>(type: "int", nullable: true),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ApplicationUsers_CreatorEmployeeId",
                        column: x => x.CreatorEmployeeId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_ApplicationUsers_ReviewerAdminId",
                        column: x => x.ReviewerAdminId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiverIban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_ApplicationUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "DateOfBirth", "Email", "Firstname", "Iban", "InsertDate", "InsertUserId", "IsActive", "LastActivityDate", "Lastname", "Password", "Role", "UpdateDate", "UpdateUserId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "tukenmez.kalem@company.com", "tukenmez", "TR777777777777777777777777", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, true, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "kalem", "ba394499b56b89fe5bda1338fcca6a04", "employee", null, null, "kalem" },
                    { 2, new DateTime(1990, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "masa@company.com", "kırmızı", "TR666666666666666666666666", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, true, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "masa", "14e1b600b1fd579f47433b88e8d85291", "employee", null, null, "kirmizimasa" },
                    { 3, new DateTime(1990, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "yonetici@admin.com", "sirket", null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, true, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "yoneticisi", "71b6551474932fda956136e87886017c", "admin", null, null, "yonetici" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Description", "InsertDate", "InsertUserId", "Name", "UpdateDate", "UpdateUserId" },
                values: new object[,]
                {
                    { 1, "Is gezisindeki masraflar", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Is Gezisi", null, null },
                    { 2, "Kirtasiye ihtiyaclarimiz", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Kirtasiye Malzemeleri", null, null },
                    { 3, "Yemege gidilmesi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Yemek", null, null }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Description", "InsertDate", "InsertUserId", "Name", "UpdateDate", "UpdateUserId" },
                values: new object[,]
                {
                    { 1, "kredi karti ile yapilan odeme", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Credit Card", null, null },
                    { 2, "Banka transferi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Bank Transfer", null, null },
                    { 3, "Nakit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Cash", null, null }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "AdminDescription", "Amount", "CategoryId", "CreatorEmployeeId", "Date", "EmployeeDescription", "InsertDate", "InsertUserId", "Location", "PaymentMethodId", "ReviewerAdminId", "Status", "UpdateDate", "UpdateUserId" },
                values: new object[,]
                {
                    { 1, null, 120.50m, 1, 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bursaya is gezisi icin gitmistim", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bursa", 1, null, 1, null, null },
                    { 2, "", 75.30m, 2, 2, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fotokopi cektirmem gerekti", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "ofis", 2, null, 2, null, null },
                    { 3, "planlama yapilmadigindan reddedildi", 200.75m, 3, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ekip olarak yemege gittik", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "pizzaci", 3, null, 3, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Description", "ExpenseId", "InsertDate", "InsertUserId", "Name", "UpdateDate", "UpdateUserId", "Url" },
                values: new object[,]
                {
                    { 1, "fatura", 1, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Is seyahati faturasi", null, null, "ornekurl" },
                    { 2, "fatura ektedir", 2, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "fatura", null, null, "pdf.com" },
                    { 3, "fisi ekte bulabilirsiniz", 3, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Yemek ücretine ait fis", null, null, "fis.com" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "Date", "EmployeeId", "ExpenseId", "IsCompleted", "PaymentMethodId", "PaymentMethodName", "ReceiverIban", "ReceiverName" },
                values: new object[] { 1, 100m, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, true, 1, "Credit Card", "TR777777777777777777777777", "Tukenmez Kalem" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Email",
                table: "ApplicationUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ExpenseId",
                table: "Documents",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_Name",
                table: "ExpenseCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreatorEmployeeId",
                table: "Expenses",
                column: "CreatorEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentMethodId",
                table: "Expenses",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ReviewerAdminId",
                table: "Expenses",
                column: "ReviewerAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Name",
                table: "PaymentMethods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EmployeeId_ExpenseId",
                table: "Payments",
                columns: new[] { "EmployeeId", "ExpenseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ExpenseId",
                table: "Payments",
                column: "ExpenseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "PaymentMethods");
        }
    }
}
