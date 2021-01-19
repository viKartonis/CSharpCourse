using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContractLibrary;
using Newtonsoft.Json;

namespace Consumer
{
    public class ExternalAPI : IExternalAPI
    {
        private readonly HttpClient _httpClient;

        public ExternalAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> GetBooksFromRemoteServer(int bookCount)
        {
            var result = await _httpClient.GetAsync(
                $"https://getbooksrestapi.azurewebsites.net/api/books/{bookCount}");
            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Book>>(resultString);
        }
        
    }
}