using System;
using System.Collections.Generic;
using UnityEngine;

public enum OperationTypes {Plowing, Planting, SellingCrop}

public class MoneyController : MonoBehaviour, IDifficultyDepended
{
   [SerializeField] private int _amountOfMoneyOnStart = 0;
   [SerializeField] private int _currentMoneyAmount;
   private int _moneyToProvide;
   
   private Vector2 _positionToMessage;
   
   private Dictionary<OperationTypes, float> _operationCosts = new Dictionary<OperationTypes, float>()
   {
      { OperationTypes.Plowing, -500f },
      { OperationTypes.Planting, -100f },
      { OperationTypes.SellingCrop, 0.1f } // per unit
   };

   public void AdjustDifficultyDependedProperties()
   {
      int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());
      
      _amountOfMoneyOnStart /= difficulty;
   }

   private void Awake()
   {
      AdjustDifficultyDependedProperties();
   }

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(ProcessPlowingTransaction);
      GlobalEventBus.Sync.Subscribe<OnCropCollected>(ProcessCroppingTransaction);
      GlobalEventBus.Sync.Subscribe<OnTilePlanted>(ProcessPlantingTransaction);
      GlobalEventBus.Sync.Subscribe<OnMoneyProvided>(ProcessMoneyProvidedSignal);
   }

   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnGrassPlowed>(ProcessPlowingTransaction);
      GlobalEventBus.Sync.Unsubscribe<OnCropCollected>(ProcessCroppingTransaction);
      GlobalEventBus.Sync.Unsubscribe<OnTilePlanted>(ProcessPlantingTransaction);
      GlobalEventBus.Sync.Unsubscribe<OnMoneyProvided>(ProcessMoneyProvidedSignal);
   }
   
   private void Start()
   {
      if (PlayerPrefs.GetInt("EndlessMoneyModeStatus") == 1)
      {
         SetStartAmountMoney(2147483647);
      }
      else
      {
         SetStartAmountMoney(_amountOfMoneyOnStart);
      }
      
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_currentMoneyAmount));
   }

   private void SetStartAmountMoney(int amount)
   {
      _currentMoneyAmount = amount;
   }

   private void ProcessMoneyProvidedSignal(object sender, EventArgs eventArgs)
   {
      OnMoneyProvided onMoneyProvided = (OnMoneyProvided)eventArgs;
      ChangeMoneyAmount(onMoneyProvided.AmountOfProvidedMoney);
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_currentMoneyAmount));
   }

   private void ProcessPlowingTransaction(object sender, EventArgs eventArgs)
   {
      OnGrassPlowed onGrassPlowed = (OnGrassPlowed)eventArgs; 
      
      _moneyToProvide = (int)_operationCosts[OperationTypes.Plowing];
      _positionToMessage = onGrassPlowed.PlowedTile.transform.position;
      
      ChangeMoneyAmount(_moneyToProvide);
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_currentMoneyAmount, _positionToMessage,_moneyToProvide));
   }

   private void ProcessCroppingTransaction(object sender, EventArgs eventArgs)
   {
      OnCropCollected onCropCollected = (OnCropCollected)eventArgs;
      
      _moneyToProvide = (int)(onCropCollected.AmountOfCollectedCrop * _operationCosts[OperationTypes.SellingCrop]);
      _positionToMessage = onCropCollected.CollectedFromTile.transform.position;
      
      ChangeMoneyAmount(_moneyToProvide);
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_currentMoneyAmount, _positionToMessage,_moneyToProvide));
   }

   private void ProcessPlantingTransaction(object sender, EventArgs eventArgs)
   {
      OnTilePlanted onTilePlanted = (OnTilePlanted)eventArgs;
      
      _moneyToProvide = (int)_operationCosts[OperationTypes.Planting];
      _positionToMessage = onTilePlanted.PlantedTile.transform.position;
      
      ChangeMoneyAmount(_moneyToProvide);
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_currentMoneyAmount, _positionToMessage,_moneyToProvide));
   }
   private void ChangeMoneyAmount(int amountToProvide)
   {
      _currentMoneyAmount += amountToProvide;
   }

   public bool CheckOperationProcessability(OperationTypes operationTypes, Transform positionOfChecking)
   {
      bool isEnoughMoney = _currentMoneyAmount + _operationCosts[operationTypes] >= 0;
      if (!isEnoughMoney)
      { 
         GlobalEventBus.Sync.Publish(this, new OnMoneyTransactionFailed(positionOfChecking));
      }
      return ((_currentMoneyAmount + _operationCosts[operationTypes]) >= 0);
   }

   public bool CheckIsEnoughMoney(int amountToTake)
   {
      return _currentMoneyAmount - amountToTake >= 0;
   }
}
