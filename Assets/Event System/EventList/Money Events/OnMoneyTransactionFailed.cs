using System;
using UnityEngine;

public class OnMoneyTransactionFailed : EventArgs
{
    public Transform TransformOfFailedAttempt;

    public OnMoneyTransactionFailed(Transform transformOfFailedAttempt)
    {
        TransformOfFailedAttempt = transformOfFailedAttempt;
    }
}
