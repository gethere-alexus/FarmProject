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
        GlobalEventBus.Sync.Subscribe<OnFinancialPresentClaimed>(PlaySoundHandler);
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
    }

    private void PlaySoundHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnButtonPressed onButtonPressed || eventArgs is OnSliderChanged onSliderChanged)
        {
            Play("Click");
        }
        else if (eventArgs is OnMoneyAmountChanged onMoneyAmountChanged)
        {
            Play("MoneyChanged");
        }
        else if (eventArgs is OnGrassPlowed onGrassPlowed || eventArgs is OnDirtCultivatingStageCompleted onDirtCultivatingStageCompleted)
        {
            Play("Digging");
        }
        else if (eventArgs is OnTilePlanted onTilePlanted)
        {
            Play("Planting");
        }
        else if (eventArgs is OnToolSwitched onToolSwitched)
        {
            Play("ToolChanged");
        }
        else if (eventArgs is OnMoneyTransactionFailed onMoneyTransactionFailed)
        {
            Play("Failed");
        }
        else if (eventArgs is OnFinancialPresentAppeared onFinancialPresentAppeared)
        {
            Play("PresentNotify");
        }
        else if (eventArgs is OnFinancialPresentClaimed onFinancialPresentClaimed)
        {
            Play("PresentCollected");
        }
    }

    private void Play(string soundName)
    {
        Array.Find(_sounds, sound => sound.name == soundName).audioSource.Play();
    }
}
