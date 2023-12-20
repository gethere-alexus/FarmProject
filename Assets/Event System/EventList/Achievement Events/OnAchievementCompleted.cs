using System;


public class OnAchievementCompleted : EventArgs
{
   public string Description;

   public OnAchievementCompleted(string description = "default")
   {
      Description = description;
   }
}
