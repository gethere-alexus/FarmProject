using System;
using UnityEngine;

public class OnGamePausePerformed : EventArgs
{
   public bool IsGamePaused;

   public OnGamePausePerformed(bool isPaused)
   {
      Debug.Log($"Game paused : {isPaused}");
      IsGamePaused = isPaused;
   }
}
