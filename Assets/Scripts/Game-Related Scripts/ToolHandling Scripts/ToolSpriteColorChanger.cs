using System;
using UnityEngine;
using UnityEngine.UI;

public class ToolSpriteColorChanger : MonoBehaviour
{
   [SerializeField] private ToolTypes _tool;

   private Color _toolPickedColor = Color.black;
   private Color _toolUnPickedColor = Color.white;
   
   private Image _toolImage;

   private void Awake()
   {
      _toolImage = this.gameObject.GetComponent<Image>();
   }

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnToolSwitched>(ChangeColor);
   }

   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnToolSwitched>(ChangeColor);
   }

   private void ChangeColor(object sender, EventArgs eventArgs)
   {
      OnToolSwitched onToolSwitched = (OnToolSwitched)eventArgs;

      if (_tool == onToolSwitched.NewToolType)
      {
         _toolImage.color = _toolPickedColor;
      }
      else
      {
         _toolImage.color = _toolUnPickedColor;
      }
   }
}
