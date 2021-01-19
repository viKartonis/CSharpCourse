using Quartz;
using WebApplication.Rabbit;

namespace WebApplication.Jobs
{
    [DisallowConcurrentExecution]
    public class SimpleJob  : IJob
    {
        private readonly IDataService _dataService;
        private List<BookResponse> _books;
        private readonly BookProducer _producer;

        public SimpleJob(IDataService dataService, BookProducer producer)
        {
            _dataService = dataService;
            _books = new List<BookResponse>();
            _producer = producer;
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            var needToOrder = _dataService.CheckNeedToOrder(context.FireTimeUtc);
           
            _producer.SentReceivedEvent(needToOrder);

            return Task.CompletedTask;
        }
    }
}