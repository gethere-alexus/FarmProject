using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickController : MonoBehaviour
{
    public enum ButtonTypes {PlayButton,SettingsButton,QuitButton, PreviousSceneButton, LoadGameButton, CreateGameButton, GenerateNewWorldButton}
    
    [SerializeField] private ButtonTypes buttonType;

    public void SendButtonClickMessage()
    {
        GlobalEventBus.Sync.Publish(this, new OnButtonPressed(buttonType, SceneManager.GetActiveScene().name));
    }
}
