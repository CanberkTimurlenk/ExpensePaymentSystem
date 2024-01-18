using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.Approve;

/// <summary>
/// Represents a command to approve a collection of pending expenses.
/// </summary>
/// <param name="ReviewerAdminId">The identifier of the admin who is approving the expenses.
/// The Id will be extracted from the claims.</param>
/// <param name="ExpenseIdsToApprove">The collection of expense Ids to be approved.</param>
public record ApproveExpensesCommand(int ReviewerAdminId, ICollection<ApproveExpenseRequest> ExpenseIdsToApprove) : IRequest<ApiResponse<IEnumerable<ExpenseResponse>>>;

/// <summary>
/// Represents a single expense to be approved.
/// </summary>
/// <param name="Id">The expense id to be approved.</param>
public record ApproveExpenseRequest(int Id); // since this is the only place where this record used, we can nest it here.