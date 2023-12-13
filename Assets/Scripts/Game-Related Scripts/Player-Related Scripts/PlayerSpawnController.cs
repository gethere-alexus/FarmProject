using System;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMapCreated>(SetPlayerSpawn);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(SetPlayerSpawn);
    }

    private void SetPlayerSpawn(object sender, EventArgs eventArgs)
    {
        OnMapCreated onMapCreated = (OnMapCreated)eventArgs;
        
        SpawnPlayer(onMapCreated.PlayerSpawnPointX, onMapCreated.PlayerSpawnPointY);   
    }
    private void SpawnPlayer(int x, int y)
    {
        this.gameObject.transform.position = new Vector3(x, y);
    }
}
