using FinalCase.Schema.Email;
using FinalCase.Services.NotificationService;
using Hangfire;
using Microsoft.Extensions.Configuration;
using static FinalCase.BackgroundJobs.Hangfire.Delayeds.Constants.BankingApiConstants;
using RestSharp;
using System.Net;
using FinalCase.Schema.ExternalApi;
using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Data.Constants.DbObjects;
using Dapper;
using FinalCase.Data.Constants.Storage;

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
    /// <param name="notificationService">The service instance managing notifications.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    public static void SendPaymentRequest(OutgoingPaymentRequest request, Email email, INotificationService notificationService, CancellationToken cancellationToken)
    {
        var sendPayment = BackgroundJob.Schedule(() => SendPaymentJobAsync(request, cancellationToken), TimeSpan.FromSeconds(3)); // Schedule a job to send the payment request to the banking system.
        var sendEmail = BackgroundJob.ContinueJobWith(sendPayment, () => notificationService.SendEmail(email)); // Schedule a job to send the email to the employee.        
        BackgroundJob.ContinueJobWith(sendEmail, () => CompletePayment(request.Description, cancellationToken));
    }

    /// <summary>
    /// Asynchronously sends a payment request to the banking system and handles the response.
    /// </summary>    
    public static async Task SendPaymentJobAsync(OutgoingPaymentRequest request, CancellationToken cancellationToken)
    {
        var bankingApiConfig = configuration.GetSection(BankingApiKey);
        var client = new RestClient(bankingApiConfig.GetValue<string>(BaseUrlKey));

        var checkResponse = await SendRequestAsync(client, bankingApiConfig.GetValue<string>(GetPaymentByDescriptionKey), $"\"{request.Description}\"", cancellationToken);

        // We have guaranteed the payment is done only once
        if (checkResponse.StatusCode == HttpStatusCode.NotFound)
        {
            // If the payment description which is the unique key of the payment, "expenseId,employeeId" , is not exists in the banking system,
            // create a new payment.
            var payResponse = await SendRequestAsync(client, bankingApiConfig.GetValue<string>(CreatePaymentKey), request.ToString(), cancellationToken);

            if (payResponse.StatusCode == HttpStatusCode.RequestTimeout)
                throw new TimeoutException(); // If timeout occurs, the job will be retried.
        }
        else if (checkResponse.StatusCode == 0)
        {
            throw checkResponse.ErrorException;  // connection is not established, the job will be retried.
        }
        // the exception will be saved by Hangfire.

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

    /// <summary>
    ///  Updates the payment status in the database.
    /// </summary>
    /// <param name="description">payment description(The payment id)</param>
    public static async Task CompletePayment(string description, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", int.Parse(description));

        await DapperExecutor.ExecuteStoredProcedureAsync(StoredProcedures.CompletePayment, parameters, configuration.GetConnectionString(DbKeys.SqlServer), cancellationToken);
    }
}
