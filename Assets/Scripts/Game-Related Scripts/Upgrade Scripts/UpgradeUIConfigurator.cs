using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIConfigurator : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Slider _progressBar;

    private UpgradeProcessor _upgradeProcessor;
    
    [SerializeField] private int _upgradeID;


    private void OnEnable()
    {
        _upgradeProcessor = this.gameObject.GetComponent<UpgradeProcessor>();
    }

    public int UpgradeID
    {
        get => _upgradeID;
        set => _upgradeID = value;
    }

    public void SetDescription(string description)
    {
        _description.text = description;
    }

    public void SetTrackingUpgrade(Upgrade upgrade)
    {
        _upgradeProcessor.SetTrackingUpgrade(upgrade);
    }

    public void SetUpgradePrice(int price)
    {
        
        _price.text = $"${FormatMoneyText(price)}";
    }
    public void SetUpgradeProgress(int currentUpgrade, int maxUpgrade)
    {
        _progressBar.maxValue = maxUpgrade;
        _progressBar.value = currentUpgrade;

        if (_progressBar.maxValue == _progressBar.value)
        {
            _price.text = $"Max.";
            _upgradeProcessor.TurnOffUpgrade();
        }
    }
    private string FormatMoneyText(int valueOfMoney)
    {
        string stringToReturn = valueOfMoney.ToString();
      
        if(valueOfMoney > 999)
        {
            int charsAmount = 0;
            for(int i = stringToReturn.Length - 1; i >= 0; i--)
            {
                if(stringToReturn[i] != '.' && (i - 1 != 0 || i != 0)) charsAmount++;
                if(charsAmount == 3 && i != 0)
                {
                    stringToReturn = stringToReturn.Substring(0,i) + '.' + stringToReturn.Substring(i);
                    charsAmount = 0;
                }
            }
        }
        return $"{stringToReturn}";
    }
}
