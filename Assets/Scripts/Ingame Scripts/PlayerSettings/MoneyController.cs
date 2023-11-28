using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;

public enum OperationTypes {Plowing, Planting, SellingCrop}
public class MoneyController : MonoBehaviour
{
   private Dictionary<OperationTypes, float> _operationCosts = new Dictionary<OperationTypes, float>()
   {
      {OperationTypes.Plowing, -500f},
      {OperationTypes.Planting, -100f},
      {OperationTypes.SellingCrop, 0.1f} // per unit
   };
   
   [SerializeField] private int _money;

   private GameObject _moneyChangedPrefab;
   private GameObject _notEnoughMoneyPrefab;
   
   private int _moneyLimit = 999999999;
   
   private string _formatedMoney;
   private TMP_Text _moneyTextComponent;

   private void OnEnable()
   {
      _moneyChangedPrefab = Resources.Load<GameObject>("Prefabs/Particles/MoneyChanges");
      _notEnoughMoneyPrefab = Resources.Load<GameObject>("Prefabs/Particles/NotEnoughMoney");
      
      GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(MoneyHandler);
      GlobalEventBus.Sync.Subscribe<OnCropCollected>(MoneyHandler);
      GlobalEventBus.Sync.Subscribe<OnTilePlanted>(MoneyHandler);
   }

   private void MoneyHandler(object sender, EventArgs eventArgs)
   {
      GameObject moneyPrefab = Instantiate(_moneyChangedPrefab);
      MoneyChangesConfigurator moneyChangesConfigurator = moneyPrefab.GetComponent<MoneyChangesConfigurator>();
      
      int moneyToProvide = 0;
      
      if (eventArgs is OnGrassPlowed onGrassCultivated)
      {
         moneyToProvide = (int)_operationCosts[OperationTypes.Plowing];
         moneyPrefab.transform.position = onGrassCultivated.PlowedTile.transform.position;
      }
      else if (eventArgs is OnCropCollected onCropCollected)
      {
         moneyToProvide = (int)(onCropCollected.AmountOfCollectedCrop * _operationCosts[OperationTypes.SellingCrop]);
         moneyPrefab.transform.position = onCropCollected.CollectedFromTile.transform.position;
      }
      else if (eventArgs is OnTilePlanted onTilePlanted)
      {
         moneyToProvide = (int)_operationCosts[OperationTypes.Planting];
         moneyPrefab.transform.position = onTilePlanted.PlantedTile.transform.position;
      }
      
      ChangeMoneyAmount(moneyToProvide);
      moneyChangesConfigurator.SetAmountOfChangedMoney(moneyToProvide);
   }

   private void ChangeMoneyAmount(int amount)
   {
      _money += amount;
      UpdateMoneyText();
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged());
   }

   public bool CheckOperationProcessability(OperationTypes operationTypes, Transform positionOfChecking)
   {
      bool isEnoughMoney = _money + _operationCosts[operationTypes] >= 0;
      if (!isEnoughMoney)
      { 
         GlobalEventBus.Sync.Publish(this, new OnMoneyTransactionFailed());
         GameObject errorMessage = Instantiate(_notEnoughMoneyPrefab, positionOfChecking);
         errorMessage.transform.rotation = quaternion.identity;
         errorMessage.transform.position = errorMessage.transform.position + new Vector3(0, .5f, 0);
      }
      return (_money + _operationCosts[operationTypes]) >= 0 ? true : false;
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
      _moneyTextComponent.text = FormatMoney(_money);
   }

   private string FormatMoney(int valueOfMoney)
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
