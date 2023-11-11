using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonPressed : EventArgs
{
    public ButtonClickManager.ButtonTypes PressedButtonType;

    public OnButtonPressed(ButtonClickManager.ButtonTypes buttonType)
    {
        PressedButtonType = buttonType;
    }
}
