using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultTile : Tile
{
    private void Start()
    {
        _pathToSprite = "Sprites/grass";
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
        
        SetObjectSprite(_tileSprite);
    }

    protected override void SetObjectSprite(Sprite tileSprite)
    {
        base.SetObjectSprite(tileSprite);
    }
}
