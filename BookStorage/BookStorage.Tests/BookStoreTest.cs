using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BookStorage.Tests
{
    public class BookStoreTest
    {
        // private BookStore _bookStore; 
        // private List<Book> _books;
        // private List<decimal> _prices;
        //
        //
        // [SetUp]
        // private void InitBookStore(int storeCapacity)
        // {
        //     _bookStore = new BookStore(10000, 10, 75,
        //         10, storeCapacity);
        //     _books = new List<Book>();
        //     _prices = new List<decimal>();
        //     var supplyDate = new DateTime(2020, 08, 23);
        //     _books.Add(new Book(Guid.Empty, "Blue thread", 330,
        //         Genre.Crime, "David Linch", supplyDate
        //     ));
        //     _prices.Add(297);
        //     _books.Add(new Book(Guid.Empty, "What's happens?", 1556,
        //         Genre.Action, "Poklonskaya Anna", new DateTime(2020, 10, 28)));
        //     _prices.Add(1556);
        //     _bookStore.BooksOrdering(supplyDate, _books);
        // }
        //
        // [Test]
        // public void BooksOrdering()
        // {
        //     //InitBookStore(2);
        //     _bookStore.ApplyDiscount(10, Genre.Crime, new DateTime(2020, 10, 29));
        //     for (var i = 0; i < _books.Count; ++i)
        //     {
        //         _books[i].Price.Should().Be(_prices[i]);
        //     }
        //     _bookStore.BooksOrdering(new DateTime(2020, 10, 20), _books);
        // }
        //
        // [Test]
        // public void BuyBookByCustomerTest()
        // {
        //     InitBookStore(100);
        //     _bookStore.BuyBookByCustomer(_books.Find(x => x.BookName == "What's happens?"));
        //     _bookStore.Books.Count.Should().Be(1);
        // }
        //
        // [Test]
        // public void CheckMoneyTest()
        // {
        //    InitBookStore(100);
        //     var allModey = 10000.0m;
        //     foreach (var book in _bookStore.Books)
        //     {
        //         allModey -= book.Price * _bookStore.SupplyPercent;
        //     }
        //     _bookStore.Money.Should().Be(allModey);
        // }
        //
        //
    }
}