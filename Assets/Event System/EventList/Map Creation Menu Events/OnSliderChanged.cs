using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSliderChanged : EventArgs
{
    public float Value;

    public OnSliderChanged(float newValue)
    {
        Value = newValue;
    }
}
