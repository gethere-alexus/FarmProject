using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrassPlowed : EventArgs
{
   public GameObject PlowedTile;

   public OnGrassPlowed(GameObject tile)
   {
      PlowedTile = tile;
   }
}
