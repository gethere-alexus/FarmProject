using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderValueChangedNotifier : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = this.gameObject.GetComponent<Slider>();
    }

    public void ValueChangedNotify()
    {
        GlobalEventBus.Sync.Publish(this, new OnMapSizeChanged((int)_slider.value));
    }
}
