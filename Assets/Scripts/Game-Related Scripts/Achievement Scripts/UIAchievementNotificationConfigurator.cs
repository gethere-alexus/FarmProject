using System;
using TMPro;
using UnityEngine;

public class UIAchievementNotificationConfigurator : MonoBehaviour
{
   [SerializeField] private TMP_Text _description;
   [SerializeField] private Animation _animation;
   public void SetDescription(string description)
   {
      _description.text = description;
   }
   private void FixedUpdate()
   {
      if (!_animation.isPlaying)
      {
         Destroy(this.gameObject);
      }
   }
}
