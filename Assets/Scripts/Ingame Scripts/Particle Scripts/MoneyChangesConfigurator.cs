using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyChangesConfigurator : MonoBehaviour
{
    private TMP_Text _text;
    private void OnEnable()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void SetAmountOfChangedMoney(int amountOfMoney)
    {
        string textToSet = amountOfMoney > 0 ? $"+{amountOfMoney}" : amountOfMoney.ToString();
        _text.text = textToSet;
    }
}
