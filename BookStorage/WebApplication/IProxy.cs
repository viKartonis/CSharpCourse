using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.DataBase;

namespace WebApplication
{
    public interface IProxy
    {
        Task<List<EntityBook>> GetBooksFromRemoteServer(int bookCount);
    }
}