using System;

namespace WebApplication
{
    public class BookResponse
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
}