using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Employee.GetEmployeeAllExpenseReportById;
public record GetEmployeeAllExpenseReportByIdQuery(int Id) : IRequest<ApiResponse<IEnumerable<EmployeeExpenseReport>>>;