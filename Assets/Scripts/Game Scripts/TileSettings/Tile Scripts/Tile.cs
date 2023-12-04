using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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

public class Tile : SaveableObjects
{
    protected virtual void CreateObjectSprite(Sprite tileSprite)
    {
        SpriteRenderer objectSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        objectSpriteRenderer.sprite = tileSprite;
    }
}
