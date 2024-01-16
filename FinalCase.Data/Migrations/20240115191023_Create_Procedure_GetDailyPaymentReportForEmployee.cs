using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Procedure_GetDailyPaymentReportForEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetDailyPaymentReportForEmployee
                    @EmployeeId INT
                AS
                BEGIN
                    SELECT
                        P.Amount,
                        P.Description,                        
                        P.ReceiverName,
                        P.PaymentMethodName,
                        P.ReceiverIban,
                        P.Date
                    FROM
                        Payments AS P
                    WHERE
                        P.Date = CONVERT(DATE, GETDATE())
                            AND P.EmployeeId = @EmployeeId
                END
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetDailyPaymentReportForEmployee");
        }
    }
}
