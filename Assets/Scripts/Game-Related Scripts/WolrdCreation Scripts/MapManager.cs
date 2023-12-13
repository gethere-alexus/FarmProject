using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private int _mapHeight, _mapWidth;

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMapCreated>(ProcessMapCreation);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(ProcessMapCreation);
    }

    private void ProcessMapCreation(object sender, EventArgs eventArgs)
    {
        OnMapCreated onMapCreated = (OnMapCreated)eventArgs;
        
        _mapHeight = onMapCreated.MapHeight;
        _mapWidth = onMapCreated.MapWidth;

    }

    public int GetMapHeight()
    {
        return _mapHeight;
    }
}
