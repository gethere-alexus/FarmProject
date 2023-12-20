using System;
using UnityEngine;

public class AchievementCompletedNotifier : MonoBehaviour
{
   [SerializeField] private UIAchievementNotificationConfigurator _UIAchievementNotifier;
   
   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnAchievementCompleted>(ProcessCompletionSignal);
   }

   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnAchievementCompleted>(ProcessCompletionSignal);
   }

   private void ProcessCompletionSignal(object sender, EventArgs eventArgs)
   {
      OnAchievementCompleted onAchievementCompleted = (OnAchievementCompleted)eventArgs;
      InstantiateMessage(onAchievementCompleted.Description);
   }
   public void InstantiateMessage(string description)
   {
      UIAchievementNotificationConfigurator instance = Instantiate(_UIAchievementNotifier);
      
      instance.SetDescription(description);
   }
}
