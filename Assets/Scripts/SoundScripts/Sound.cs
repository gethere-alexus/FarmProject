using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    public bool IsLooped;
    
    [Range(0, 1)]public float volume;

    [HideInInspector] public AudioSource audioSource;
}
