﻿using Libs.Singleton;
using System;
using Mod;

public delegate int CoinCallback();
public class Coin
{
    private  uint _amount = 0;
    private Action<uint> _gameTimeCallback;
    public static Coin GetInstance()
    {
        return SingletonProvider<Coin>.Instance;
    }
    public Coin()
    {
    }
    public void GameTimeCallback(Action<uint> value)
    {
            _gameTimeCallback = value;
    }
    public void insert(uint amount = 0, PaymentType type = PaymentType.Coin)
    {
        if (amount == 0) amount = (uint)Mod.Game.get().coin;
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
    public uint coin()
    {
        return _amount;
    }
    public uint afford(uint amount = 0)
    {
        if (amount == 0) amount = (uint)Mod.Game.get().coin;
        return (_amount / amount);
    }
    public bool consume(int cabinetId, bool doGame = true, uint amount = 0)
    {
        Cabinet cabinet = Cabinet.GetById(cabinetId);
        if (amount == 0)
        {
            amount = doGame ? (uint)Mod.Game.get().coin : (uint)(cabinet.Good().gameCount);
        }
        if (_amount < amount) return false;
        _amount -= amount;
        int goodId = cabinet.GoodsId;
        Mod.Purchase purchase = new Mod.Purchase
        {
            dateTime = System.DateTime.Now,
            doGame = doGame,
            amount = amount * Game.get().price,
            cabinetId = cabinetId,
            goodsId = goodId
        };
        purchase.insert();
        if (_gameTimeCallback != null) _gameTimeCallback(_amount);
        return true;
    }
}
