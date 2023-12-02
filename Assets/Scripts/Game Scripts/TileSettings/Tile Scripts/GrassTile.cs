using UnityEngine;

public class GrassTile : Tile, IPlowable
{
    public void Plow()
    {
        GlobalEventBus.Sync.Publish(this, new OnGrassPlowed(this.gameObject));
        Destroy(this.gameObject);
    }
}
