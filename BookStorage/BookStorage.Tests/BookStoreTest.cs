using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BookStorage.Tests
{
    public class BookStoreTest
    {
        [Test]
        public void BooksOrdering()
        {
            var bookStore = new BookStore();
            var books = new List<Book>();
            var supplyDate = new DateTime(2020, 08, 23);
            books.Add(new Book(Guid.Empty, "Blue thread", 330,
                Genre.Crime, "David Linch", supplyDate
                ));
            bookStore.BooksOrdering(supplyDate, books);
            bookStore.ApplyDiscount(10, Genre.Crime);
            foreach (var i in bookStore.Books)
            {
                i.Price.Should().Be(297);
            }
        }

        [Test]
        public void BuyBookByCustomerTest()
        {
            var bookStore = new BookStore();
            var books = new List<Book>();
            var supplyDate = new DateTime(2020, 08, 23);
            books.Add(new Book(Guid.Empty, "Blue thread", 330,
                Genre.Crime, "David Linch", supplyDate
            ));
            books.Add(new Book(Guid.Empty, "What's happens?", 1556,
                Genre.Action, "Poklonskaya Anna", supplyDate));
            bookStore.BooksOrdering(supplyDate, books);
            bookStore.BuyBookByCustomer(books.Find(x => x.BookName == "What's happens?"));
            bookStore.Books.Count.Should().Be(1);
        }
        
    }
}