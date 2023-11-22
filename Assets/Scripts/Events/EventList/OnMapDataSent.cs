using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMapDataSent : EventArgs
{
    public int MapWidth, MapHeight;
    private int borderReservation = 1;
    private int valueToMapSizeCoefficient = 10;
    public OnMapDataSent(int sizeValue)
    {
        MapWidth = (sizeValue * valueToMapSizeCoefficient) + borderReservation;
        MapHeight = (sizeValue * valueToMapSizeCoefficient) + borderReservation;
    }
    
}
