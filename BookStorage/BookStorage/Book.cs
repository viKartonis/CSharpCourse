using System;

namespace BookStorage
{
    public enum Genre
    {
        Action,
        Adventure,
        Crime, 
        Drama,
        Fairytale
    }
    public class Book
    {
        public Guid _bookId { get; private set; }
        public string _bookName { get; private set; }
        public decimal _coast { get; private set; }
        public Genre _genre { get; private set; }
        
        public Book(Guid bookId, string bookName, decimal coast, Genre genre)
        {
            _bookId = bookId;
            _bookName = bookName;
            _coast = coast;
            _genre = genre;
        }
        
    }
}