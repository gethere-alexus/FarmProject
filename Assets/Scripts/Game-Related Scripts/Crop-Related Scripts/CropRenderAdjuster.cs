using UnityEngine;

public class CropRenderAdjuster : MonoBehaviour
{
    private CultivatedDirt _usedTile;
    private SpriteRenderer _spriteRenderer;
    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _usedTile = GetComponentInParent<CultivatedDirt>();
        _spriteRenderer.sortingOrder = -(int)_usedTile.transform.position.y;
    }
}
