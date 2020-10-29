using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication
{
    public interface IProxy
    {
        Task<List<BookResponse>> GetBooksFromRemoteServer(int bookCount);
    }
}