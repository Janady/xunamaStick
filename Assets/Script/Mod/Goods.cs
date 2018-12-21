using SQLite4Unity3d;
using System.Collections.Generic;

namespace Mod
{
    public class Goods : Model
    {
        public string Sku { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int Price { get; set; }


        public static IEnumerable<Goods> AllGoods()
        {
            return connection.Table<Goods>();
        }

        public static Goods GetGood(int id)
        {
            return connection.Find<Goods>(id);
            //return connection.Table<Goods>().Where(x => x.Title == "Johnny").FirstOrDefault();
        }

        public override string ToString()
        {
            return string.Format("[Person: Id={0}, Sku={1}, Title={2}, ImagePath={3}, Price={4}]", Id, Sku, Title, ImagePath, Price);
        }
    }
}