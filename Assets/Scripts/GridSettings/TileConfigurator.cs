using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[ExecuteInEditMode]
public class TileConfigurator : MonoBehaviour
{
    
    [SerializeField] private TileTypes _currentTile;

    private Sprite _grassSprite, _dirtSprite,_sandSprite;
    internal enum TileTypes { Idle, Dirt, EndMap }

    private SpriteRenderer _tileSpriteRenderer;
    
    
    private void Start()
    {
        LoadResources();
        
    }

    private void Update()
    {
        switch (_currentTile)
        {
            case TileTypes.Idle:
                _tileSpriteRenderer.sprite = _grassSprite;
                break;
            case TileTypes.Dirt:
                _tileSpriteRenderer.sprite = _dirtSprite;
                break;
            case TileTypes.EndMap:
                _tileSpriteRenderer.sprite = _sandSprite;
                break;
        }
    }

    internal void SetTile(TileTypes tileType)
    {
        _currentTile = tileType;
    }

    private void LoadResources()
    {
        _tileSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        
        _dirtSprite = Resources.Load<Sprite>("Sprites/dirt");
        _grassSprite = Resources.Load<Sprite>("Sprites/grass");
        _sandSprite = Resources.Load<Sprite>("Sprites/sand");
    }
}
