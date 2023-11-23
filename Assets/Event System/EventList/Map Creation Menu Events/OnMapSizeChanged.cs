using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMapSizeChanged : EventArgs
{
    public int MapSize;

    public OnMapSizeChanged(int mapSize)
    {
        MapSize = mapSize;
    }
    
}
