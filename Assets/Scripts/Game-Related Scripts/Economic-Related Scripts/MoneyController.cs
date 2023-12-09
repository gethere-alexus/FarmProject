using System;
using System.Collections.Generic;
using UnityEngine;

public enum OperationTypes {Plowing, Planting, SellingCrop}

public class MoneyController : MonoBehaviour
{
   private Dictionary<OperationTypes, float> _operationCosts = new Dictionary<OperationTypes, float>()
   {
      { OperationTypes.Plowing, -500f },
      { OperationTypes.Planting, -100f },
      { OperationTypes.SellingCrop, 0.1f } // per unit
   };

   [SerializeField] private int _moneyAmount;

   private int _moneyToProvide;
   private Vector2 _positionToMessage;

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(PlowingTransactionHandler);
      GlobalEventBus.Sync.Subscribe<OnCropCollected>(CroppingTransactionHandler);
      GlobalEventBus.Sync.Subscribe<OnTilePlanted>(PlantingTransactionHandler);
   }

   private void OnDisable()
   {

      GlobalEventBus.Sync.Unsubscribe<OnGrassPlowed>(PlowingTransactionHandler);
      GlobalEventBus.Sync.Unsubscribe<OnCropCollected>(CroppingTransactionHandler);
      GlobalEventBus.Sync.Unsubscribe<OnTilePlanted>(PlantingTransactionHandler);
   }

   private void Start()
   {
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(0, _moneyAmount));
   }

   private void PlowingTransactionHandler(object sender, EventArgs eventArgs)
   {
      OnGrassPlowed onGrassPlowed = (OnGrassPlowed)eventArgs; 
      
      _moneyToProvide = (int)_operationCosts[OperationTypes.Plowing];
      _positionToMessage = onGrassPlowed.PlowedTile.transform.position;
      
      ChangeMoneyAmount(_moneyToProvide);
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_moneyToProvide, _moneyAmount, _positionToMessage));
   }

   private void CroppingTransactionHandler(object sender, EventArgs eventArgs)
   {
      OnCropCollected onCropCollected = (OnCropCollected)eventArgs;
      
      _moneyToProvide = (int)(onCropCollected.AmountOfCollectedCrop * _operationCosts[OperationTypes.SellingCrop]);
      _positionToMessage = onCropCollected.CollectedFromTile.transform.position;
      
      ChangeMoneyAmount(_moneyToProvide);
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_moneyToProvide, _moneyAmount, _positionToMessage));
   }

   private void PlantingTransactionHandler(object sender, EventArgs eventArgs)
   {
      OnTilePlanted onTilePlanted = (OnTilePlanted)eventArgs;
      
      _moneyToProvide = (int)_operationCosts[OperationTypes.Planting];
      _positionToMessage = onTilePlanted.PlantedTile.transform.position;
      
      ChangeMoneyAmount(_moneyToProvide);
      GlobalEventBus.Sync.Publish(this, new OnMoneyAmountChanged(_moneyToProvide, _moneyAmount, _positionToMessage));
   }
   private void ChangeMoneyAmount(int amount)
   {
      _moneyAmount += amount;
   }

   public bool CheckOperationProcessability(OperationTypes operationTypes, Transform positionOfChecking)
   {
      bool isEnoughMoney = _moneyAmount + _operationCosts[operationTypes] >= 0;
      if (!isEnoughMoney)
      { 
         GlobalEventBus.Sync.Publish(this, new OnMoneyTransactionFailed(positionOfChecking));
      }
      return (_moneyAmount + _operationCosts[operationTypes]) >= 0 ? true : false;
   }
}
