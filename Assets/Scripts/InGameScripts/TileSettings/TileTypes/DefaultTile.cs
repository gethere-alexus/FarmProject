using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultTile : Tile, ICultivatable
{
    private void Start()
    {
        _pathToSprite = "Sprites/grass";
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
        
        SetObjectSprite(_tileSprite);
    }

    public void Cultivate()
    {
        
    }
}
