using System;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [SerializeField] private GameObject _dirtTile;
    [SerializeField] private GameObject _cultivatedDirt;
    
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMapCreated>(SetPrefabs);
        
        GlobalEventBus.Sync.Subscribe<OnGrassPlowed>(HandleGrassPlowed);
        GlobalEventBus.Sync.Subscribe<OnDirtCultivated>(HandleDirtCultivated);
        GlobalEventBus.Sync.Subscribe<OnCropCollected>(HandleTileCollected);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(SetPrefabs);
        GlobalEventBus.Sync.Unsubscribe<OnGrassPlowed>(HandleGrassPlowed);
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivated>(HandleDirtCultivated);
        GlobalEventBus.Sync.Unsubscribe<OnCropCollected>(HandleTileCollected);
    }

    private void SetPrefabs(object sender, EventArgs eventArgs)
    {
        OnMapCreated onMapCreated = (OnMapCreated)eventArgs;
        
        _dirtTile = onMapCreated.DirtTile;
        _cultivatedDirt = onMapCreated.CultivatedDirt;
    }

    private void HandleDirtCultivated(object sender, EventArgs eventArgs)
    {
        OnDirtCultivated onDirtCultivated = (OnDirtCultivated) eventArgs;

       SetNewTile(onDirtCultivated.Tile, _cultivatedDirt);
    }

    private void HandleTileCollected(object sender, EventArgs eventArgs)
    {
        OnCropCollected onCropCollected = (OnCropCollected)eventArgs;
        
        SetNewTile(onCropCollected.CollectedFromTile, _dirtTile);
    }
    private void HandleGrassPlowed(object sender, EventArgs eventArgs)
    {
        OnGrassPlowed onGrassPlowed = (OnGrassPlowed)eventArgs;
        SetNewTile(onGrassPlowed.PlowedTile, _dirtTile);
    }

    private void SetNewTile(GameObject changedTile, GameObject newTile)
    {
        Transform parent = changedTile.transform.parent;
        Instantiate(newTile, parent).transform.position = changedTile.transform.position;
        Destroy(changedTile);
    }
}
