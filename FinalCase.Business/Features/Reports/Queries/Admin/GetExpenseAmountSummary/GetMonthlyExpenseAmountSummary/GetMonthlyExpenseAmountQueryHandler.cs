using FinalCase.Base.Response;
using FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
using FinalCase.Business.MicroOrm.Constants;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetMonthlyExpenseAmountSummary;

public class GetMonthlyExpenseAmountQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetMonthlyExpenseAmountQuery, ApiResponse<IEnumerable<ExpenseAmountSummary>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<ExpenseAmountSummary>>> Handle(GetMonthlyExpenseAmountQuery request, CancellationToken cancellationToken)
    {
        var expenseAmountSummary = await DapperExecutor.QueryViewAsync<ExpenseAmountSummary>(
                                            Views.MonthlyExpenseAmountSummary,
                                            configuration.GetConnectionString(DbKeys.SqlServer),
                                            cancellationToken);

        return new ApiResponse<IEnumerable<ExpenseAmountSummary>>(expenseAmountSummary);
    }
}