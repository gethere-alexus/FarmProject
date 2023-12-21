using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonPressed : EventArgs
{
    public ButtonTypes PressedButtonType;
    public string SceneToSwitchName;

    public OnButtonPressed(ButtonTypes buttonType, string previousSceneName)
    {
        PressedButtonType = buttonType;

        Dictionary<ButtonTypes, string> buttonToScene =
            new Dictionary<ButtonTypes, string>()
            {
                {ButtonTypes.PlayButton, "Choose Type Of Loading"},
                { ButtonTypes.MainMenu , "Main Menu"},
                {ButtonTypes.QuitButton, "Quit"},
                {ButtonTypes.SettingsButton , "Settings"},
                {ButtonTypes.CreateGameButton, "Create New Game"},
                {ButtonTypes.GenerateNewWorldButton, "Game"},
                {ButtonTypes.PreviousSceneButton, previousSceneName},
            };
        SceneToSwitchName = buttonToScene[buttonType];
    }
}
