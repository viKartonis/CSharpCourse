using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication.DTO;

namespace WebApplication
{
    public class Proxy : IProxy
    {
        private readonly HttpClient _httpClient;

        public Proxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookRequest>> GetBooksFromRemoteServer(int bookCount)
        {
            var result = await _httpClient.GetAsync(
                $"https://getbooksrestapi.azurewebsites.net/api/books/{bookCount}");
            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<BookRequest>>(resultString);
        }
        
    }
}