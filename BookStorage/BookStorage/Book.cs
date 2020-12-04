using System;
using System.Collections.Generic;

namespace BookStorage
{
    #warning в С# обычно принято каждый тип помещать в отдельный файл
    #warning в случае доменных объектов (а жанр и книга у тебя именно они) это всегда так
    #warning бывают случаю,когда несколько классов есть смысл размещать в одном файле: например, когда объявляешь контракт для фронтенда
    #warning в котором у тебя много разных объектов; нет смысла их разделять по файлам, т.к. они могут существовать только совместно
    #warning тут же лучше разделить на файлы класс и енам
    public enum Genre
    {
        #warning при объявлении енама неявно подразумевается, что значения начинаются с нуля
        #warning хорошей практикой является явно проставлять нумерацию и начинать её с единицы
        #warning можешь почитать вот тут обсуждение https://stackoverflow.com/a/7257458/9665365
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