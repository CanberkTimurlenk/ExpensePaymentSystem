using FinalCase.BackgroundJobs.QueueService;
using System.Text;

namespace FinalCase.Services.DocumentCreation;

public class DocumentCreationService(IQueueService queueService) : IDocumentCreationService
{
    private readonly IQueueService queueService = queueService;

    public void CreateDocument(string serializedBody)
    {
        queueService.SendMessage("PdfQueue", Encoding.UTF8.GetBytes(serializedBody));
    }
}
