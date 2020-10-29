using System;
using System.Collections.Generic;

namespace BookStorage
{
    public enum Genre
    {
        Action,
        Adventure,
        Crime, 
        Drama,
        Fantasy,
        Encyclopedia,
        Fairytale
    }
    public class Book
    {
        private Stack<decimal> _coasts = new Stack<decimal>();
        
        public Guid BookId { get; }
        public string BookName { get; }
        public Genre Genre { get; }
        public decimal Price => _coasts.Peek();
        public string Author { get; }
        public DateTime SupplyData { get; }

        public bool IsNew(DateTime dateTime)
        {
           return (dateTime.Month - SupplyData.Month) < 1;
        }
 
        public Book(Guid bookId, string bookName, decimal price, Genre genre, string author, 
            DateTime supplyData)
        {
            BookId = bookId;
            BookName = bookName;
            _coasts.Push(price);
            Genre = genre;
            Author = author;
            SupplyData = supplyData;
        }
        
        public void ApplyDiscount(decimal discount)
        {
            if (discount != 0 && discount <= 100)
            {
                _coasts.Push(_coasts.Peek() * (100m - discount)/100);
            }
        }
        
    }
}