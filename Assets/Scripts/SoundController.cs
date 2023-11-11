using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private Dictionary<string, int> _sounds = new Dictionary<string, int>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _sounds.Add(child.gameObject.name, child.GetSiblingIndex());
        }
        PlayAudio("MainMusic");
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(OnButtonPressedHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(OnButtonPressedHandler);
    }

    private void OnButtonPressedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnButtonPressed onButtonPressed)
        {
            PlayAudio("Click");
        }
    }
    private void PlayAudio(string name)
    {
        this.gameObject.transform.GetChild(_sounds[name]).gameObject.GetComponent<AudioSource>().Play();
    }
}
