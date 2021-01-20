using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.Service;
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
        
        public async Task Execute(IJobExecutionContext context)
        {
            var needToOrder = await _dataService.CheckNeedToOrder();
           
            await _producer.SentReceivedEvent(needToOrder);
        }
    }
}