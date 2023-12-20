using System;
using UnityEngine;

public class OnGamePausePerformed : EventArgs
{
   public bool IsGamePaused;

   public OnGamePausePerformed(bool isPaused)
   {
      IsGamePaused = isPaused;
   }
}
