using Mod;
using Libs.Singleton;

public class GoodsService
{
    public static GoodsService Instance()
    {
        return SingletonProvider<GoodsService>.Instance;
    }

}