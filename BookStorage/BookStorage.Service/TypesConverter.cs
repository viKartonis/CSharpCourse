using System.Threading.Tasks;
using BookStorage.DataBase;
using BookStorage.DataBase.Entities;
using ContractLibrary;

namespace WebApplication
{
    public static class TypesConverter
    {
        public static Book EntityToResponse(EntityBook entityBook)
        {
            return new Book()
            {
                Id = entityBook.Id,
                DateOfDelivery = entityBook.DateOfDelivery,
                Genre = entityBook.Genre.Name,
                IsNew = entityBook.IsNew,
                Price = entityBook.Price,
                Title = entityBook.Title
            };
        }

        public static async Task<EntityBook> RequestToEntityBook(Book book,
            BookContext context)
        {
            var genreId = await context.GetGenreId(book.Genre);
            return new EntityBook()
            {
                Price = book.Price,
                Title = book.Title,
                DateOfDelivery = book.DateOfDelivery,
                GenreId = genreId,
                IsNew = book.IsNew,
                ShopId = 1
            };
        }
    }
}