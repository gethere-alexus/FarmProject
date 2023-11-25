using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCleaner : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = gameObject.GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if (_slider.value >= _slider.maxValue)
        {
            Destroy(this.gameObject);
        }
    }
}
