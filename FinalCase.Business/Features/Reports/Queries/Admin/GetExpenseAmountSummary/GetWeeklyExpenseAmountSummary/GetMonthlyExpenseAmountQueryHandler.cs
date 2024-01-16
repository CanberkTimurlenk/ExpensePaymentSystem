using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Constants;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using FinalCase.Data.Constants.Storage;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
public class GetWeeklyExpenseAmountQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetWeeklyExpenseAmountQuery, ApiResponse<IEnumerable<ExpenseAmountSummary>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<ExpenseAmountSummary>>> Handle(GetWeeklyExpenseAmountQuery request, CancellationToken cancellationToken)
    {
        var expenseAmountSummary = await DapperExecutor.QueryViewAsync<ExpenseAmountSummary>(
                                            Views.WeeklyExpenseAmountSummary,
                                            configuration.GetConnectionString(DbKeys.SqlServer),
                                            cancellationToken);

        return new ApiResponse<IEnumerable<ExpenseAmountSummary>>(expenseAmountSummary);
    }
}