using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTextChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyTextComponent;
    private string _formatedMoney;
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMoneyAmountChanged>(MoneyChangesHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMoneyAmountChanged>(MoneyChangesHandler);
    }

    private void MoneyChangesHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnMoneyAmountChanged onMoneyAmountChanged)
        {
            UpdateMoneyText(onMoneyAmountChanged.BalanceAfterTransaction);
        }
    }
    private void UpdateMoneyText(int balance)
    {
        int moneyTextLimit = 999999999;
        
        bool isBeyondMoneyLimit = balance > moneyTextLimit;
        bool isBelowMoneyLimit = balance < 0;
        
        balance = isBeyondMoneyLimit ? moneyTextLimit : balance;
        if (isBelowMoneyLimit) balance = 0;
        _moneyTextComponent.text = FormatMoneyText(balance);
    }
    
    private string FormatMoneyText(int valueOfMoney)
    {
        string stringToReturn = valueOfMoney.ToString();
      
        if(valueOfMoney > 999)
        {
            int charsAmount = 0;
            for(int i = stringToReturn.Length - 1; i >= 0; i--)
            {
                if(stringToReturn[i] != '.' && (i - 1 != 0 || i != 0)) charsAmount++;
                if(charsAmount == 3 && i != 0)
                {
                    stringToReturn = stringToReturn.Substring(0,i) + '.' + stringToReturn.Substring(i);
                    charsAmount = 0;
                }
            }
        }
        return $"{stringToReturn}";
    }
    
}
