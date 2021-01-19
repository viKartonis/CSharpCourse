namespace WebApplication
{
    public class MassTransitConfiguration
    {
        public string RabbitMqAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Durable { get; set; }
        public bool PurgeOnStartup { get; set; }
        public string InputQueue { get; set; }
        public string OutputQueue { get; set; }

        public Uri GetQueueAddress(string queueName)
        {
            return new Uri(RabbitMqAddress + "/" + queueName);
        }
    }
}