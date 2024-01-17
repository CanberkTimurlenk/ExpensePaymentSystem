using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_MonthlyExpenseAmountSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW MonthlyExpenseAmountSummary AS
                SELECT
                    CAST(DATEADD(DAY, 1 - DAY(GETDATE()), DATEDIFF(DAY, 0, GETDATE())) AS DATETIME) AS StartDateTime,
                    DATEADD(SECOND, -1, DATEADD(DAY, 1, CAST(EOMONTH(GETDATE()) AS DATETIME))) AS FinalDateTime,
                    SUM(CASE WHEN Status = 1 THEN Amount ELSE 0 END) AS PendingAmount,
                    SUM(CASE WHEN Status = 2 THEN Amount ELSE 0 END) AS ApprovedAmount,
                    SUM(CASE WHEN Status = 3 THEN Amount ELSE 0 END) AS RejectedAmount
                FROM
                    Expenses as E
                WHERE
                    E.Date BETWEEN CAST(DATEADD(DAY, 1 - DAY(GETDATE()), DATEDIFF(DAY, 0, GETDATE())) AS DATETIME)
                        AND DATEADD(SECOND, -1, DATEADD(DAY, 1, CAST(EOMONTH(GETDATE()) AS DATETIME)))
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS MonthlyExpenseAmountSummary;");
        }
    }
}