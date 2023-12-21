using System;
using UnityEngine;

public class AmountOfCultivatingStagesUpgrade : Upgrade
{
    [SerializeField] private DirtTile _dirtPrefab;

    private const float _defaultStageReduction = 1;

    private void Start()
    {
        _dirtPrefab.StageReduction = _defaultStageReduction;
    }

    public override void IncreaseUpgradeLevel()
    {
        base.IncreaseUpgradeLevel();
        _dirtPrefab.StageReduction = _dirtPrefab.StageReduction + _upgradeBoost;
    }
}
