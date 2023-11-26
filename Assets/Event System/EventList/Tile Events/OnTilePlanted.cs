using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTilePlanted : EventArgs
{
    public GameObject PlantedTile;

    public OnTilePlanted(GameObject tile)
    {
        PlantedTile = tile;
    }
}
