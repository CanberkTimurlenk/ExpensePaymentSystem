namespace FinalCase.Schema.Responses;
public record AdminResponse
{
    public int Id { get; init; }
    public string Username { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public DateTime LastActivityDate { get; init; }
    public int PasswordRetryCount { get; init; }
    public DateTime DateOfBirth { get; init; }
    public bool IsActive { get; init; }

    //public virtual ICollection<PaymentReponse> PaymentsResponse { get; set; }
    //public virtual ICollection<ApprovedExpenseResponse> ApprovedExpensesResponse { get; set; }
    //public virtual ICollection<RejectedExpenseResponse> RejectedExpensesResponse { get; set; }
}
