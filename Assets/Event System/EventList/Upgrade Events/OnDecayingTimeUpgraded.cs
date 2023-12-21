using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDecayingTimeUpgraded : EventArgs
{
    public float Boost;

    public OnDecayingTimeUpgraded(float boost)
    {
        Boost = boost;
    }
}
