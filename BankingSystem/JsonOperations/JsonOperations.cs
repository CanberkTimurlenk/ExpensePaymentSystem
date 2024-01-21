using BankingSystem.Models;
using System.Text.Json;

namespace BankingSystem.JsonOperations;
public static class JsonFile
{
    public static void Add(ICollection<OutgoingPaymentRequest> payments, OutgoingPaymentRequest payment)
    {
        payments.Add(payment);
        string json = JsonSerializer.Serialize(payments);

        using var streamWriter = new StreamWriter("data.json");
        streamWriter.Write(json);
    }
}
