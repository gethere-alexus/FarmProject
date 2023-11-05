using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    private GameObject _mapStorage;
    private MapManager _currentMapManager;
    private void Start()
    {
        _mapStorage = GameObject.FindWithTag("MapStorage");
        _currentMapManager = _mapStorage.transform.GetChild(0).GetComponent<MapManager>();
        
        SpawnPlayer(_currentMapManager.PlayerSpawnPointX, _currentMapManager.PlayerSpawnPointY);
    }

    private void SpawnPlayer(int x, int y)
    {
        this.gameObject.transform.position = new Vector2(x, y);
    }
}
