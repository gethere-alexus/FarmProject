using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDirtCultivated : EventArgs
{
   public GameObject Tile;

   public OnDirtCultivated(GameObject tile)
   {
      Tile = tile;
   }
}
