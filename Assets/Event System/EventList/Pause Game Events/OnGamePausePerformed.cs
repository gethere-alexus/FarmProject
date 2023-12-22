using System;
using UnityEngine;

public class OnGamePausePerformed : EventArgs
{
   public bool IsGamePaused;
   public bool DoesInstatiatePauseMenu;

   public OnGamePausePerformed(bool isPaused, bool doesInstatiatePauseMenu = false)
   {
      IsGamePaused = isPaused;
      DoesInstatiatePauseMenu = doesInstatiatePauseMenu;
   }
}
