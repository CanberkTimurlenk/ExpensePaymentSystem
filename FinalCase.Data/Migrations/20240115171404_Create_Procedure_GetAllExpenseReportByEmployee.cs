using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Procedure_GetAllExpenseReportByEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllExpenseReportByEmployee
                    @EmployeeId INT
                AS
                BEGIN
                    SELECT
                        E.EmployeeDescription,
                        E.Amount,
                        E.Date,
                        E.Location,
                        E.AdminDescription,
                        E.Status,                        
                        EC.Name AS CategoryName,
                        EC.Description AS CategoryDescription,                        
                        PM.Name AS PaymentMethodName,
                        PM.Description AS PaymentMethodDescription,
                        D.Name AS DocumentName,
                        D.Description AS DocumentDescription,
                        D.Url AS DocumentUrl
                    FROM
                        Expenses AS E
                        INNER JOIN ExpenseCategories AS EC ON E.CategoryId = EC.Id
                        INNER JOIN PaymentMethods AS PM ON E.PaymentMethodId = PM.Id
                        LEFT JOIN Documents AS D ON E.Id = D.ExpenseId
                    WHERE
                        E.CreatorEmployeeId = @EmployeeId                        
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllExpenseReportByEmployee");
        }
    }
}