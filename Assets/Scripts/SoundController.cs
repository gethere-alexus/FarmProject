using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(PlayClickAudio);
    }

    private void PlayClickAudio(object sender, EventArgs eventArgs)
    {
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
