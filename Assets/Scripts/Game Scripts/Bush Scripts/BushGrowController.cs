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

    [SerializeField] 
    private float _timeNeedToGrow = 15.0f, _timeNeedToDecay = 120.0f;
    private float _timePastSincePlanted, _timePastSinceGrew, _timePastSinceIncreased;
    private float _changeSpriteAfter;
    
    private int _amountOfGrowingStages, _amountOfDecayingStages; 

    private bool _isBushReadyToCrop, _isBushDecaying, _isLifeCycleOver;
    
    private CultivatedDirt _onPlantedTile;
    private void Start()
    {
        _bushSpriteUpdater = this.gameObject.GetComponent<BushSpriteUpdater>();
        _bushCropController = this.gameObject.GetComponent<BushCropController>();

        _bushLifeStagesArray = Enum.GetNames(typeof(BushLifeStages));
        _amountOfGrowingStages = GetAmountOfGrowingStages();
        _amountOfDecayingStages = GetAmountOfDecayingStages();
        
        _changeSpriteAfter = _timeNeedToGrow / _amountOfGrowingStages;
        
        UpdateBushStage();
    }

    private int GetAmountOfGrowingStages()
    {
        int amount = 0;
        foreach(BushLifeStages value in Enum.GetValues(typeof(BushLifeStages)))
        {
            if (value == BushLifeStages.LastGrowingStage)
            {
                return amount;
            }
            amount++;
        }
        return 0;
    }

    private int GetAmountOfDecayingStages()
    {
        int amount = 0;
        bool countStarted = false;
        
        foreach(BushLifeStages value in Enum.GetValues(typeof(BushLifeStages)))
        {
            if (value == BushLifeStages.FirstDecayingStage)
            {
                countStarted = true;
            }
            if (countStarted)
            {
                if (value == BushLifeStages.LastDecayingStage)
                {
                    return amount;
                }
                amount++;
            }
        }

        return 0;
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
            _changeSpriteAfter = _timeNeedToDecay / _amountOfDecayingStages;
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
        get => (_timeNeedToGrow);
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
