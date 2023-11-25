using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTileTriggered : EventArgs
{
    public GameObject Tile;
    

    public OnTileTriggered(GameObject tile)
    {
        Tile = tile;
    }
}
