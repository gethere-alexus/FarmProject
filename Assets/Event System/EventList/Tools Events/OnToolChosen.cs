using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;

public class OnToolChosen : EventArgs
{
    public ToolTypes ChosenTool;
    public Texture2D ToolCursor;

    public OnToolChosen(ToolTypes chosenTool)
    {
        
        this.ChosenTool = chosenTool;
        ToolCursor = toolsCursors[chosenTool];
    }
    private Dictionary<ToolTypes, Texture2D> toolsCursors = new Dictionary<ToolTypes, Texture2D>()
    {
        { ToolTypes.Hoe , Resources.Load<Texture2D>("Sprites/Cursors/hoe")},
        { ToolTypes.Sickle ,Resources.Load<Texture2D>("Sprites/Cursors/sickle")},
        { ToolTypes.None , Resources.Load<Texture2D>("Sprites/Cursors/cursor")},
        { ToolTypes.Shovel , Resources.Load<Texture2D>("Sprites/Cursors/shovel")},
    };
    
}

