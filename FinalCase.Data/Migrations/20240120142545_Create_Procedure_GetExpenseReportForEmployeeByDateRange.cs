using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Procedure_GetExpenseReportForEmployeeByDateRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetExpenseReportForEmployeeByDateRange
                    @UserId INT,
                    @StartDate DATETIME,
                    @EndDate DATETIME
                AS
                BEGIN
                    SELECT
                        E.Id AS ExpenseId,   
                        E.EmployeeDescription,
                        E.Amount,
                        E.Date,
                        E.Location,
                        E.AdminDescription,
                        E.Status AS ExpenseStatus,                        
                        EC.Name AS CategoryName,                        
                        PM.Name AS PaymentMethodName,                        
                        D.Name AS DocumentName,
                        D.Description AS DocumentDescription,
                        D.Url AS DocumentUrl
                    FROM
                        Expenses AS E
                        INNER JOIN ExpenseCategories AS EC ON E.CategoryId = EC.Id
                        INNER JOIN PaymentMethods AS PM ON E.PaymentMethodId = PM.Id
                        LEFT JOIN Documents AS D ON E.Id = D.ExpenseId
                    WHERE
                        E.Date BETWEEN @StartDate AND @EndDate
                        AND E.CreatorEmployeeId = @UserId
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetExpenseReportForEmployeeByDateRange");
        }
    }
}
