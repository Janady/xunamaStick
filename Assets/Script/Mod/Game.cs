using SQLite4Unity3d;

namespace Mod
{
    public class Game : Model
    {
        public const int defaultRatio = 100;
        public const int defaultLucky = 0;
        public const int defaultOffset = 0;
        public const int defaultPrice = 1;
        /*
         * 0-200, default 100;
         */
        public int ratio { get; set; }

        /*
         * Add with play game,
         * Clear after wining game
         * 
         * default 0;
         */
        public int lucky { get; set; }

        /*
         * Winning impact this,
         * value = price - lucky;
         * default 0;
         */
        public int offset { get; set; }
        /*
         * Winning impact this,
         * value = price - lucky;
         * default 1;
         */
        public int price { set; get; }

        public static Game get()
        {
            Game g = connection.Find<Game>(1);
            if (g == null)
            {
                g = new Game
                {
                    ratio = defaultRatio,
                    lucky = defaultLucky,
                    offset = defaultOffset,
                    price = defaultPrice
                };
                g.insert();
            }
            return g;
        }
        public static void clear()
        {
            Game g = get();
            g.ratio = defaultRatio;
            g.lucky = defaultLucky;
            g.offset = defaultOffset;
            g.price = defaultPrice;
            g.update();
        }
    }
}