using System;
using UnityEngine;

public class OnCropCollected : EventArgs
{
    public int AmountOfCollectedCrop;
    public GameObject CollectedFromTile;

    public OnCropCollected(GameObject tile, int amountOfCollectedCrop)
    {
        AmountOfCollectedCrop = amountOfCollectedCrop;
        CollectedFromTile = tile;
    }
}
