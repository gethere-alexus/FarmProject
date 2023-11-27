using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.FullSerializer;

public enum OperationTypes {Plowing, Planting}
public class MoneyController : MonoBehaviour
{
   private Dictionary<OperationTypes, int> _operationCosts = new Dictionary<OperationTypes, int>()
   {
      {OperationTypes.Plowing, 500 },
      {OperationTypes.Planting, 100}
   };
   
   [SerializeField] private int _money;
   
   private int _moneyLimit = 999999999;
   private float _costOfCropUnit = 0.1f;
   
   private string _formatedMoney;
   
   private TMP_Text _moneyTextComponent;

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(MoneyHandler);
      GlobalEventBus.Sync.Subscribe<OnCropCollected>(MoneyHandler);
   }

   private void MoneyHandler(object sender, EventArgs eventArgs)
   {
      if (eventArgs is OnGrassPlowed onGrassCultivated)
      {
         SpendMoney(_operationCosts[OperationTypes.Plowing]);
      }
      else if (eventArgs is OnCropCollected onCropCollected)
      {
         int moneyToGive = (int)(onCropCollected.AmountOfCollectedCrop * _costOfCropUnit);
         GiveMoney(moneyToGive);
      }
   }

   private void SpendMoney(int amount)
   {
      _money -= amount;
      UpdateMoneyText();
   }

   private void GiveMoney(int amount)
   {
      _money += amount;
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
      bool isBeyondMoneyLimit = _money > _moneyLimit;
      bool isBelowMoneyLimit = _money < 0;
      _money = isBeyondMoneyLimit ? _moneyLimit : _money;
      if (isBelowMoneyLimit) _money = 0;
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
            if(charsAmount == 3 && i != 0)
            {
               stringToReturn = stringToReturn.Substring(0,i) + '.' + stringToReturn.Substring(i);
               charsAmount = 0;
            }
         }
      }
      return $"${stringToReturn}";
   }
   
}
