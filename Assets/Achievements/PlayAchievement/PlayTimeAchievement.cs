using System;
using UnityEngine;

public class PlayTimeAchievement : Achievement
{
    private void FixedUpdate()
    {
        if (!_isCompleted)
        {
            if (_currentAmountOfSignals >= _amountOfSignalsToAchieve)
            {
                NotifyAchievementCompleted();
                ProvideReward();
                _isCompleted = true;
            }
            _currentAmountOfSignals += Time.deltaTime;
        }
    }
}
