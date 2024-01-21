﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_WeeklyExpenseAmountSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW WeeklyExpenseAmountSummary AS
                SELECT
                    CAST(DATEADD(DAY, 2 - DATEPART(WEEKDAY, GETDATE()), DATEDIFF(DAY, 0, GETDATE())) AS DATETIME) AS StartDateTime, 
                    DATEADD(SECOND, -1, DATEADD(DAY, 9 - DATEPART(WEEKDAY, GETDATE()), DATEDIFF(DAY, 0, GETDATE()))) AS FinalDateTime,
                    SUM(CASE WHEN Status = 1 THEN Amount ELSE 0 END) AS PendingAmount,
                    SUM(CASE WHEN Status = 2 THEN Amount ELSE 0 END) AS ApprovedAmount,
                    SUM(CASE WHEN Status = 3 THEN Amount ELSE 0 END) AS RejectedAmount
                FROM
                    Expenses as E
                WHERE
                    E.Date BETWEEN CAST(DATEADD(DAY, 2 - DATEPART(WEEKDAY, GETDATE()), DATEDIFF(DAY, 0, GETDATE())) AS DATETIME)
                        AND DATEADD(SECOND, -1, DATEADD(DAY, 9 - DATEPART(WEEKDAY, GETDATE()), DATEDIFF(DAY, 0, GETDATE())))
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS WeeklyExpenseAmountSummary;");
        }
    }
}