using System.Threading.Tasks;
using ContractLibrary;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Rabbit
{
    public class BookProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;

        public BookProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task SentReceivedEvent(int bookCount)
        {
            var message = new IBookNumberContract()
            {
                Count = bookCount
            };
            
            var hostConfig = new MassTransitConfiguration();
            _configuration.GetSection("MassTransit").Bind(hostConfig);
            var endpoint = await _sendEndpointProvider
                .GetSendEndpoint(hostConfig.GetQueueAddress(hostConfig.OutputQueue));
            await endpoint.Send(message);
        }
        
    }
}