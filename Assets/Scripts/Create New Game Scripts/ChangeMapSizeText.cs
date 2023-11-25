using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMapSizeText : MonoBehaviour
{

    [SerializeField] private TMP_Text _mapSizeText;
    
    private string _defaultText = "Maps size : ";
    private string _sizeDescription;

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
        if (eventArgs is OnSliderChanged onSliderChanged)
        {
           SetNewSizeText(onSliderChanged.Value);
        }
    }

    private void SetNewSizeText(float value)
    {
        _sizeDescription = UpdateSizeDescription(value);
        _mapSizeText.text = GetStringToDisplay(_sizeDescription);   
    }

    private string UpdateSizeDescription(float mapSize)
    {
        return mapSize <= 2.0f ? "Tiny" :
            mapSize <= 4.0f ? "Small" :
            mapSize == 5.0f ? "Default" :
            mapSize < 8.0f ? "Large" : "Huge";
    }

    private string GetStringToDisplay(string sizeDescription)
    {
        return _defaultText + sizeDescription;
    }
}
