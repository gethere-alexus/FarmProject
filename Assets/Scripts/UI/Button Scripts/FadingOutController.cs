using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingOutController : MonoBehaviour
{
    [SerializeField] private Dictionary<string, string> _animationNames = new Dictionary<string, string>()
    {
        {"Fading Out", "SceneFadingOut"},
        {"Fading In", "SceneFadingIn"}
    };
    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
        
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(FadeOutScene);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(FadeOutScene);
    }

    private void FadeOutScene(object sender, EventArgs eventArgs)
    {
        PlayFadingInOutAnimation(_animationNames["Fading Out"]);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayFadingInOutAnimation(_animationNames["Fading In"]);
    }

    private void PlayFadingInOutAnimation(string animationName)
    {
        this.gameObject.GetComponent<Animation>().Play(animationName);
    }
}
