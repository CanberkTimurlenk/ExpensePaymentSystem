using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseReportForEmployee.GetWeeklyExpenseReportForEmployee;

public record GetWeeklyExpenseReportForEmployeeQuery(int Id) : IRequest<ApiResponse<IEnumerable<EmployeeExpenseReport>>>;