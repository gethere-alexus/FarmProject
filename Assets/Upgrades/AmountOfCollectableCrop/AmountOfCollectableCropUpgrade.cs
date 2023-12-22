using System;

public class AmountOfCollectableCropUpgrade : Upgrade
{
    public override void IncreaseUpgradeLevel()
    {
        base.IncreaseUpgradeLevel();
        GlobalEventBus.Sync.Publish(this, new OnAmountOfCollectableCropUpgrade(_upgradeBoost));
    }
}
