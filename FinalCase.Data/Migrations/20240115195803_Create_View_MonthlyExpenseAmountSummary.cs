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
                    FORMAT(CAST(GETDATE() - DAY(GETDATE()) + 1 AS DATETIME),'yyyy-MM-dd 00:00:00') AS StartDateTime,
                    GETDATE() AS FinalDateTime,
                    SUM(CASE WHEN Status = 1 THEN Amount ELSE 0 END) AS PendingAmount,
                    SUM(CASE WHEN Status = 2 THEN Amount ELSE 0 END) AS ApprovedAmount,
                    SUM(CASE WHEN Status = 3 THEN Amount ELSE 0 END) AS RejectedAmount
                FROM
                    Expenses as E
                WHERE
                    MONTH(E.Date) = MONTH(GETDATE()) AND YEAR(E.Date) = YEAR(GETDATE())
                GROUP BY
                    CAST(Date AS DATE);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS MonthlyExpenseAmountSummary;");
        }
    }
}