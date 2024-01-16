using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Employee.GetEmployeeAllExpenseReport;
public record GetEmployeeAllExpenseReportByIdQuery(int Id) : IRequest<ApiResponse<IEnumerable<EmployeeExpenseReport>>>;