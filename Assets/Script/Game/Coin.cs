using Libs.Singleton;
using System;

public delegate int CoinCallback();
public class Coin
{
    private  uint _amount;
    private uint _game = 2;
    private Action<uint> _gameTimeCallback;
    public static Coin GetInstance()
    {
        return SingletonProvider<Coin>.Instance;
    }
    public void GameTimeCallback(Action<uint> value)
    {
            _gameTimeCallback = value;
    }
    public void insert(uint amount = 0)
    {
        if (amount == 0) amount = _game;
        _amount += amount;
        if (_gameTimeCallback != null) _gameTimeCallback(_amount);
    }
    public bool consume(uint amount = 0)
    {
        if (amount == 0) amount = _game;
        if (_amount < amount) return false;
        _amount -= amount;
        if (_gameTimeCallback != null) _gameTimeCallback(_amount);
        return true;
    }
}
