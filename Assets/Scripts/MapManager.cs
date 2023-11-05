using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private int _mapHeight,_mapWidth,_playerSpawnPointX,_playerSpawnPointY;

    private void Start()
    {
        _playerSpawnPointX = _mapWidth / 2;
        _playerSpawnPointY = _mapHeight / 2;
    }

    public int MapWidth
    {
        get => _mapWidth;
        set => _mapWidth = value;
    }
    public int PlayerSpawnPointX
    {
        get => _playerSpawnPointX;
    }
    public int PlayerSpawnPointY
    {
        get => _playerSpawnPointY;
    }

    public int MapHeight
    {
        get => _mapHeight;
        set => _mapHeight = value;
    }
}
