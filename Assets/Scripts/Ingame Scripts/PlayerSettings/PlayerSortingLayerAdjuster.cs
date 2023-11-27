using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSortingLayerAdjuster : MonoBehaviour
{
    private SpriteRenderer _playerSpawnRenderer;
    private void OnEnable()
    {
        _playerSpawnRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(LayerHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnPlayerMoved>(LayerHandler);
    }

    private void LayerHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnPlayerMoved onPlayerMoved)
        {
            _playerSpawnRenderer.sortingOrder = -(int)onPlayerMoved.PlayerPositionY + 1;
        }
    }
}