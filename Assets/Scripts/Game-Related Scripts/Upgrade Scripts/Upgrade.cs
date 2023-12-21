using UnityEngine;
public abstract class Upgrade : MonoBehaviour
{
    private MoneyController _moneyController;
    [SerializeField] private int _upgradeID;
    private int _currentUpgradeLevel;
    
    [SerializeField] private string _description;
    [SerializeField] private int _maximumUpgradeLevel;
    [SerializeField] protected float _upgradeBoost;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _stepCostMultiplication = 2;

    private void OnEnable()
    {
        _moneyController = GameObject.FindWithTag("MoneyContoller").GetComponent<MoneyController>();
    }

    public virtual void IncreaseUpgradeLevel()
    {
        bool isEnoughMoney = _moneyController.CheckIsEnoughMoney(_upgradeCost);

        if (isEnoughMoney)
        {
            bool isBeyondMaximumLevel = _currentUpgradeLevel + 1 > _maximumUpgradeLevel;
            
            _currentUpgradeLevel = isBeyondMaximumLevel ? _maximumUpgradeLevel : _currentUpgradeLevel + 1;
            
            GlobalEventBus.Sync.Publish(this, new OnMoneyProvided(-_upgradeCost));
            _upgradeCost *= _stepCostMultiplication;
        }
    }

    public int UpgradeID
    {
        get => _upgradeID;
        set => _upgradeID = value;
    }
    
    public string Description
    {
        get => _description;
    }

    public int CurrentUpgradeLevel
    {
        get => _currentUpgradeLevel;
    }

    public int UpgradeCost
    {
        get => _upgradeCost;
    }
    
    public int MaximumUpgradeLevel
    {
        get => _maximumUpgradeLevel;
    }
}
