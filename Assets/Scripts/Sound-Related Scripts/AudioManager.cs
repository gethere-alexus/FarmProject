using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;
    private static AudioManager _instance;
    

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
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnSliderChanged>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnMoneyAmountChanged>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnDirtCultivatingStageCompleted>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnTilePlanted>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnToolSwitched>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnMoneyTransactionFailed>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnFinancialPresentAppeared>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnMovementActionPerformed>(PlaySoundHandler);
        GlobalEventBus.Sync.Subscribe<OnMovementActionCanceled>(PlaySoundHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnSliderChanged>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnMoneyAmountChanged>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnGrassPlowed>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivatingStageCompleted>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnTilePlanted>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnToolSwitched>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnMoneyTransactionFailed>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentAppeared>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentClaimed>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnMovementActionPerformed>(PlaySoundHandler);
        GlobalEventBus.Sync.Unsubscribe<OnMovementActionCanceled>(PlaySoundHandler);
    }

    private void PlaySoundHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnButtonPressed || eventArgs is OnSliderChanged)
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
        else if (eventArgs is OnFinancialPresentAppeared)
        {
            Play("PresentNotify");
        }
        else if (eventArgs is OnFinancialPresentClaimed)
        {
            Play("PresentCollected");
        }
        else if (eventArgs is OnMovementActionPerformed)
        {
            if (!CheckIsPlaying("Step"))
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
