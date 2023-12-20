using System;
using UnityEngine;

public class OnNewCropChosen : EventArgs
{
    public GameObject ChosenCrop;

    public OnNewCropChosen(GameObject chosenCrop)
    {
        ChosenCrop = chosenCrop;
    }
}
