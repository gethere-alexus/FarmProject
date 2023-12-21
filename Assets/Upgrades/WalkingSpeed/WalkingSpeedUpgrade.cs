public class WalkingSpeedUpgrade : Upgrade
{
    public override void IncreaseUpgradeLevel()
    {
        base.IncreaseUpgradeLevel();
        GlobalEventBus.Sync.Publish(this, new OnWalkingSpeedBoosted(_upgradeBoost));
    }
}
