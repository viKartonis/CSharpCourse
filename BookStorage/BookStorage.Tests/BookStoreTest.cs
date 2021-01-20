using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.DataBase;
using BookStorage.DataBase.Entities;
using BookStorage.Service;
using ContractLibrary;
using FluentAssertions;
using NUnit.Framework;
using WebApplication;
using WebApplication.DTO;

namespace BookStorage.Tests
{
    public class BookStoreTest
    {
        public const string DefaultSchemaName = "bookshop";
        private DataService _service;
        private readonly BookContext _context;
        private readonly List<Book> _books;
        private readonly decimal _money;

        [SetUp]
        public void InitService()
        {
            for (var i = 0; i < 3; ++i)
            {
                var book = new EntityBook()
                {
                    DateOfDelivery = new DateTime(2020, 08, 23),
                    Title = "Book" + i,
                    GenreId = i,
                    Id = i,
                    IsNew = true,
                    ShopId = i,
                    Price = 100 * i
                };
                _books.Add(TypesConverter.EntityToResponse(book));
            }
            AddData(_books);
        }
        
        [Test]
        public void GetData()
        {
            var list =  _service.GetData().Result;
            _books.Count.Should().Be(list.Count);
        }
        public void AddData(List<Book> bookRequest)
        {
            _service.AddData(bookRequest);
        }
        
    }
}