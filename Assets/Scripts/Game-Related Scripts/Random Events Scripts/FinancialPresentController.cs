using UnityEngine;

public class FinancialPresentController : MonoBehaviour, IDifficultyDepended
{
    [SerializeField] private int _minimumAmountOfMoneyToPresent = 500;
    [SerializeField] private int _maximumAmountOfMoneyToPresent = 1000;
    
    private FinancialPresentExistanceController _presentExsitanceController;
    
    private int _moneyToCollect;
    
    private int _modifiedMinimumAmountOfMoneyToPresent;
    private int _modifiedMaximumAmountOfMoneyToPresent;

    private void OnEnable()
    {
        _presentExsitanceController = this.gameObject.GetComponent<FinancialPresentExistanceController>();
        AdjustDifficultyDependedProperties();
    }
    public void AdjustDifficultyDependedProperties()
    {
        int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());

        _modifiedMinimumAmountOfMoneyToPresent = _minimumAmountOfMoneyToPresent / difficulty;
        _modifiedMaximumAmountOfMoneyToPresent = _maximumAmountOfMoneyToPresent / difficulty;
      
    }

    public void ClaimPresent()
    {
        _moneyToCollect = Random.Range(_modifiedMinimumAmountOfMoneyToPresent, _modifiedMaximumAmountOfMoneyToPresent);
        
        GlobalEventBus.Sync.Publish(this, new OnMoneyProvided(_moneyToCollect));
        GlobalEventBus.Sync.Publish(this, new OnFinancialPresentClaimed());
        
        _presentExsitanceController.DeletePresent();
    }
    
    public int SetMoneyToCollect
    {
        set => _moneyToCollect = value;
    }
}
