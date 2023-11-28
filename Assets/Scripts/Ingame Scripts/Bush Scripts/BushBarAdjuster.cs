using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BushBarAdjuster : MonoBehaviour
{
    [SerializeField] private GameObject _barGameObject;
    private BushGrowController _bushGrowController;
    private Slider _slider;

    private void Start()
    {
        _bushGrowController = this.gameObject.GetComponent<BushGrowController>();
        _slider = _barGameObject.GetComponent<Slider>();
        _slider.value = 0;
        _slider.maxValue = _bushGrowController.GetTimeNeedToGrow;
        _slider.wholeNumbers = false;
    }

    private void Update()
    {
        _slider.value = _bushGrowController.GetTimePastSincePlanted;
    }
}
