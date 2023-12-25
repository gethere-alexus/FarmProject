using System;
using UnityEngine;
using UnityEngine.UI;

public enum PropertyTypes {MapSize,Difficulty, EndlessMoney}
public class SliderValueChangesNotifier : MonoBehaviour
{
   [SerializeField] private PropertyTypes _propertyType;
   [SerializeField] private float _multiplicator = 1;
   private Slider _slider;
   private void OnEnable()
   {
      _slider = this.gameObject.GetComponent<Slider>();
      _slider.value = (int)Math.Ceiling(_slider.maxValue / 2);
      NotifyValueChanged();
   }

   public void NotifyValueChanged()
   {
      PlayerPrefs.SetFloat(_propertyType.ToString(), _slider.value * _multiplicator);
      GlobalEventBus.Sync.Publish(this, new OnSliderChanged(_slider.value, _propertyType));
   }
}
