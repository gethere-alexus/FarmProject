using System;
using UnityEngine;


public class OnMoneyAmountChanged : EventArgs
{
    public int ChangeValue;
    public int BalanceAfterTransaction;
    public Vector3 PositionForMessage;
    public OnMoneyAmountChanged(int balanceAfterTransaction, Vector3 positionOfChanges = default, int value = 0)
    {
        ChangeValue = value;
        PositionForMessage = positionOfChanges;
        BalanceAfterTransaction = balanceAfterTransaction;
    }
}
