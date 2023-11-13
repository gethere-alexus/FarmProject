using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonPressed : EventArgs
{
    public ButtonClickController.ButtonTypes PressedButtonType;
    public string SceneToSwitchName;

    public OnButtonPressed(ButtonClickController.ButtonTypes buttonType, string previousSceneName)
    {
        PressedButtonType = buttonType;

        Dictionary<ButtonClickController.ButtonTypes, string> buttonToScene =
            new Dictionary<ButtonClickController.ButtonTypes, string>()
            {
                {ButtonClickController.ButtonTypes.PlayButton, "ChooseTypeOfLoad"},
                {ButtonClickController.ButtonTypes.SettingsButton , "Settings"},
                {ButtonClickController.ButtonTypes.CreateGameButton, "NewGame"},
                {ButtonClickController.ButtonTypes.GenerateNewWorldButton, "GameScene"},
                {ButtonClickController.ButtonTypes.PreviousSceneButton, previousSceneName},
                
            };
        SceneToSwitchName = buttonToScene[buttonType];
    }
}
