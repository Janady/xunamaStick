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
        public void InitRecharge()
        {
            connection.DropTable<Recharge>();
            connection.CreateTable<Recharge>();
            List<Recharge> allCabinet = new List<Recharge>();
            for (int i = 0; i < 64; i++)
            {
                Recharge c = new Recharge
                {
                    amount = (uint)UnityEngine.Random.Range(2, 10),
                    dateTime = DateTime.Now.AddDays(UnityEngine.Random.Range(-5, 0)),
                    payment = UnityEngine.Random.Range(0,1f) > 0.5 ? PaymentType.Coin : PaymentType.Network
                };
                allCabinet.Add(c);
            }
            connection.InsertAll(allCabinet);
        }
        public void InitPurchase()
        {
            connection.DropTable<Purchase>();
            connection.CreateTable<Purchase>();
            List<Purchase> allCabinet = new List<Purchase>();
            for (int i = 0; i < 64; i++)
            {
                Purchase c = new Purchase
                {
                    amount = (uint)UnityEngine.Random.Range(2, 10),
                    dateTime = DateTime.Now.AddDays(UnityEngine.Random.Range(-5, 0)),
                    doGame = UnityEngine.Random.Range(0, 1f) > 0.5 ? true : false,
                    goodsId = UnityEngine.Random.Range(1, 15),
                    cabinetId = UnityEngine.Random.Range(1,64)
                };
                allCabinet.Add(c);
            }
            connection.InsertAll(allCabinet);
        }
    }
}