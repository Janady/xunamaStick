using System;
using Mod;
using Libs.Frame;
/*
 * open lock: EA 06 type 03 num FA
 * 
 */
namespace Service
{
    public class LockingPlateService
    {
        public enum OpenType
        {
            None = (byte)0x00,
            Select = (byte)0x01,
            Sold = (byte)0x02,
            Replenishment = (byte)0x03
        }
        private byte openCmd = 0x03;
        private byte len = 6;
        public static LockingPlateService Instance()
        {
            return Libs.Singleton.SingletonProvider<LockingPlateService>.Instance;
        }
        public LockingPlateService()
        {
        }
        public void openLock(Cabinet cabinet, OpenType type)
        {
            Libs.Api.SerailApi.serialTransfer(Frame.packet((byte)type, openCmd, (byte)cabinet.Id));
        }
        public void OpenAll()
        {
            foreach (Cabinet c in Cabinet.All())
            {
                openLock(c, OpenType.Select);
            }
        }
    }
}
