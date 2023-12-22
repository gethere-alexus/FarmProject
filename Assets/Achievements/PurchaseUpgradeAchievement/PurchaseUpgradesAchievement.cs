using System;

public class PurchaseUpgradesAchievement : Achievement
{
    protected override void SubscribeOnObservedEvents()
    {
        GlobalEventBus.Sync.Subscribe<OnUpgradePurchased>(HandleSubscribedSignals);
    }

    protected override void UnsubscribeFromObservedEvents()
    {
        GlobalEventBus.Sync.Unsubscribe<OnUpgradePurchased>(HandleSubscribedSignals);
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
