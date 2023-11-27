using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCropCollected : EventArgs
{
    public int AmountOfCollectedCrop;

    public OnCropCollected(int amountOfCollectedCrop)
    {
        AmountOfCollectedCrop = amountOfCollectedCrop;
    }
}
