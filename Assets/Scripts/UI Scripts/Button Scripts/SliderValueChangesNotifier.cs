using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PropertyTypes {MapSize,Difficulty}
public class SliderValueChangesNotifier : MonoBehaviour
{
   [SerializeField] private PropertyTypes _propertyType;
   [SerializeField] private int _multiplicator = 1;
   private Slider _slider;
   private void OnEnable()
   {
      _slider = this.gameObject.GetComponent<Slider>();
   }

   public void NotifyValueChanged()
   {
      PlayerPrefs.SetFloat(_propertyType.ToString(), _slider.value * _multiplicator);
      Debug.Log((_slider.value * _multiplicator));
      GlobalEventBus.Sync.Publish(this, new OnSliderChanged(_slider.value, _propertyType));
   }
}
