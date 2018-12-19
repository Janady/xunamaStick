using SQLite4Unity3d;

namespace Mod
{
    public class Goods
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return string.Format("[Person: Id={0}, Sku={1}, Title={2}, ImagePath={3}, Price={4}]", Id, Sku, Title, ImagePath, Price);
        }
    }
}