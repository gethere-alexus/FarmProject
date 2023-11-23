using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelecter : MonoBehaviour
{
    [SerializeField]private ToolTypes _currentTool = ToolTypes.None;
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
            _currentTool = onToolChosen.ChosenTool;
        }
    }
}
