using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_WeeklyPaymentReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW WeeklyPaymentReport
                AS
                SELECT
                    P.Amount,                    
                    P.ReceiverIban,
                    P.ReceiverName,
                    P.PaymentMethodName,
                    P.Date
                FROM
                    Payments AS P
                WHERE
                    P.Date BETWEEN CAST(DATEADD(DAY, 2 - DATEPART(WEEKDAY, GETDATE()), DATEDIFF(DAY, 0, GETDATE())) AS DATETIME)
                               AND DATEADD(SECOND, -1, DATEADD(DAY, 9 - DATEPART(WEEKDAY, GETDATE()), DATEDIFF(DAY, 0, GETDATE())))
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS WeeklyPaymentReport");
        }
    }
}