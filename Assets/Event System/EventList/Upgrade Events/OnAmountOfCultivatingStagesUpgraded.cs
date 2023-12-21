using System;

public class OnAmountOfCultivatingStagesUpgraded : EventArgs
{
    public float Boost;

    public OnAmountOfCultivatingStagesUpgraded(float boost)
    {
        Boost = boost;
    }
}
