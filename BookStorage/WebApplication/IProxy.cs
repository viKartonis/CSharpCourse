using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.DTO;

namespace WebApplication
{
    public interface IProxy
    {
        Task<List<BookRequest>> GetBooksFromRemoteServer(int bookCount);
    }
}