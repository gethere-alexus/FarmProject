using System;
using UnityEngine;

public enum BushLifeStages
{
    FirstGrowingStage,SecondGrowingStage,ThirdGrowingStage,LastGrowingStage,
    FirstDecayingStage,SecondDecayingStage,ThirdDecayingStage,LastDecayingStage,
}
public class BushGrowController : MonoBehaviour
{
    private BushSpriteUpdater _bushSpriteUpdater;
    private BushCropController _bushCropController;

    private BushLifeStages _currentLifeStage = BushLifeStages.FirstGrowingStage;
    
    private string[] _bushLifeStagesArray;
    
    [SerializeField] private float _lifeCycleTime = 30.0f;
    
    private float _timePastSincePlanted, _timePastSinceGrew, _timePastSinceIncreased;
    private float _changeSpriteAfter;
    
    private int _amountOfGrowStages; 

    private bool _isBushReadyToCrop, _isBushDecaying, _isLifeCycleOver;
    
    private CultivatedDirt _onPlantedTile;
    private void Start()
    {
        _bushSpriteUpdater = this.gameObject.GetComponent<BushSpriteUpdater>();
        _bushCropController = this.gameObject.GetComponent<BushCropController>();

        _bushLifeStagesArray = Enum.GetNames(typeof(BushLifeStages));
        _amountOfGrowStages = Enum.GetNames(typeof(BushLifeStages)).Length;
        
        _changeSpriteAfter = _lifeCycleTime / _amountOfGrowStages;
        
        UpdateBushStage();
    }

    private void Update()
    {
        _timePastSincePlanted += Time.deltaTime;
        _timePastSinceIncreased += Time.deltaTime;

        if (!_isLifeCycleOver && _timePastSinceIncreased >= _changeSpriteAfter)
        {
            IncreaseStage();
            _timePastSinceIncreased = 0;
        }
    }
    private void UpdateBushStage()
    {
        bool isBushReady = _currentLifeStage == BushLifeStages.LastGrowingStage;
        bool isDecayingStarted = _currentLifeStage == BushLifeStages.FirstDecayingStage;
        
        if (isBushReady)
        {
            _bushCropController.SetBushReadiness(true);
        }
        else if (isDecayingStarted)
        {
            _isBushDecaying = true;
        }
        
        _bushCropController.GiveStageCrop();
        _bushSpriteUpdater.SetSprite(_currentLifeStage);
    }

    public void IncreaseStage()
    {
        int indexOfCurrentStage = Array.IndexOf(_bushLifeStagesArray, _currentLifeStage.ToString());
        
        Enum.TryParse(_bushLifeStagesArray[indexOfCurrentStage + 1], out _currentLifeStage);
        
        _isLifeCycleOver = indexOfCurrentStage + 2 >= _bushLifeStagesArray.Length;
        
        UpdateBushStage();
    }
    public float GetTimeNeedToGrow
    {
        get => _lifeCycleTime;
    }

    public int GetAmountOfStages
    {
        get => _amountOfGrowStages;
    }
    public bool CheckIfDecaying
    {
        get => _isBushDecaying;
    }

    public float GetTimePastSincePlanted
    {
        get => _timePastSincePlanted;
    }
}
