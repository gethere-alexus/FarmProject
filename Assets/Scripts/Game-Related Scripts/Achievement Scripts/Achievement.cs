using System;
using UnityEngine;

public abstract class Achievement : MonoBehaviour
{
    [SerializeField] protected string _achievementDescription;
    
    [SerializeField] protected float _amountOfSignalsToAchieve;
    [SerializeField] protected int _reward;
    [SerializeField] protected float _currentAmountOfSignals = 0;
    
    [SerializeField] protected bool _isCompleted;


    private void OnEnable()
    {
        SubscribeOnObservedEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromObservedEvents();
    }

    protected void ProvideReward()
    {
        GlobalEventBus.Sync.Publish(this, new OnMoneyProvided(_reward));
    }
    protected virtual void NotifyAchievementCompleted()
    {
        GlobalEventBus.Sync.Publish(this, new OnAchievementCompleted(_achievementDescription));
    }
    protected virtual void HandleSubscribedSignals(object sender, EventArgs eventArgs)
    {
        
    }
    protected virtual void SubscribeOnObservedEvents()
    {
        
    }
    protected virtual void UnsubscribeFromObservedEvents()
    {
        
    }

    public string AchievementDescription
    {
        get => _achievementDescription;
    }
    public float AmountOfSignalsToAchieve
    {
        get => _amountOfSignalsToAchieve;
    }
    public float CurrentAmountOfSignals
    {
        get => _currentAmountOfSignals;
    }
    public int Reward
    {
        get => _reward;
    }
    
}
