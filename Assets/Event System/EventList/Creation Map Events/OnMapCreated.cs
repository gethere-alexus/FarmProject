using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMapCreated : EventArgs
{
    public int MapHeight, MapWidth;

    public int PlayerSpawnPointX, PlayerSpawnPointY;
    public float XMaxBorderCoordinate;
    public float YMaxBorderCoordinate;

    public OnMapCreated(int width, int height)
    {
        MapHeight = height;
        MapWidth = width;

        XMaxBorderCoordinate = MapWidth + 1.5f;
        YMaxBorderCoordinate = MapHeight + 1.5f;
        
        PlayerSpawnPointX = width / 2;
        PlayerSpawnPointY = height / 2;
    }
    
}
