namespace BankingSystem.Models;

public class OutgoingPaymentRequest
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    //public string Method { get; set; } // TODO
    public DateTime Date { get; set; }
    public string Description { get; set; }
}