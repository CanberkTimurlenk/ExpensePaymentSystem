using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using FinalCase.Data.Constants.Storage;
using FinalCase.Data.Constants.DbObjects;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
public class GetWeeklyExpenseAmountQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetWeeklyExpenseAmountSummaryQuery, ApiResponse<ExpenseAmountSummary>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<ExpenseAmountSummary>> Handle(GetWeeklyExpenseAmountSummaryQuery request, CancellationToken cancellationToken)
    {
        var expenseAmountSummary = await DapperExecutor.QueryViewAsync<ExpenseAmountSummary>(
                                            Views.WeeklyExpenseAmountSummary,
                                            configuration.GetConnectionString(DbKeys.SqlServer),
                                            cancellationToken);

        return new ApiResponse<ExpenseAmountSummary>(expenseAmountSummary.FirstOrDefault());
    }
}