using System;
using System.ComponentModel.DataAnnotations;

namespace BookStorage.DataBase
{
    public class EntityBook
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
}