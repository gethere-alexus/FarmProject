using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonClickManager : MonoBehaviour
{
    public enum ButtonTypes {PlayButton,SettingsButton,QuitButton}
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(ButtonPressedHandler);
    }

    private void Start()
    {
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(ButtonPressedHandler);
    }

    private void ButtonPressedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnButtonPressed onButtonPressed)
        {
            switch (onButtonPressed.PressedButtonType)
            {
                case ButtonTypes.QuitButton:
                    if(!Application.isEditor) Application.Quit();
                    //else EditorApplication.ExitPlaymode();
                    break;
            }
            
        }
    }
}
