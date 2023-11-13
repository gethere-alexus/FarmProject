using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMapSizeText : MonoBehaviour
{

    [SerializeField] private TMP_Text _mapSizeText;
    [SerializeField] private GameObject _sliderGameObject;
    
    private string _defaultText = "Maps size : ";
    private string _sizeDescription;
    
    private int _mapSize = 1;

    private Slider _slider;
    private void Start()
    {
        _slider = _sliderGameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if (_mapSize != _slider.value)
        {
            GlobalEventBus.Sync.Publish(this, new OnSliderChanged());
            _mapSize = Int32.Parse(_slider.value.ToString());
            _sizeDescription = UpdateSizeDescription(_mapSize);
            _mapSizeText.text = GetStringToDisplay(_sizeDescription);   
        }
    }

    private string UpdateSizeDescription(int mapSize)
    {
        return mapSize <= 2 ? "tiny" :
            mapSize <= 4 ? "small" :
            mapSize == 5 ? "default" :
            mapSize < 8 ? "large" : "HUUGEE!";
    }

    private string GetStringToDisplay(string sizeDescription)
    {
        return _defaultText + sizeDescription;
    }
}
