using System;
using UnityEngine;

public class CursorSwitcher : MonoBehaviour
{
    private Texture2D currentCursor;
    
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProcessGamePause);
        GlobalEventBus.Sync.Subscribe<OnToolSwitched>(ProcessToolSwitch);
    }
    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnGamePausePerformed>(ProcessGamePause);
        GlobalEventBus.Sync.Unsubscribe<OnToolSwitched>(ProcessToolSwitch);
    }
    private void ProcessToolSwitch(object sender, EventArgs eventArgs)
    {
        OnToolSwitched onToolSwitched = (OnToolSwitched)eventArgs;

        currentCursor = onToolSwitched.ToolCursor;
        Cursor.SetCursor(onToolSwitched.ToolCursor, Vector2.zero, CursorMode.Auto);
        
    }
    private void ProcessGamePause(object sender, EventArgs eventArgs)
    {
        OnGamePausePerformed onGamePausePerformed = (OnGamePausePerformed)eventArgs;

        if (onGamePausePerformed.IsGamePaused)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(currentCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
