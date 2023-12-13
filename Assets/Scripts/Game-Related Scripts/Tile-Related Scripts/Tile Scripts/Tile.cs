using System;
using UnityEngine;

public interface IPlantable
{
    void Plant();
}

public interface IPlowable
{
    void Plow();
}
public interface ICultivatable
{
    void Cultivate();
}

public class Tile : MonoBehaviour
{

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Tile";

    }

    protected virtual void CreateObjectSprite(Sprite tileSprite)
    {
        SpriteRenderer objectSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        objectSpriteRenderer.sprite = tileSprite;
    }
}
