using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Constants;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;

public class GetDailyExpenseAmountQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetDailyExpenseAmountQuery, ApiResponse<IEnumerable<ExpenseAmountSummary>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<ExpenseAmountSummary>>> Handle(GetDailyExpenseAmountQuery request, CancellationToken cancellationToken)
    {
        var expenseAmountSummary = await DapperExecutor.QueryViewAsync<ExpenseAmountSummary>(
                                            Views.DailyExpenseAmountSummary,
                                            configuration.GetConnectionString(DbKeys.SqlServer),
                                            cancellationToken);

        return new ApiResponse<IEnumerable<ExpenseAmountSummary>>(expenseAmountSummary);
    }
}