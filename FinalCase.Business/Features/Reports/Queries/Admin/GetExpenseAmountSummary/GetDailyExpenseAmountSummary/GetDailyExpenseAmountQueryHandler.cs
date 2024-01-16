using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;

public class GetDailyExpenseAmountQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetDailyExpenseAmountSummaryQuery, ApiResponse<ExpenseAmountSummary>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<ExpenseAmountSummary>> Handle(GetDailyExpenseAmountSummaryQuery request, CancellationToken cancellationToken)
    {
        var expenseAmountSummary = await DapperExecutor.QueryViewAsync<ExpenseAmountSummary>(
                                            Views.DailyExpenseAmountSummary,
                                            configuration.GetConnectionString(DbKeys.SqlServer),
                                            cancellationToken);

        return new ApiResponse<ExpenseAmountSummary>(expenseAmountSummary.FirstOrDefault());
    }
}