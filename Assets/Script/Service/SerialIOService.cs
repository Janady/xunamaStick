using System;
using Libs.Singleton;
using Libs.Event;

namespace Service
{
    public enum SerialCommand
    {
        PayInit = 0x01,
        CoinInsert = 0x02,
        AdminOpr = 0x06,


    }
    public class SerialIOService
    {
        private const int cmdPos = 3;
        private byte[] _payIdentity = null;
        public byte[] PayIdentity
        {
            get
            {
                return _payIdentity;
            }
        }
        public static SerialIOService GetInstance()
        {
            return SingletonProvider<SerialIOService>.Instance;
        }
        public SerialIOService()
        {
            Libs.Api.SerailApi.check();
        }
        public void Received(byte[] packet)
        {
            if (packet.Length <= cmdPos) return;
            switch ((SerialCommand)packet[cmdPos])
            {
                case SerialCommand.PayInit:
                    PayInitHandler(packet);
                    break;
                case SerialCommand.CoinInsert:
                    CoinInsertHandler(packet);
                    break;
                case SerialCommand.AdminOpr:
                    AdminOprHandler();
                    break;
            }
        }
        private void PayInitHandler(byte[] packet)
        {
            int datalen = (int)(packet[cmdPos + 1] & 0xff);
            byte[] data = new byte[datalen];
            Buffer.BlockCopy(packet, cmdPos + 2, data, 0, datalen);
            _payIdentity = data;
        }
        private void CoinInsertHandler(byte[] packet)
        {
            uint count = (uint)(packet[cmdPos + 1] & 0xff);
            Coin.GetInstance().insert(count);
            EventMgr.Instance.DispatchEvent(EventNameData.CoinInsert, count);
        }
        private void AdminOprHandler()
        {
            EventMgr.Instance.DispatchEvent(EventNameData.AdminOpr);
        }
    }
}
