using System.Collections.Generic;

namespace BookStorage.DataBase.Entities
{
    public class EntityGenre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public List<EntityBook> Books { get; set; }
        public List<EntityDiscounts> Discounts { get; set; }

    }
}