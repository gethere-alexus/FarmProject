using System;
using UnityEngine;

public class OnDirtCultivated : EventArgs
{
   public GameObject Tile;

   public OnDirtCultivated(GameObject tile)
   {
      Tile = tile;
   }
}
