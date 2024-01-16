using FinalCase.BackgroundJobs.QueueService;
using FinalCase.Services.Queue.Constants;
using System.Text;

namespace FinalCase.Services.DocumentCreation;

public class DocumentCreationService(IQueueService queueService) : IDocumentCreationService
{
    private readonly IQueueService queueService = queueService;

    public void CreateDocument(string serializedBody)
    {
        queueService.SendMessage(Queues.Pdf, Encoding.UTF8.GetBytes(serializedBody));
    }
}
