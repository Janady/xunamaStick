using UnityEngine;
using Mod;

public class GameFacts
{
    private readonly int[] CountForlevel = { 5, 7, 10, 14 };
    private readonly int[] SpeedForlevel = { 80, 120, 180, 300 };
    private int level;
    private int count;
    private int speed;
    private float _speed;
    private int calibrate; // 0-100,
    private int luckyRate = 60;
    public GameFacts(int level, int cabinetId)
    {
        Cabinet c = Cabinet.GetById(cabinetId);
        int price = 0;
        if (c != null)
        {
            Goods good = c.Good();
            if (good != null) price = good.Price;
        }
        Game g = Game.get();
        Debug.Log("lucky:"+ g.lucky + " offset:" + g.offset + " ratio:" + g.ratio + " once:" + g.price + " price:" + price);// price
        calibrate = price / g.price * g.ratio / 100 - g.lucky * luckyRate / 100 + g.offset;
        if (calibrate > 100) calibrate = 100;
        if (calibrate < 0) calibrate = 0;
        this.level = level;
        this.count = CountForlevel[level] + (int)(calibrate * (CountForlevel[level + 1] - CountForlevel[level]) / 100);
        speed = SpeedForlevel[level] + (calibrate * (SpeedForlevel[level + 1] - SpeedForlevel[level]) / 100);
        _speed = speed;
    }

    public int Level
    {
        get
        {
            return level;
        }
    }

    public int Count
    {
        get
        {
            return count;
        }
    }
    private readonly float[] radio = {1.1f, 1f, 0.9f, -0.9f, -1f, -1.1f };
    public float Speed
    {
        get
        {
            if (Random.Range(0, 100) < calibrate)
            {
                _speed = speed * radio[Random.Range(0, 6)];
                Debug.Log("change speed:" + _speed + System.DateTime.Now.ToString());
            }
            return _speed;
        }
    }
}