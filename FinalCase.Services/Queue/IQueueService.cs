namespace FinalCase.BackgroundJobs.QueueService;
public interface IQueueService
{
    void SendMessage(string queueName, byte[] body);

}
