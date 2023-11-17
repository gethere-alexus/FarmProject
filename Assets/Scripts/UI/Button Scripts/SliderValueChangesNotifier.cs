using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChangesNotifier : MonoBehaviour
{
   private Slider _slider;

   private void OnEnable()
   {
      _slider = this.gameObject.GetComponent<Slider>();
   }

   public void NotifyValueChanged()
   {
      GlobalEventBus.Sync.Publish(this, new OnSliderChanged(_slider.value));
   }
}
