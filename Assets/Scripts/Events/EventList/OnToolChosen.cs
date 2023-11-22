using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OnToolChosen : EventArgs
{
    public ToolTypes chosenTool;

    public OnToolChosen(ToolTypes chosenTool)
    {
        this.chosenTool = chosenTool;
    }
}
