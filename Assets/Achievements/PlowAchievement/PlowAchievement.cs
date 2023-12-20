using System;

public class PlowAchievement : Achievement
{
    protected override void SubscribeOnObservedEvents()
    {
        GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(HandleSubscribedSignals);
    }

    protected override void UnsubscribeFromObservedEvents()
    {
        GlobalEventBus.Sync.Unsubscribe<OnGrassPlowed>(HandleSubscribedSignals);
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
