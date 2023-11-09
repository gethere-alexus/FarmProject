using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
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
            SpawnPlayer(onMapCreatedSignal.PlayerSpawnPointX, onMapCreatedSignal.PlayerSpawnPointY);   
        }
    }

    private void Start()
    {
        SpawnPlayer(1  ,1);
    }

    private void SpawnPlayer(int x, int y)
    {
        this.gameObject.transform.position = new Vector2(x, y);
    }
}
