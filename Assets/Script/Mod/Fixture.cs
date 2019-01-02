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
            connection.CreateTable<Purchase>();
            connection.CreateTable<Recharge>();
        }
        public void InitGoods()
        {
            connection.DropTable<Goods>();
            connection.CreateTable<Goods>();

            List<Goods> allGoods = new List<Goods>();
            for (int i = 0; i < 16; i++)
            {
                Goods g = new Goods
                {
                    Sku = "Dior" + i,
                    Title = "Dior/迪奥烈艳蓝金唇膏-" + i,
                    Price = UnityEngine.Random.Range(100,500),
                    Type = "999"
                };
                allGoods.Add(g);
            }
            connection.InsertAll(allGoods);
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
                    GoodsId = UnityEngine.Random.Range(1,16),
                    Enabled = true
                };
                allCabinet.Add(c);
            }
            connection.InsertAll(allCabinet);
        }
    }
}