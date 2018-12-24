using SQLite4Unity3d;
using System.Collections.Generic;

namespace Mod
{
    public class Cabinet : Model
    {
        public int Num { get; set; }
        public string Title { get; set; }
        [Indexed]
        public int GoodsId { get; set; }
        public bool Enabled { get; set; }

        public static IEnumerable<Cabinet> All()
        {
            return connection.Table<Cabinet>();
        }

        public static Cabinet GetById(int id)
        {
            return connection.Find<Cabinet>(id);
        }

        public Goods Good()
        {
            return connection.Find<Goods>(GoodsId);
        }

        public override string ToString()
        {
            return string.Format("[Person: Id={0}, Num={1}, Title={2}, Good={3}, Enabled={4}]", Id, Num, Title, Good(), Enabled);
        }
    }
}