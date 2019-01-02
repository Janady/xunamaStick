using Libs.Singleton;
using System;
using Mod;

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
    public void insert(uint amount = 0, PaymentType type = PaymentType.Coin)
    {
        if (amount == 0) amount = _game;
        _amount += amount;
        Recharge charege = new Recharge
        {
            dateTime = DateTime.Now,
            amount = amount,
            payment = type
        };
        charege.insert();
        if (_gameTimeCallback != null) _gameTimeCallback(_amount);
    }
    public bool afford(uint amount = 0)
    {
        if (amount == 0) amount = _game;
        return (_amount >= amount);
    }
    public bool consume(int cabinetId, bool doGame = true, uint amount = 0)
    {
        if (amount == 0) amount = _game;
        if (_amount < amount) return false;
        _amount -= amount;
        Cabinet cabinet = Cabinet.GetById(cabinetId);
        int goodId = cabinet.GoodsId;
        Mod.Purchase purchase = new Mod.Purchase
        {
            dateTime = System.DateTime.Now,
            doGame = true,
            amount = amount,
            cabinetId = cabinetId,
            goodsId = goodId
        };
        purchase.insert();
        if (_gameTimeCallback != null) _gameTimeCallback(_amount);
        return true;
    }
}
