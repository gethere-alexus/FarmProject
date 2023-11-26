using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum OperationTypes {Plowing, Planting}
public class MoneyController : MonoBehaviour
{
   private Dictionary<OperationTypes, int> _operationCosts = new Dictionary<OperationTypes, int>()
   {
      {OperationTypes.Plowing, 500 },
      {OperationTypes.Planting, 100}
   };
   
   [SerializeField] private int _money;
   private string _formatedMoney;
   private TMP_Text _moneyTextComponent;

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(MoneyHandler);
   }

   private void MoneyHandler(object sender, EventArgs eventArgs)
   {
      if (eventArgs is OnGrassPlowed onGrassCultivated)
      {
         SpendMoney(_operationCosts[OperationTypes.Plowing]);
      }
   }

   private void SpendMoney(int amount)
   {
      _money -= amount;
      UpdateMoneyText();
   }

   public bool CheckOperationProcessability(OperationTypes operationTypes)
   {
      return (_money - _operationCosts[operationTypes]) >= 0 ? true : false;
   }
   private void Start()
   {
      
      _moneyTextComponent = GameObject.FindWithTag("MoneyHandlerUI").GetComponent<TMP_Text>();
      UpdateMoneyText();
   }

   private void UpdateMoneyText()
   {
      _moneyTextComponent.text = FormateMoney(_money);
   }

   private string FormateMoney(int valueOfMoney)
   {
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
      return $"${stringToReturn}";
   }
   
}
