using System;

public class GrassTile : Tile, IPlowable
{
    public void Plow()
    {
        GlobalEventBus.Sync.Publish(this, new OnGrassPlowed(this.gameObject));
    }
}
