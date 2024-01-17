using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseReportForEmployee.GetDailyExpenseReportForEmployee;
public record GetDailyExpenseReportForEmployeeQuery(int Id) : IRequest<ApiResponse<IEnumerable<EmployeeExpenseReport>>>;