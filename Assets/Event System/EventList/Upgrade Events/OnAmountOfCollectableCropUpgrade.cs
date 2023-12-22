using System;

public class OnAmountOfCollectableCropUpgrade : EventArgs
{
   public float Boost;

   public OnAmountOfCollectableCropUpgrade(float boost)
   {
      Boost = boost;
   }
      
}
