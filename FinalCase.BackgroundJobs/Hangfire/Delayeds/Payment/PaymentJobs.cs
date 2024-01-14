using FinalCase.BackgroundJobs.Hangfire.FireAndForgets.SendEmail;
using FinalCase.Schema.Email;
using FinalCase.Schema.Requests;
using Hangfire;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;

namespace FinalCase.BackgroundJobs.Hangfire.FireAndForgets.SendPaymentRequest;
public static class PaymentJobs
{
    public static void SendPaymentRequest(OutgoingPaymentRequest request, Email content, IConfiguration configuration, CancellationToken cancellationToken)
    {
        var jobId = BackgroundJob.Schedule(() => SendPaymentJobAsync(request, cancellationToken), TimeSpan.FromMinutes(1));
        BackgroundJob.ContinueJobWith(jobId, () => EmailJobs.SendEmail(content, configuration));
    }

    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail, DelaysInSeconds = [20, 60])]
    private static async Task SendPaymentJobAsync(OutgoingPaymentRequest request, CancellationToken cancellationToken)
    {
        var client = new RestClient("https://localhost:7258");

        var restRequest = new RestRequest()
        {
            RequestFormat = DataFormat.Json,
            Method = Method.Post,
            Resource = "api/Payments/pay",
            Timeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds
        }.AddBody(request);

        var response = await client.ExecuteAsync(restRequest, cancellationToken);

        if (response.StatusCode == HttpStatusCode.RequestTimeout)
            throw new TimeoutException();
    }
}
