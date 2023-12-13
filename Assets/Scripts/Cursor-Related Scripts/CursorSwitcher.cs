using System;
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
        OnToolSwitched onToolSwitched = (OnToolSwitched)eventArgs;
        
        Cursor.SetCursor(onToolSwitched.ToolCursor, Vector2.zero, CursorMode.Auto);
        
    }
}
