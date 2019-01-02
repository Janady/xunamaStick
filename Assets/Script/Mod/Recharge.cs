using System;
using System.Collections.Generic;

namespace Mod
{
    public enum PaymentType
    {
        Coin = 0,
        Network
    }
    public class Recharge : Model
    {
        public DateTime dateTime { get; set; }
        public uint amount { get; set; }
        public PaymentType payment { get; set; }
        public static IEnumerable<Recharge> All()
        {
            return connection.Table<Recharge>();
        }

        public static IEnumerable<Recharge> during(DateTime from)
        {
            return during(from, DateTime.Now);
        }
        public static IEnumerable<Recharge> during(DateTime from, DateTime to)
        {
            return connection.Table<Recharge>().Where(x => x.dateTime >= from && x.dateTime <= to);
        }
        public override string ToString()
        {
            return string.Format("[Recharge: Id={0}, dateTime={1}, payment={2}, amount={3}]", Id, dateTime, payment, amount);
        }
    }
}
