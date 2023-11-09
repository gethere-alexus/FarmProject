using System;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private int _mapHeight, _mapWidth;

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMapCreated>(OnMapCreatedHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(OnMapCreatedHandler);
    }

    private void OnMapCreatedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnMapCreated onMapCreatedSignal)
        {
            _mapHeight = onMapCreatedSignal.MapHeight;
            _mapWidth = onMapCreatedSignal.MapWidth;
            
        }
    }
}
