using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerMoved : EventArgs
{
    public float PlayerPositionX, PlayerPositionY;

    public OnPlayerMoved(float x, float y)
    {
        PlayerPositionX = x;
        PlayerPositionY = y;
    }
}
