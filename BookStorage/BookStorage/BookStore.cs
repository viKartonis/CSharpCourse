using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BookStorage
{
    public class BookStore
    {
        private List<Book> _books;
        private int _storeCapacity;
        private int _currentBooksCount;
        private double _discount;
        public decimal Money { get; private set;  }
        private decimal _supplyPercent;
        private decimal _minimumBookCountPercent;
        private decimal _countMonthNotSoldBooksPercent;

        public decimal SupplyPercent { get => _supplyPercent; set => _supplyPercent = value / 100m;  }
        public decimal MinimumBookCountPercent { get => _minimumBookCountPercent; 
            set => _minimumBookCountPercent = value / 100m;  }
        public decimal CountMonthNotSoldBooksPercent { get => _countMonthNotSoldBooksPercent; 
            set => _countMonthNotSoldBooksPercent = value / 100m;  }
        public BookStore()
        {
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

        public void ApplyDiscount(decimal discount, Genre genre)
        {
            foreach (var book in _books)
            {
                if (book.Genre == genre && !book.IsNew)
                {
                    book.ApplyDiscount(discount);
                }
            }
        }

        private int countMonthNotSoldBooks()
        {
            var count = 0;
            foreach (var book in _books)
            {
                if (!book.IsNew)
                {
                    count++;
                }
            }

            return count;
        }
        public void BooksOrdering(DateTime date, List<Book> books)
        {
            if (_currentBooksCount == _storeCapacity * _minimumBookCountPercent ||
                countMonthNotSoldBooks() == _storeCapacity * _countMonthNotSoldBooksPercent)
            {
                AddBooksInStore(books);
            }
        }

    }
}