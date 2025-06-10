namespace Taxually.TechnicalTest.Interfaces
{
    public interface ITaxuallyQueueClient
    {
        Task EnqueueAsync(string queueName, object payload);
    }
}