using System;
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
    private void SpawnPlayer(int x, int y)
    {
        this.gameObject.transform.position = new Vector3(x, y);
    }
}
