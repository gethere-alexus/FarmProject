using System;
using UnityEngine;

public class RunTimeTileHandler : MonoBehaviour
{
    private GameObject _dirtTile;
    private GameObject _cultivatedDirt;
    
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMapCreated>(HandlePrefabs);
        GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(HandleGrassPlowed);
        GlobalEventBus.Sync.Subscribe<OnDirtCultivated>(HandleDirtCultivated);
        GlobalEventBus.Sync.Subscribe<OnCropCollected>(HandleTileCollected);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(HandlePrefabs);
        GlobalEventBus.Sync.Unsubscribe<OnGrassPlowed>(HandleGrassPlowed);
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivated>(HandleDirtCultivated);
        GlobalEventBus.Sync.Unsubscribe<OnCropCollected>(HandleTileCollected);
    }

    private void HandlePrefabs(object sender, EventArgs eventArgs)
    {
        OnMapCreated onMapCreated = (OnMapCreated)eventArgs;
        
        _dirtTile = onMapCreated.DirtTile;
        _cultivatedDirt = onMapCreated.CultivatedDirt;
    }

    private void HandleDirtCultivated(object sender, EventArgs eventArgs)
    {
        OnDirtCultivated onDirtCultivated = (OnDirtCultivated) eventArgs;

        Instantiate(_cultivatedDirt).transform.position = onDirtCultivated.Tile.transform.position;
    }

    private void HandleTileCollected(object sender, EventArgs eventArgs)
    {
        OnCropCollected onCropCollected = (OnCropCollected)eventArgs;

        Instantiate(_dirtTile).transform.position = onCropCollected.CollectedFromTile.transform.position;
    }
    private void HandleGrassPlowed(object sender, EventArgs eventArgs)
    {
        OnGrassPlowed onGrassPlowed = (OnGrassPlowed)eventArgs;
        
        Instantiate(_dirtTile).transform.position = onGrassPlowed.PlowedTile.transform.position;
    }
}
