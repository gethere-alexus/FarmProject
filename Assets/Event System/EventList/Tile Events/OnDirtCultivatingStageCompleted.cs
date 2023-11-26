using System;
using UnityEngine;

public class OnDirtCultivatingStageCompleted : EventArgs
{
    public GameObject CultivatedTile;
    public bool isTheFirstCultivating, isCultivationCompleted;
    public int CurrentStage, MaxStages;

    public OnDirtCultivatingStageCompleted(GameObject tileGameObject,int currentStage, int maxStages)
    {
        CultivatedTile = tileGameObject;
        CurrentStage = currentStage;
        MaxStages = maxStages;
        isTheFirstCultivating = currentStage == 1;
        isCultivationCompleted = currentStage >= maxStages;
    }
}
