using SQLite4Unity3d;
using System.Collections.Generic;
using System;

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

            List<Cabinet> allCabinet = new List<Cabinet>();
            for (int i=0; i<64; i++) {
                Cabinet c = new Cabinet
                {
                    Num = i,
                    Title = i + "号箱",
                    GoodsId = new Random(Guid.NewGuid().GetHashCode()).Next(1,3),
                    Enabled = true
                };
                allCabinet.Add(c);
            }
            connection.InsertAll(allCabinet);
        }
    }
}