using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_DailyPaymentReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW DailyPaymentReport
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
                    P.Date BETWEEN CAST(DATEDIFF(DAY, 0, GETDATE()) AS DATETIME) 
                        AND DATEADD(SECOND, -1, DATEADD(DAY, 1, CAST(DATEDIFF(DAY, 0, GETDATE()) AS DATETIME)))
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS DailyPaymentReport");
        }
    }
}