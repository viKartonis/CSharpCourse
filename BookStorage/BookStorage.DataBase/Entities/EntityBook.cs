using System;

namespace BookStorage.DataBase.Entities
{
    public class EntityBook
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public virtual EntityGenre Genre { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public virtual EntityShop Shop { get; set; } 
        public int ShopId { get; set; }

    }
}