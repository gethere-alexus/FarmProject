using System;
using UnityEngine;

public class UpgradeProcessor : MonoBehaviour
{
    private Upgrade _trackingUpgrade;
    private UpgradeUIConfigurator _upgradeUIConfigurator;
    private UpgradesUILoader _upgradesUILoader;
    private bool _isButtonTurnedOff = false;

    private void OnEnable()
    {
        _upgradeUIConfigurator = this.gameObject.GetComponent<UpgradeUIConfigurator>();
        _upgradesUILoader = this.gameObject.GetComponentInParent<UpgradesUILoader>();
    }

    public void SetTrackingUpgrade(Upgrade upgrade)
    {
        _trackingUpgrade = upgrade;
    }

    public void TurnOffUpgrade()
    {
        _isButtonTurnedOff = true;
    }

    public void IncreaseUpgrade()
    {
        if (!_isButtonTurnedOff)
        {
            _trackingUpgrade.IncreaseUpgradeLevel();
            _upgradesUILoader.UpdateUpgradeUIElement(_upgradeUIConfigurator);
        }
    }
}
