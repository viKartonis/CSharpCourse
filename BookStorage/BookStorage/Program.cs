using System.Collections.Generic;
using System.IO;

namespace BookStorage
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var bookStorage = new BookStore();
            var books = new List<Book>();
            
            using (var r = new StreamReader("BookBatch.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Item>>(json);
            }
            
            bookStorage.GetCurrentModey();
            bookStorage.AddBooks(books);
        }
    }
}