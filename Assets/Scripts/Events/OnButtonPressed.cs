using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonPressed : EventArgs
{
    public ButtonClickManager.ButtonTypes PressedButtonType;
    public string SceneToSwitchName;

    public OnButtonPressed(ButtonClickManager.ButtonTypes buttonType, string previousSceneName)
    {
        PressedButtonType = buttonType;
        
        switch (buttonType)
        {
            case ButtonClickManager.ButtonTypes.PlayButton:
                SceneToSwitchName = "ChooseGameScene";
                break;
            case ButtonClickManager.ButtonTypes.SettingsButton:
                SceneToSwitchName = "SettingsScene";
                break;
            case ButtonClickManager.ButtonTypes.CreateGameButton:
                SceneToSwitchName = "CreateNewGameScene";
                break;
            case ButtonClickManager.ButtonTypes.PreviousSceneButton:
                SceneToSwitchName = previousSceneName;
                break;
        }
        
    }
}
