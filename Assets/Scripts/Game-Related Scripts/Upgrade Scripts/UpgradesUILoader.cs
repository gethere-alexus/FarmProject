using System;
using UnityEngine;

public class UpgradesUILoader : MonoBehaviour
{
    [SerializeField] private UpgradeUIConfigurator _upgradeUIConfigurator;

    private UpgradesHandler _upgradesHandler;

    private void OnEnable()
    {
        _upgradesHandler = GameObject.FindWithTag("UpgradesController").GetComponent<UpgradesHandler>();
    }

    private void Start()
    {
        InstantiateUpgradeUIElements();
    }

    private void InstantiateUpgradeUIElements()
    {
        foreach (var upgrade in _upgradesHandler.GetActiveUpgrades())
        {
            UpgradeUIConfigurator instance = Instantiate(_upgradeUIConfigurator, this.gameObject.transform);

            ConfigureUpgradeUI(instance, upgrade);
        }
    }
    public void UpdateUpgradeUIElement(UpgradeUIConfigurator upgradeUIConfigurator)
    {
        Upgrade updatingUpgrade = _upgradesHandler.GetUpgradeByID(upgradeUIConfigurator.UpgradeID);
        
        ConfigureUpgradeUI(upgradeUIConfigurator, updatingUpgrade);
    }
    private void ConfigureUpgradeUI(UpgradeUIConfigurator configuratorToSetUp, Upgrade upgrade)
    {
        configuratorToSetUp.SetTrackingUpgrade(upgrade);
        configuratorToSetUp.UpgradeID = upgrade.UpgradeID;
        configuratorToSetUp.SetDescription(upgrade.Description);
        configuratorToSetUp.SetUpgradePrice(upgrade.UpgradeCost);
        configuratorToSetUp.SetUpgradeProgress(upgrade.CurrentUpgradeLevel,upgrade.MaximumUpgradeLevel);
            
    }
}
