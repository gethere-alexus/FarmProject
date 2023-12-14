using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    public bool IsLooped;
    
    [Range(0, 1)]public float volume;
    [Range(-3, 3)]public float pitch = 1;

    [HideInInspector] public AudioSource audioSource;
}
