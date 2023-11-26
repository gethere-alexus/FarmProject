using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushRenderAdjuster : MonoBehaviour
{
    private CultivatedDirt _usedTile;
    private SpriteRenderer _spriteRenderer;
    private void OnEnable()
    {
        _usedTile = GetComponentInParent<CultivatedDirt>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(RenderingHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnPlayerMoved>(RenderingHandler);
    }

    private void RenderingHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnPlayerMoved onPlayerMoved)
        {
            bool isPlayerAboveBush = _usedTile.transform.position.y + 0.6f < onPlayerMoved.PlayerPositionY;
            _spriteRenderer.sortingOrder = isPlayerAboveBush ? 2 : 1;
        }
    }
}
