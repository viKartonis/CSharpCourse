using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BookStorage
{
    public class BookStore
    {
        private List<Book> _books = new List<Book>();
        private int _storeCapacity;
        private int _currentBooksCount;
        private double _discount;
        public decimal Money { get; private set;  }
        private decimal _supplyPercent;
        private decimal _minimumBookCountPercent;
        private decimal _countMonthNotSoldBooksPercent;

        public IReadOnlyList<Book> Books => _books;
        public decimal SupplyPercent { get => _supplyPercent; set => _supplyPercent = value / 100m;  }
        public decimal MinimumBookCountPercent { get => _minimumBookCountPercent; 
            set => _minimumBookCountPercent = value / 100m;  }
        public decimal CountMonthNotSoldBooksPercent { get => _countMonthNotSoldBooksPercent; 
            set => _countMonthNotSoldBooksPercent = value / 100m;  }
        public BookStore(decimal money, decimal supplyPercent, decimal countMonthNotSoldBooksPercent, 
            decimal minimumBookCountPercent, int storeCapacity)
        {
            Money = money;
            SupplyPercent = supplyPercent;
            CountMonthNotSoldBooksPercent = countMonthNotSoldBooksPercent;
            MinimumBookCountPercent = minimumBookCountPercent;
            _storeCapacity = storeCapacity;
        }
        
        private void AddBook(Book book)
        {
            _currentBooksCount++;
            _books.Add(book);
            Money -= book.Price*_supplyPercent;
        }
        
        public void AddBooksInStore(IReadOnlyList<Book> books)
        {
            foreach (var book in books)
            {
                AddBook(book);   
            }
        }

        public void BuyBookByCustomer(Book book)
        {
            _currentBooksCount--;
            Money += book.Price;
            _books.Remove(book);
        }

        public void ApplyDiscount(decimal discount, Genre genre, DateTime dateTime)
        {
            foreach (var book in _books)
            {
                if (book.Genre == genre && !book.IsNew(dateTime))
                {
                    book.ApplyDiscount(discount);
                }
            }
        }

        private int CountMonthNotSoldBooks(DateTime dateTime)
        {
            var count = 0;
            foreach (var book in _books)
            {
                if (!book.IsNew(dateTime))
                {
                    count++;
                }
            }

            return count;
        }
        public void BooksOrdering(DateTime dateTime, List<Book> books)
        {
            if (_currentBooksCount <= _storeCapacity * MinimumBookCountPercent ||
                CountMonthNotSoldBooks(dateTime) >= _storeCapacity * CountMonthNotSoldBooksPercent)
            {
                AddBooksInStore(books);
            }
        }

    }
}