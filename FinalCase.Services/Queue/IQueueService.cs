namespace FinalCase.BackgroundJobs.QueueService;

/// <summary>
/// Represents a service responsible for working with queues.
/// </summary>
public interface IQueueService
{
    void SendMessage(string queueName, byte[] body);

}
