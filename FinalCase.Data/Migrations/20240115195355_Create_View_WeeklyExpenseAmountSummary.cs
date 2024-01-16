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
                    FORMAT(DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), GETDATE()),'yyyy-MM-dd 00:00:00') AS StartDateTime, 
                    GETDATE() AS FinalDateTime,
                    SUM(CASE WHEN Status = 1 THEN Amount ELSE 0 END) AS PendingAmount,
                    SUM(CASE WHEN Status = 2 THEN Amount ELSE 0 END) AS ApprovedAmount,
                    SUM(CASE WHEN Status = 3 THEN Amount ELSE 0 END) AS RejectedAmount
                FROM
                    Expenses as E
                WHERE
                    E.Date BETWEEN DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE))
                               AND DATEADD(DAY, 7 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE))                
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS WeeklyExpenseAmountSummary;");
        }
    }
}