using System;

public class CollectCropAchievement : Achievement
{
    protected override void SubscribeOnObservedEvents()
    {
        GlobalEventBus.Sync.Subscribe<OnCropCollected>(HandleSubscribedSignals);
    }

    protected override void UnsubscribeFromObservedEvents()
    {
        GlobalEventBus.Sync.Unsubscribe<OnCropCollected>(HandleSubscribedSignals);
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
