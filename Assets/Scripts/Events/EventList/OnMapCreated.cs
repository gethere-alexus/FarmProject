using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMapCreated : EventArgs
{
    public int MapHeight, MapWidth;

    public int PlayerSpawnPointX, PlayerSpawnPointY;

    public OnMapCreated(int width, int height)
    {
        MapHeight = height;
        MapWidth = width;

        PlayerSpawnPointX = width / 2;
        PlayerSpawnPointY = height / 2;
    }
}
