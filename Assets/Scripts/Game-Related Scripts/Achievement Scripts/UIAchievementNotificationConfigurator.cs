using System;
using TMPro;
using UnityEngine;

public class UIAchievementNotificationConfigurator : MonoBehaviour
{
   [SerializeField] private TMP_Text _description;
   public void SetDescription(string description)
   {
      _description.text = description;
   }
}
