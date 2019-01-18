using Mod;
using Libs.Singleton;

namespace Service
{
    public class GoodsService
    {
        public static GoodsService Instance()
        {
            return SingletonProvider<GoodsService>.Instance;
        }
        public void Replenishment()
        {
            foreach (Cabinet c in Cabinet.All())
            {
                if (c.Good() != null && c.Count == 0)
                {
                    c.Count++;
                    c.update();
                }
            }
        }
    }
}
