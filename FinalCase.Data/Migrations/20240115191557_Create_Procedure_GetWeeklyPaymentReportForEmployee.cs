using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Procedure_GetWeeklyPaymentReportForEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetWeeklyPaymentReportForEmployee
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
                        (P.Date BETWEEN DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE))
                               AND DATEADD(DAY, 7 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE)))
                        AND P.EmployeeId = @EmployeeId
                END
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetWeeklyPaymentReportForEmployee");
        }
    }
}
