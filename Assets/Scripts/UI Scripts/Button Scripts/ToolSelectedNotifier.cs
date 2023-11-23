using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolTypes {Shovel, Hoe, Sickle, None}
public class ToolSelectedNotifier : MonoBehaviour
{
    [SerializeField] private ToolTypes _chosenToolType;
    private ToolTypes switcher;

    private void Start()
    {
        switcher = _chosenToolType;
    }

    public void ToolChosen()
    {
        GlobalEventBus.Sync.Publish(this, new OnToolChosen(switcher));
        switcher = switcher == ToolTypes.None ? _chosenToolType : ToolTypes.None;
    }
}
