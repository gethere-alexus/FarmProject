using UnityEngine;

public class FinancialPresentExistanceController : MonoBehaviour, IDifficultyDepended, IPauseable
{
    private FinancialPresentConfigurator _financialPresentConfigurator;

    [SerializeField] private float _timeToDisapear = 20;
    
    private float _timePastSinceAppeared = 0;
    private float _modifiedTimeToDisapear;

    private bool _isScriptPaused;
    public void AdjustDifficultyDependedProperties()
    {
        int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());

        _modifiedTimeToDisapear = _timeToDisapear / difficulty;
    }
    public void DeletePresent()
    {
        GlobalEventBus.Sync.Publish(this, new OnFinancialPresentDeleted());
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    public void SwitchPauseState(bool isPaused)
    {
        _isScriptPaused = isPaused;
    }
    private void Start()
    {
        AdjustDifficultyDependedProperties();
    }

    private void FixedUpdate()
    {
        if (!_isScriptPaused)
        {
            _timePastSinceAppeared += Time.deltaTime;

            if (_timePastSinceAppeared >= _modifiedTimeToDisapear)
            {
                DeletePresent();
            }
        }
    }
}
