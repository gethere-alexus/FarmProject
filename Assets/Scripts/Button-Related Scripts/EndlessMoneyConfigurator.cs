using System;
using UnityEngine;

public class EndlessMoneyConfigurator : MonoBehaviour
{
    private bool isEndLessModeTurnedOn = false;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("EndlessMoneyModeStatus", isEndLessModeTurnedOn ? 1 : 0);
    }

    public void SwitchMode()
    {
        isEndLessModeTurnedOn = !isEndLessModeTurnedOn;
        PlayerPrefs.SetInt("EndlessMoneyModeStatus", isEndLessModeTurnedOn ? 1 : 0);
    }
}
