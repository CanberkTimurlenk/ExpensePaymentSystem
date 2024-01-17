using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_MonthlyPaymentReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW MonthlyPaymentReport
                AS
                SELECT
                    P.Amount,
                    P.Description,
                    P.ReceiverIban,
                    P.ReceiverName,
                    P.PaymentMethodName,
                    P.Date
                FROM
                    Payments AS P
                WHERE
                    P.Date BETWEEN CAST(DATEADD(DAY, 1 - DAY(GETDATE()), DATEDIFF(DAY, 0, GETDATE())) AS DATETIME)
                        AND DATEADD(SECOND, -1, DATEADD(DAY, 1, CAST(EOMONTH(GETDATE()) AS DATETIME)))
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS MonthlyPaymentReport");
        }
    }
}
