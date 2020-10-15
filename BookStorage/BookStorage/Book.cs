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
        private Queue<decimal> _coasts = new Queue<decimal>();
        
        public Guid BookId { get; }
        public string BookName { get; }
        public Genre Genre { get; }
        public decimal Price => _coasts.Peek();
        public string Author { get; }
        public DateTime SupplyData { get; }
        public bool IsNew{get => (DateTime.Now.Month - SupplyData.Month) < 1;}
 
        public Book(Guid bookId, string bookName, decimal price, Genre genre, string author, 
            DateTime supplyData)
        {
            BookId = bookId;
            BookName = bookName;
            _coasts.Enqueue(price);
            Genre = genre;
            Author = author;
            SupplyData = supplyData;
        }
        
        public void ApplyDiscount(decimal discount)
        {
            _coasts.Enqueue(_coasts.Peek() * (100m - discount));
        }
        
    }
}