using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMapDataSent : EventArgs
{
    public int MapWidth, MapHeight;

    public OnMapDataSent(int sizeValue)
    {
        MapWidth = sizeValue * 10;
        MapHeight = sizeValue * 10;
    }
    
}
