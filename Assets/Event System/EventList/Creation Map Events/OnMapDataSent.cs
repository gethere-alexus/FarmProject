using System;
using System.Collections.Generic;
using UnityEngine;

public class OnMapDataSent : EventArgs
{
    public int MapWidth, MapHeight;
    public int Difficulty;
    public Dictionary<OperationTypes, float> OperationCosts;
    
    private int borderReservation = 1;
    private int valueToMapSizeCoefficient = 10;
    public OnMapDataSent(int sizeValue, int difficulty)
    {
        Difficulty = difficulty;
        Debug.Log("Difficulty is " + difficulty);
        MapWidth = (sizeValue * valueToMapSizeCoefficient) + borderReservation;
        MapHeight = (sizeValue * valueToMapSizeCoefficient) + borderReservation;
    }
    
}
