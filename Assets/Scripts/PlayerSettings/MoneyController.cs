using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
   
   [SerializeField] private int _money;
   [SerializeField] private CurrencyTypes _chosenCurrencyType;
   private enum CurrencyTypes {USD, Euro}
   private string _formatedMoney;
   private TMP_Text _moneyTextComponent;

   private void Start()
   {
      _moneyTextComponent = GameObject.FindWithTag("MoneyHandlerUI").GetComponent<TMP_Text>();
      _moneyTextComponent.text = FormateMoney(_money, _chosenCurrencyType);
   }
   

   private string FormateMoney(int valueOfMoney, CurrencyTypes currency)
   {
      char currencyCh = GetCurrencyChar(currency);
      string stringToReturn = valueOfMoney.ToString();
      
      if(valueOfMoney > 999)
      {
         int charsAmount = 0;
         for(int i = stringToReturn.Length - 1; i >= 0; i--)
         {
            if(stringToReturn[i] != '.' && (i - 1 != 0 || i != 0)) charsAmount++;
            if(charsAmount == 3)
            {
               stringToReturn = stringToReturn.Substring(0,i) + '.' + stringToReturn.Substring(i);
               charsAmount = 0;
            }
         }
      }
      return $"{currencyCh}{stringToReturn}";
   }

   private char GetCurrencyChar(CurrencyTypes currency)
   {
      switch (currency)
      {
         case CurrencyTypes.USD:
            return '$';
         case CurrencyTypes.Euro:
            return '€';
         default:
            return '$';
      }
   }
}
