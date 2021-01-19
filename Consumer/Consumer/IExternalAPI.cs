using System.Collections.Generic;
using System.Threading.Tasks;
using ContractLibrary;

namespace Consumer
{
    public interface IExternalAPI
    {
        Task<List<Book>> GetBooksFromRemoteServer(int bookCount);
    }
}