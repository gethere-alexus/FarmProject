using System;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class ChangePropertyText : MonoBehaviour
{

    [SerializeField] private TMP_Text _propertyText;
    [SerializeField] private string[] _propertyDescripitons;
    [SerializeField] private string _defaultText = "Map size : ";
    private string _sizeDescription;

    private int _maxSliderValue = 0;

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnSliderChanged>(SliderValueChangedHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnSliderChanged>(SliderValueChangedHandler);
    }

    private void SliderValueChangedHandler(object sender, EventArgs eventArgs)
    {
        OnSliderChanged onSliderChanged = (OnSliderChanged)eventArgs; 
        SliderValueChangesNotifier sliderValueChangesNotifier = (SliderValueChangesNotifier)sender;
        if (sliderValueChangesNotifier == GetComponentInChildren<SliderValueChangesNotifier>())
        {
            _maxSliderValue = (int)sliderValueChangesNotifier.gameObject.GetComponent<Slider>().maxValue;
            _sizeDescription = UpdateSizeDescription(onSliderChanged.Value);
            _propertyText.text = GetStringToDisplay(_sizeDescription);   
        }
    }

    private string UpdateSizeDescription(float sliderValue)
    {
        int distanceToChange = _maxSliderValue / _propertyDescripitons.Length;
        int stage = (int)Math.Floor(sliderValue / distanceToChange);
        
        stage = stage == 0 ? stage : stage - 1;
        
        return _propertyDescripitons[stage];
    }
    

    private string GetStringToDisplay(string sizeDescription)
    {
        return _defaultText + sizeDescription;
    }
}
