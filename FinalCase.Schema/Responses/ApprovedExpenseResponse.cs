namespace FinalCase.Schema.Responses
{
    public class ApprovedExpenseResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public virtual ExpenseCategoryResponse ExpenseCategory { get; set; }
        public virtual FieldEmployeeResponse FieldEmployee { get; set; }
    }
}
