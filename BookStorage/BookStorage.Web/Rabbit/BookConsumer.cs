using System.Threading.Tasks;
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
        
        public async Task Consume(ConsumeContext<IBookContract> context)
        {
            var books = context.Message;
            await _dataService.AddData(books.Books);
        }
    }
}