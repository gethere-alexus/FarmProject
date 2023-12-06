using System;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class ChangePropertyText : MonoBehaviour
{

    [SerializeField] private TMP_Text _propertyText;
    [SerializeField] private string[] _propertyDescripiton;
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
            SetNewPropertyText(onSliderChanged.Value);
        }
    }

    private void SetNewPropertyText(float value)
    {
        _sizeDescription = UpdateSizeDescription(value);
        _propertyText.text = GetStringToDisplay(_sizeDescription);   
    }

    private string UpdateSizeDescription(float mapSize)
    {
        int distanceToChange = (int)_maxSliderValue / _propertyDescripiton.Length;
        int stage = (int)Math.Floor((mapSize / distanceToChange)) >= _propertyDescripiton.Length ? _propertyDescripiton.Length - 1 : (int)Math.Floor((mapSize / distanceToChange));
        return _propertyDescripiton[stage];
    }
    

    private string GetStringToDisplay(string sizeDescription)
    {
        return _defaultText + sizeDescription;
    }
}
