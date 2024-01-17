using FinalCase.BackgroundJobs.QueueService;
using FinalCase.Services.Queue.Constants;
using RabbitMQ.Client;
using System.Text;

namespace FinalCase.Services.DocumentCreation;

public class DocumentCreationService(IQueueService queueService) : IDocumentCreationService
{
    private readonly IQueueService queueService = queueService;

    public void CreatePdf(string serializedSource, string recipentEmail)
    {
        queueService.SendMessage(Queues.Pdf, Encoding.UTF8.GetBytes(serializedSource));
    }

    
}
