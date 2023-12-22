using System;
using UnityEngine;

public class UpgradeMenuInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeMenu;

    private GameObject instance;

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProcessGamePause);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnGamePausePerformed>(ProcessGamePause);
    }

    private void ProcessGamePause(object sender, EventArgs eventArgs)
    {
        OnGamePausePerformed onGamePausePerformed = (OnGamePausePerformed)eventArgs;
        if (onGamePausePerformed.DoesInstatiatePauseMenu)
        {
            Destroy(instance);
        }
    }

    public void InstantiateMenu()
    {
        GlobalEventBus.Sync.Publish(this, new OnGamePausePerformed(true,false ));
        instance = Instantiate(_upgradeMenu);
    }
    
}
