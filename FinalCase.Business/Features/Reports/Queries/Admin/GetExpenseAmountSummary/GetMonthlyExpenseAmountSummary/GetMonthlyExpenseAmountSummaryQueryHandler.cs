using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetMonthlyExpenseAmountSummary;

public class GetMonthlyExpenseAmountSummaryQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetMonthlyExpenseAmountSummaryQuery, ApiResponse<ExpenseAmountSummary>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<ExpenseAmountSummary>> Handle(GetMonthlyExpenseAmountSummaryQuery request, CancellationToken cancellationToken)
    {
        var expenseAmountSummary = await DapperExecutor.QueryViewAsync<ExpenseAmountSummary>(
                                            Views.MonthlyExpenseAmountSummary,
                                            configuration.GetConnectionString(DbKeys.SqlServer),
                                            cancellationToken);

        return new ApiResponse<ExpenseAmountSummary>(data: expenseAmountSummary.FirstOrDefault());
    }
}