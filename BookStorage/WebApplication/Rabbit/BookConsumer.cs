using MassTransit;
using ContractLibrary;

namespace WebApplication.Rabbit
{
    public class BookConsumer : IConsumer<IBookContract>
    {
        private readonly IDataService _dataService;

        public BookConsumer(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        public Task Consume(ConsumeContext<IBookContract> context)
        {
            var books = context.Message;
            _dataService.AddData(books.Books);
            return Task.CompletedTask;
        }
    }
}