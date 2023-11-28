using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class OnMoneyTransactionFailed : EventArgs
{
    public Transform TransformOfFailedAttempt;

    public OnMoneyTransactionFailed(Transform transformOfFailedAttempt)
    {
        TransformOfFailedAttempt = transformOfFailedAttempt;
    }
}
