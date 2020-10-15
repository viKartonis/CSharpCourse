using System;
using NUnit.Framework;

namespace BookStorage.Tests
{
    public class BookTest
    {
        [Test]
        public void ApplyDiscount()
        {
            var book = new Book(Guid.NewGuid(), "Gone with the wind", 1000, Genre.Drama, 
                "Margaret Mitchell", new DateTime(2020, 10, 15));
            
        }
    }
}