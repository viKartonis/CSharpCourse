using System.Threading.Tasks;
using BookStorage.Service;
using MassTransit;
using ContractLibrary;

namespace WebApplication.Rabbit
{
    public class BookConsumer : IConsumer<IBookContract>
    {
        private readonly IBookService _service;

        public BookConsumer(IBookService service)
        {
            _service = service;
        }
        
        public async Task Consume(ConsumeContext<IBookContract> context)
        {
            var books = context.Message;
            await _service.AddData(books.Books);
        }
    }
}