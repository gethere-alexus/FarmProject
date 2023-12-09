using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSwitcher : MonoBehaviour
{
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnToolSwitched>(ToolChooseHandler);
    }
    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnToolSwitched>(ToolChooseHandler);
    }
    private void ToolChooseHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnToolSwitched onToolSwitched)
        {
            Cursor.SetCursor(onToolSwitched.ToolCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
