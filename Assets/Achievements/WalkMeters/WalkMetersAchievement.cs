using System;
using UnityEngine;

public class WalkMetersAchievement : Achievement
{
    private bool _isMoving = false;

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            if (_currentAmountOfSignals >= _amountOfSignalsToAchieve)
            {
                _isMoving = false;
                NotifyAchievementCompleted();
                ProvideReward();
                UnsubscribeFromObservedEvents();
                _isCompleted = true;
            }

            _currentAmountOfSignals += Time.deltaTime * 2;
        }
    }

    protected override void SubscribeOnObservedEvents()
    {
        GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(HandleSubscribedSignals);
        GlobalEventBus.Sync.Subscribe<OnPlayerStopped>(HandleSubscribedSignals);
    }

    protected override void UnsubscribeFromObservedEvents()
    {
        GlobalEventBus.Sync.Unsubscribe<OnPlayerMoved>(HandleSubscribedSignals);
        GlobalEventBus.Sync.Unsubscribe<OnPlayerStopped>(HandleSubscribedSignals);
    }

    protected override void HandleSubscribedSignals(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnPlayerMoved onPlayerMoved)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }
}
