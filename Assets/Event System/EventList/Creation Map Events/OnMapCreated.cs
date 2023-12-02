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
    public GameObject BorderTile;
    public GameObject SandTile;
    public GameObject GrassTile;
    public GameObject DirtTile;
    public GameObject CultivatedDirt;

    public OnMapCreated(int width, int height,GameObject border, GameObject sand, GameObject grass, GameObject dirt, GameObject cultivatedDirt)
    {
        MapHeight = height;
        MapWidth = width;

        BorderTile = border;
        SandTile = sand;
        GrassTile = grass;
        DirtTile = dirt;
        CultivatedDirt = cultivatedDirt;

        XMaxBorderCoordinate = MapWidth + 1.5f;
        YMaxBorderCoordinate = MapHeight + 1.5f;
        
        PlayerSpawnPointX = width / 2;
        PlayerSpawnPointY = height / 2;
    }
    
}
