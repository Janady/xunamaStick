using SQLite4Unity3d;

namespace Mod
{
    public class Fixture
    {
        private SQLiteConnection connection;
        public Fixture()
        {
            connection = DataService.GetInstance().sQLiteConnection();
        }
        public void InitGoods()
        {
            connection.DropTable<Goods>();
            connection.CreateTable<Goods>();
            connection.InsertAll(new[] {
                new Goods {
                    Sku = "Dior/迪奥烈艳蓝金唇膏",
                    Title = "Dior/迪奥烈艳蓝金唇膏",
                    Price = 299,
                    Type = "999"
                },
                new Goods {
                    Sku = "Dior/迪奥烈艳蓝金唇膏",
                    Title = "Dior/迪奥烈艳蓝金唇膏",
                    Price = 299,
                    Type = "520"
                }
            });
        }
        public void InitCabinet()
        {
            connection.DropTable<Cabinet>();
            connection.CreateTable<Cabinet>();
            connection.InsertAll(new[] {
                new Cabinet {
                    Num = 1,
                    Title = "1号箱",
                    GoodsId = 1,
                    Enabled = true
                },
                new Cabinet {
                    Num = 2,
                    Title = "2号箱",
                    GoodsId = 2,
                    Enabled = false
                },
            });
        }
    }
}
