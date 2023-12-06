using System;

public class OnSliderChanged : EventArgs
{
    public float Value;
    public PropertyTypes PropertyType;

    public OnSliderChanged(float newValue, PropertyTypes property)
    {
        PropertyType = property;
        Value = newValue;
    }
}
