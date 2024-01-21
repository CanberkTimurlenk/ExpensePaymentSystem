namespace FinalCase.Business.Features.Documents.Constants;
public static class DocumentMessages
{
    public const string DocumentNotFound = "Document not found.";
    public const string ExpenseNotFound = "The expense ID : {0} does not match any existing expenses.";

    public const  string ExpenseNotBelongingToUser = "You do not have permission to add a document to this expense.";

    public const  string UnauthorizedDocumentUpdate = "You do not have permission to update this document.";

}
