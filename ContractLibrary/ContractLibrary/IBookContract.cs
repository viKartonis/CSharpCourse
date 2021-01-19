using System;
using System.Collections.Generic;

namespace ContractLibrary
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
    public class IBookContract
    {
        public List<Book> Books { get; set; }
    }
}