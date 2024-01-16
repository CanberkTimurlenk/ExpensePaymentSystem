using FinalCase.Schema.Email;
using FinalCase.Schema.Requests;
using FinalCase.Services.PdfCreator;
using Hangfire;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;

namespace FinalCase.BackgroundJobs.Hangfire.Delayeds.Payment;

/// <summary>
/// Contains jobs related with payments.
/// </summary>
public static class PaymentJobs
{
    private readonly static IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();   // Get the configuration settings.

    /// <summary>
    /// Schedules a job to send a payment request to the banking system and continues with sending if the job is completed.
    /// </summary>
    /// <param name="request">The outgoing payment request.</param>
    /// <param name="email">The email to be sent.</param>
    /// <param name="configuration">The configuration settings.</param>
    public static void SendPaymentRequest(OutgoingPaymentRequest request, Email email, CancellationToken cancellationToken)
    {
        var jobId = BackgroundJob.Schedule(() => SendPaymentJobAsync(request, cancellationToken), TimeSpan.FromSeconds(3)); // Schedule a job to send the payment request to the banking system.
        BackgroundJob.ContinueJobWith(jobId, () => EmailSender.SendEmail(email)); // Schedule a job to send the email to the employee.        
    }

    /// <summary>
    /// Asynchronously sends a payment request to the banking system and handles the response.
    /// </summary>
    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail, DelaysInSeconds = [20, 60])]
    public static async Task SendPaymentJobAsync(OutgoingPaymentRequest request, CancellationToken cancellationToken)
    {
        var bankingApiConfig = configuration.GetSection("BankingApi");
        var client = new RestClient(bankingApiConfig.GetValue<string>("BaseUrl"));

        var checkResponse = await SendRequestAsync(client, bankingApiConfig.GetValue<string>("GetPaymentByDescription"), $"\"{request.Description}\"", cancellationToken);

        // We have guaranteed the payment is done only once
        if (checkResponse.StatusCode == HttpStatusCode.NotFound)
        {
            // If the payment description which is the unique key of the payment, base64(expenseId + employeeId), is not exists in the banking system,
            // create a new payment.
            var payResponse = await SendRequestAsync(client, bankingApiConfig.GetValue<string>("CreatePayment"), request.ToString(), cancellationToken);

            if (payResponse.StatusCode == HttpStatusCode.RequestTimeout)
                throw new TimeoutException(); // If timeout occurs, the job will be retried.


        }
    }

    // Create a private method to make the repeated requests shorter.
    /// <summary>
    /// Sends a request to the banking system.
    /// </summary>
    /// <param name="client"> The instance of RestClient using a specific BaseUrl for requests made by this client instance</param>
    /// <param name="resource">The Resource URL to make the request against.</param>
    /// <param name="body">The content to include in the request body as JSON.</param>
    public static Task<RestResponse> SendRequestAsync(RestClient client, string resource, string body, CancellationToken cancellationToken)
    {
        var request = new RestRequest(resource, Method.Post)
        {
            RequestFormat = DataFormat.Json,
            Timeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds
        }.AddBody(body);


        return client.ExecuteAsync(request, cancellationToken);
    }
}
