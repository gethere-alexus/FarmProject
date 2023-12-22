using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingTimeUpgrade : Upgrade
{
    public override void IncreaseUpgradeLevel()
    {
        base.IncreaseUpgradeLevel();
        GlobalEventBus.Sync.Publish(this, new OnGrowingTimeUpgraded(_upgradeBoost));
    }
}
