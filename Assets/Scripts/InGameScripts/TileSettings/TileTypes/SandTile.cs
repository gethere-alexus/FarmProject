using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTile : Tile
{
    private void Start()
    {
        _pathToSprite = "Sprites/sand";
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
        
        SetObjectSprite(_tileSprite);
    }
    protected override void SetObjectSprite(Sprite tileSprite)
    {
        base.SetObjectSprite(tileSprite);
    }
}
