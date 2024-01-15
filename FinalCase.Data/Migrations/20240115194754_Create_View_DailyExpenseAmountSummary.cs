using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_DailyExpenseAmountSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW DailyExpenseAmountSummary AS
                SELECT
                    CAST(Date AS DATE) AS ExpenseDate,
                    SUM(CASE WHEN Status = 1 THEN Amount ELSE 0 END) AS PendingAmount,
                    SUM(CASE WHEN Status = 2 THEN Amount ELSE 0 END) AS ApprovedAmount,
                    SUM(CASE WHEN Status = 3 THEN Amount ELSE 0 END) AS RejectedAmount
                FROM
                    Expenses
                WHERE
                    CAST(Date AS DATE) = CAST(GETDATE() AS DATE)
                GROUP BY
                    CAST(Date AS DATE);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS DailyExpenseAmountSummary;");
        }
    }
}
