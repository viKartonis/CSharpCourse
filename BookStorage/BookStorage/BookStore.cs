using System;
using System.Collections;
using System.Collections.Generic;

namespace BookStorage
{
    public class BookStore
    {
        private List<Book> _books { get; set; }
        private int _saled { get; set; }
        private decimal _moneyStorage { get; set;  }
        private double _discount { get; set; }

        public BookStore()
        {
        }
        
        private void AddBook(Book book)
        {
            _books.Add(book);
        }
        
        public void AddBooks(List<Book> books)
        {
            foreach (var book in books)
            {
                AddBook(book);   
            }
        }

        public void BuyBook(Book book)
        {
            _saled++;
            _moneyStorage += book._coast;
            _books.Remove(book);
        }

        public decimal GetCurrentModey()
        {
            return _moneyStorage;
        }
        
    }
}