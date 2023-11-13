using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingOutController : MonoBehaviour
{
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
        PlayFadingInOutAnimation("CanvasFadingOut");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayFadingInOutAnimation("CanvasFadingIn");
    }

    private void PlayFadingInOutAnimation(string animationName)
    {
        this.gameObject.GetComponent<Animation>().Play(animationName);
    }
}
