using System;
using UnityEngine;


public class OnGamePauseMenuSwitched : EventArgs
{
    public GameObject NewMenu;

    public OnGamePauseMenuSwitched(GameObject newMenu)
    {
        NewMenu = newMenu;
    }
}
