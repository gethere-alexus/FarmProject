using System;
using UnityEngine;

public class PausePopUpController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePrefab;

    private GameObject _pauseInstance;
    

    private void ProcessGamePause(object sender, EventArgs eventArgs)
    {
        OnGamePausePerformed onGamePausePerformed = (OnGamePausePerformed)eventArgs;
        
        if (!onGamePausePerformed.IsGamePaused)
        {
            if (_pauseInstance != null)
            {
                Destroy(_pauseInstance);
            }
        }
        else
        {
            if (onGamePausePerformed.DoesInstatiatePauseMenu)
            {
                _pauseInstance = Instantiate(_pausePrefab, this.gameObject.transform);
            }
        }
    }

    private void ProcessSwitch(object sender, EventArgs eventArgs)
    {
        OnGamePauseMenuSwitched onGamePauseMenuSwitched = (OnGamePauseMenuSwitched)eventArgs;
        _pauseInstance = onGamePauseMenuSwitched.NewMenu;
    }
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProcessGamePause);
        GlobalEventBus.Sync.Subscribe<OnGamePauseMenuSwitched>(ProcessSwitch);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnGamePausePerformed>(ProcessGamePause);
        GlobalEventBus.Sync.Unsubscribe<OnGamePauseMenuSwitched>(ProcessSwitch);
    }
}
