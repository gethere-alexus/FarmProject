using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class MoneyMessageNotifier : MonoBehaviour
{
    [SerializeField] private Vector3 positionAsset = new Vector3(0, .5f);
    [SerializeField] private GameObject _moneyChangedPrefab;
    [SerializeField] private GameObject _notEnoughMoneyPrefab;

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMoneyAmountChanged>(TransactionMessageHandler);
        GlobalEventBus.Sync.Subscribe<OnMoneyTransactionFailed>(ErrorMessageHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMoneyAmountChanged>(TransactionMessageHandler);
        GlobalEventBus.Sync.Unsubscribe<OnMoneyTransactionFailed>(ErrorMessageHandler);
    }

    private void TransactionMessageHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnMoneyAmountChanged onMoneyAmountChanged)
        {
            InstantiateMoneyTransactionMessage(onMoneyAmountChanged.ChangeValue, onMoneyAmountChanged.PositionForMessage);
        }
    }

    private void ErrorMessageHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnMoneyTransactionFailed onMoneyTransactionFailed)
        {
            InstantiateInsufficientMoneyMessage(onMoneyTransactionFailed.TransformOfFailedAttempt);
        }
    }

    private void InstantiateMoneyTransactionMessage(int value, Vector3 messagePosition)
    {
        GameObject moneyPrefab = Instantiate(_moneyChangedPrefab);
        moneyPrefab.transform.position = messagePosition + positionAsset;
        
        MoneyChangesConfigurator moneyChangesConfigurator = moneyPrefab.GetComponent<MoneyChangesConfigurator>();
        moneyChangesConfigurator.SetAmountOfChangedMoney(value);
    }

    private void InstantiateInsufficientMoneyMessage(Transform transformToInstantiate)
    {
        GameObject errorMessage = Instantiate(_notEnoughMoneyPrefab,transformToInstantiate);
        
        errorMessage.transform.rotation = quaternion.identity;
        errorMessage.transform.position = errorMessage.transform.position + positionAsset;
    }
}
