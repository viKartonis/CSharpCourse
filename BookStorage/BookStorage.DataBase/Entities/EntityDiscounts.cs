using System.Collections.Generic;

namespace BookStorage.DataBase.Entities
{
    public class EntityDiscounts
    {
        public int Id { get; set; }
        public decimal Value { get; set; }

        public virtual EntityShop Shop { get; set; }
        public int ShopId { get; set; }
        public virtual EntityGenre Genre { get; set; }
        public int GenreId { get; set; }
    }
}