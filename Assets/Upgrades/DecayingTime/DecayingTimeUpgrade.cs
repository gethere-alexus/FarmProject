public class DecayingTimeUpgrade : Upgrade
{
    public override void IncreaseUpgradeLevel()
    {
        base.IncreaseUpgradeLevel();
        GlobalEventBus.Sync.Publish(this, new OnDecayingTimeUpgraded(_upgradeBoost));
    }
}
