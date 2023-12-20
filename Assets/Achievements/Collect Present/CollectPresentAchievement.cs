using System;


public class CollectPresentAchievement : Achievement
{
    protected override void SubscribeOnObservedEvents()
    {
        GlobalEventBus.Sync.Subscribe<OnFinancialPresentClaimed>(HandleSubscribedSignals);
    }

    protected override void UnsubscribeFromObservedEvents()
    {
        GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentClaimed>(HandleSubscribedSignals);
    }

    protected override void HandleSubscribedSignals(object sender, EventArgs eventArgs)
    {
        _currentAmountOfSignals++;
        if (_currentAmountOfSignals >= _amountOfSignalsToAchieve)
        {
            _isCompleted = true;
            NotifyAchievementCompleted();
            ProvideReward();
            UnsubscribeFromObservedEvents();
        }
    }
}
