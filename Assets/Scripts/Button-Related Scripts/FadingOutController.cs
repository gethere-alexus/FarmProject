using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingOutController : MonoBehaviour
{
    [SerializeField] private string _animationStartName;
    [SerializeField] private string _animationEndName;

    [SerializeField] private Animation _animationController;
    
    private Dictionary<string, string> _animationNames;

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
        
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(FadeOutScene);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(FadeOutScene);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void FadeOutScene(object sender, EventArgs eventArgs)
    {
        PlayFadingInOutAnimation(_animationNames["Start"]);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _animationNames = new Dictionary<string, string>()
        {
            { "Start",  _animationStartName},
            { "End", _animationEndName}
        };
        
        PlayFadingInOutAnimation(_animationNames["End"]);
    }

    private void PlayFadingInOutAnimation(string animationName)
    {
        
        _animationController.Play(animationName);
    }
}
