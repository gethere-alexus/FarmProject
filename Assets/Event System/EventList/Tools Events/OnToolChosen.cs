using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;

public class OnToolChosen : EventArgs
{
    public ToolTypes ChosenTool;

    public OnToolChosen(ToolTypes chosenTool)
    {
        
        this.ChosenTool = chosenTool;
    }
}

