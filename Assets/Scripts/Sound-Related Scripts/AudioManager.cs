using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;
    private static AudioManager _instance;

    private bool _isGamePaused;
    

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return ;
        }
        
        foreach (var sound in _sounds)
        {
            sound.audioSource = this.gameObject.AddComponent<AudioSource>();

            sound.audioSource.clip = sound.clip;
            sound.audioSource.loop = sound.IsLooped;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.playOnAwake = false;
        }
        
        DontDestroyOnLoad(this.gameObject);
        
        Play("Theme");
    }

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProcessGamePause);
        
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnSliderChanged>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnMoneyAmountChanged>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnDirtCultivatingStageCompleted>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnTilePlanted>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnToolSwitched>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnMoneyTransactionFailed>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnFinancialPresentAppeared>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnMovementActionPerformed>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnMovementActionCanceled>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnNewCropChosen>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProcessSound);
        GlobalEventBus.Sync.Subscribe<OnAchievementCompleted>(ProcessSound);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnGamePausePerformed>(ProcessGamePause);
        
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnSliderChanged>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnMoneyAmountChanged>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnGrassPlowed>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivatingStageCompleted>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnTilePlanted>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnToolSwitched>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnMoneyTransactionFailed>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentAppeared>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentClaimed>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnMovementActionPerformed>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnMovementActionCanceled>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnNewCropChosen>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnGamePausePerformed>(ProcessSound);
        GlobalEventBus.Sync.Unsubscribe<OnAchievementCompleted>(ProcessSound);
    }

    private void ProcessGamePause(object sender, EventArgs eventArgs)
    {
        OnGamePausePerformed onGamePausePerformed = (OnGamePausePerformed)eventArgs;
        _isGamePaused = onGamePausePerformed.IsGamePaused;
        
        Stop("Step");
    }
    private void ProcessSound(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnButtonPressed || eventArgs is OnSliderChanged || eventArgs is OnNewCropChosen || eventArgs is OnGamePausePerformed)
        {
            Play("Click");
        }
        else if (eventArgs is OnMoneyAmountChanged)
        {
            Play("MoneyChanged");
        }
        else if (eventArgs is OnGrassPlowed || eventArgs is OnDirtCultivatingStageCompleted)
        {
            Play("Digging");
        }
        else if (eventArgs is OnTilePlanted)
        {
            Play("Planting");
        }
        else if (eventArgs is OnToolSwitched)
        {
            Play("ToolChanged");
        }
        else if (eventArgs is OnMoneyTransactionFailed)
        {
            Play("Failed");
        }
        else if (eventArgs is OnFinancialPresentAppeared || eventArgs is OnAchievementCompleted)
        {
            Play("PresentNotify");
        }
        else if (eventArgs is OnFinancialPresentClaimed)
        {
            Play("PresentCollected");
        }
        else if (eventArgs is OnMovementActionPerformed)
        {
            if (!CheckIsPlaying("Step") && !_isGamePaused)
            {
                Play("Step");
            }
        }
        else if (eventArgs is OnMovementActionCanceled)
        {
            Stop("Step");
        }
    }

    private void Play(string soundName)
    {
        Array.Find(_sounds, sound => sound.name == soundName).audioSource.Play();
    }

    private void Stop(string soundName)
    {
        Array.Find(_sounds, sound => sound.name == soundName).audioSource.Stop();
    }

    private bool CheckIsPlaying(string soundName)
    {
        return Array.Find(_sounds, sound => sound.name == soundName).audioSource.isPlaying;
    }
}
