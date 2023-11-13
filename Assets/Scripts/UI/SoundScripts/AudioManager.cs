using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(ButtonPressedHandler);
        GlobalEventBus.Sync.Subscribe<OnSliderChanged>(ButtonPressedHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(ButtonPressedHandler);
        GlobalEventBus.Sync.Unsubscribe<OnSliderChanged>(ButtonPressedHandler);
    }

    private void ButtonPressedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnButtonPressed onButtonPressed || eventArgs is OnSliderChanged onSliderChanged)
        {
            Play("Click");
        }
    }

    private void Play(string soundName)
    {
        Array.Find(_sounds, sound => sound.name == soundName).audioSource.Play();
    }
}
