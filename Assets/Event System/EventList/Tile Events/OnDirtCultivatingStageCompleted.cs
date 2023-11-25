using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnDirtCultivatingStageCompleted : EventArgs
{
    public GameObject CultivatedTile;
    public bool isTheFirstCultivating, isCultivationCompleted;
    public int CurrentStage, MaxStages;

    public OnDirtCultivatingStageCompleted(GameObject tileGameObject,int currentStage, int maxStages)
    {
        Debug.Log(currentStage);
        CultivatedTile = tileGameObject;
        CurrentStage = currentStage;
        MaxStages = maxStages;
        isTheFirstCultivating = currentStage == 1;
        isCultivationCompleted = currentStage >= maxStages;
    }
}
