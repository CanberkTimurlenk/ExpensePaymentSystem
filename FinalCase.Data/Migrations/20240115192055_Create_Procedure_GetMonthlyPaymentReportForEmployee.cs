using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Procedure_GetMonthlyPaymentReportForEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetMonthlyPaymentReportForEmployee
                    @EmployeeId INT
                AS
                BEGIN
                    SELECT
                        P.Amount,
                        P.Description,                        
                        P.ReceiverName,
                        P.PaymentMethodName,
                        P.Date
                    FROM
                        Payments AS P
                    WHERE
                        MONTH(P.Date) = MONTH(GETDATE()) AND YEAR(P.Date) = YEAR(GETDATE())
                        AND P.EmployeeId = @EmployeeId
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetMonthlyPaymentReportForEmployee");
        }
    }
}
