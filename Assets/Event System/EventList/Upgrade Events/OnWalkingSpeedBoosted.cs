using System;
using UnityEngine;


public class OnWalkingSpeedBoosted : EventArgs
{
   public float Boost;
   public OnWalkingSpeedBoosted(float boost)
   {
      Boost = boost;
   }
}
