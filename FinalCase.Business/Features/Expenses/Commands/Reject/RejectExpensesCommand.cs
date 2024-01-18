using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.Reject;

/// <summary>
/// Represents a command to reject a collection of pending expenses.
/// </summary>
/// <param name="ReviewerAdminId">The identifier of the admin who is rejecting the expenses.</param>
/// <param name="Model">The collection of expense requests to be rejected.</param>
public record RejectExpensesCommand(int ReviewerAdminId, ICollection<RejectExpensesRequest> Model) : IRequest<ApiResponse<IEnumerable<ExpenseResponse>>>;

/// <summary>
/// Represents a request to reject a single expense.
/// </summary>
/// <param name="Id">The id of the expense to be rejected.</param>
/// <param name="AdminDescription">The rejection reason</param>
public record RejectExpensesRequest(int Id, string AdminDescription); // since this is the only place where this record used, we can nest it here.
