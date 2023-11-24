using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSwitcher : MonoBehaviour
{
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnToolChosen>(ToolChooseHandler);
    }
    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnToolChosen>(ToolChooseHandler);
    }
    private void ToolChooseHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnToolChosen onToolChosen)
        {
            
            Cursor.SetCursor(onToolChosen.ToolCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
