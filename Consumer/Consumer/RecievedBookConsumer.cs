using System.Threading.Tasks;
using ContractLibrary;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Consumer
{
    public class ReceivedBookConsumer : IConsumer<IBookNumberContract>
    {
        private readonly IExternalAPI _api;
        private readonly IConfiguration _configuration;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ReceivedBookConsumer(IExternalAPI api, IConfiguration configuration,
            ISendEndpointProvider sendEndpointProvider)
        {
            _api = api;
            _configuration = configuration;
            _sendEndpointProvider = sendEndpointProvider;
        }
        
        public async Task Consume(ConsumeContext<IBookNumberContract> context)
        {
            var message = context.Message;
            var books = await _api.GetBooksFromRemoteServer(message.Count);

            var response = new IBookContract()
            {
                Books = books
            };
            var hostConfig = new MassTransitConfigurations();
            _configuration.GetSection("MassTransit").Bind(hostConfig);
             var endpoint = await _sendEndpointProvider.GetSendEndpoint(
                 hostConfig.GetQueueAddress(hostConfig.OutputQueue));
             await endpoint.Send(response);
        }
    }
}