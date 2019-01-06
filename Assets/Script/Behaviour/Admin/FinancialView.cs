using UnityEngine;
using UnityEngine.UI;
using System;
using Mod;

public class FinancialView : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Text fromText = transform.FindChild("Content").FindChild("dateFrom").GetComponent<Text>();
        Text toText = transform.FindChild("Content").FindChild("dateTo").GetComponent<Text>();
        DateTime dateTo = DateTime.Now;
        DateTime dateFrom = DateTime.Now.AddMonths(-1);
        fromText.text = dateFrom.ToString("yyyy-MM-dd");
        toText.text = dateTo.ToShortDateString();
        uint onlineSum = 0;
        uint coinSum = 0;
        foreach (Recharge r in Recharge.during(dateFrom, dateTo))
        {
            switch (r.payment)
            {
                case PaymentType.Coin:
                    coinSum += r.amount;
                    break;
                case PaymentType.Network:
                    onlineSum += r.amount;
                    break;
            }
        }
        Text onlineText = transform.FindChild("Content").FindChild("online").GetComponent<Text>();
        onlineText.text = "￥" + onlineSum.ToString();
        Text coinText = transform.FindChild("Content").FindChild("coin").GetComponent<Text>();
        coinText.text = "￥" + coinSum.ToString();
        uint gameSum = 0;
        foreach (Purchase p in Purchase.during(dateFrom, dateTo))
        {
            if (p.doGame)
            {
                gameSum += p.amount;
            }
        }
        Text gameText = transform.FindChild("Content").FindChild("game").GetComponent<Text>();
        gameText.text = "￥" + gameSum.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
