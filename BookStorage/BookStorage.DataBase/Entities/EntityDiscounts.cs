using System.Collections.Generic;

namespace BookStorage.DataBase.Entities
{
    public class EntityDiscounts
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public List<EntityShop> Shops { get; set; }
    }
}