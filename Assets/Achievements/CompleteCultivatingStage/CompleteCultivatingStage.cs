using System;

public class CompleteCultivatingStage : Achievement
{
    protected override void SubscribeOnObservedEvents()
    {
        GlobalEventBus.Sync.Subscribe<OnDirtCultivatingStageCompleted>(HandleSubscribedSignals);
    }

    protected override void UnsubscribeFromObservedEvents()
    {
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivatingStageCompleted>(HandleSubscribedSignals);
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
