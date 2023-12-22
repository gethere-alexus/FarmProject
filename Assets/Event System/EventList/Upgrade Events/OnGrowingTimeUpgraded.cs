using System;

public class OnGrowingTimeUpgraded : EventArgs
{
    public float Boost;

    public OnGrowingTimeUpgraded(float boost)
    {
        Boost = boost;
    }
}
