using System;

using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] TMPro.TextMeshProUGUI text;

    public void Add(int moneyGain)
    {
        amount += moneyGain;
        UpdateText();   
    }

    internal bool Check(int totalPrice)
    {
        return amount >= totalPrice;
    }

    internal void Decrease(int totalPrice)
    {
        amount -= totalPrice;
        if(amount < 0)
        {
            amount = 0;
        }
        UpdateText();
    }

    private void Start()
    {
        amount = 99999;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = amount.ToString();
    }
}
