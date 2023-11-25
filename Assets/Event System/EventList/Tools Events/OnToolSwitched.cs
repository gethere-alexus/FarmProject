using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnToolSwitched : EventArgs
{
    public Texture2D ToolCursor;
    
    public OnToolSwitched(ToolTypes toolType)
    {
        ToolCursor = _toolsCursors[toolType];
    }
    
    private Dictionary<ToolTypes, Texture2D> _toolsCursors = new Dictionary<ToolTypes, Texture2D>()
    {
        { ToolTypes.Hoe , Resources.Load<Texture2D>("Sprites/Cursors/hoe")},
        { ToolTypes.Sickle ,Resources.Load<Texture2D>("Sprites/Cursors/sickle")},
        { ToolTypes.None , Resources.Load<Texture2D>("Sprites/Cursors/cursor")},
        { ToolTypes.Shovel , Resources.Load<Texture2D>("Sprites/Cursors/shovel")},
        { ToolTypes.Bag , Resources.Load<Texture2D>("Sprites/Cursors/bag")}
    };
}
