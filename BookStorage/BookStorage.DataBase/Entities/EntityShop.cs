using System.Collections.Generic;

namespace BookStorage.DataBase.Entities
{
    public class EntityShop
    {
        public int Id { get; set; }
        public List<EntityBook> Books { get; set; }
        public int StoreCapacity { get; set; }
        public int CurrentBookCount { get; set; }
        public List<EntityDiscounts> Discounts { get; set; }
        public decimal Money { get; set; }
        public decimal MinimumBookCountPercent { get; set; }
        public decimal CountMonthNotSoldBooksPercent { get; set; }
        public decimal SupplyPercent { get; set; }
    }
}