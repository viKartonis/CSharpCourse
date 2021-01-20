using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.DataBase.Entities;
using ContractLibrary;

namespace BookStorage.Service
{
    public interface IBookService 
    {
        Task<List<Book>> GetData();
        Task AddData(List<Book> bookRequest);
        Task CreateBook(Book bookRequest);
        Task<EntityBook> GetBook(int bookId);
        Task DeleteBookFromShop(int bookId);
        Task UpdateBook(Book bookRequest);

    }
}