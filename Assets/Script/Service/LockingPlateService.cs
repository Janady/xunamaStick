using System;
namespace Service
{
    public class LockingPlateService
    {
        public static LockingPlateService GetInstance()
        {
            return Libs.Singleton.SingletonProvider<LockingPlateService>.Instance;
        }
        public LockingPlateService()
        {
        }
    }
}
