using System;
using System.Collections.Generic;

namespace Mod
{
    public class Purchase : Model
    {
        public DateTime dateTime { get; set; }
        public bool doGame { get; set; }
        public int goodsId { get; set; }
        public int cabinetId { get; set; }
        public float amount { get; set; }

        public static IEnumerable<Purchase> All()
        {
            return connection.Table<Purchase>();
        }
        public static IEnumerable<Purchase> during(DateTime from) {
            return during(from, DateTime.Now);
        }
        public static IEnumerable<Purchase> during(DateTime from, DateTime to)
        {
            return connection.Table<Purchase>().Where(x => x.dateTime >= from && x.dateTime <= to);
        }

        public override string ToString()
        {
            return string.Format("[Purchase: Id={0}, dateTime={1}, goodsId={2}, cabinetId={3}, amount={4}]", Id, dateTime, goodsId, cabinetId, amount);
        }
    }
}
