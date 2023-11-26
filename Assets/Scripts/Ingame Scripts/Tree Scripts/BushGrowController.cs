using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class BushGrowController : MonoBehaviour
{
    private SpriteRenderer _bushSpriteRendered;
    private List<Sprite> _bushStages = new List<Sprite>();
    private int _growMaxStages; // where max is ready to collect
    
    private float _timePastSincePlanted;
    private float _timePastSinceIncreased;
    private float _timeToGrow = 15.0f;
    private float _changeSpriteAfter;
    
    private int _currentGrowStage;
    private CultivatedDirt _onPlantedTile;
    private void Start()
    {
        _onPlantedTile = this.gameObject.GetComponentInParent<CultivatedDirt>();
        _bushSpriteRendered = this.gameObject.GetComponent<SpriteRenderer>();
        
        string[] bushPaths = new[]
        {
            "Sprites/BushStages/Stage1",
            "Sprites/BushStages/Stage2",
            "Sprites/BushStages/Stage3",
            "Sprites/BushStages/Stage4",
        };
        foreach (var path in bushPaths)
        {
            _bushStages.Add(Resources.Load<Sprite>(path));
        }

        _growMaxStages = _bushStages.Count - 1;
        _currentGrowStage = 0;
        
        _changeSpriteAfter = _timeToGrow / _bushStages.Count;
        UpdateBushStage();
    }

    private void Update()
    {
        _timePastSincePlanted += Time.deltaTime;
        _timePastSinceIncreased += Time.deltaTime;
        if (_timePastSinceIncreased >= _changeSpriteAfter)
        {
            _timePastSinceIncreased = 0;
            IncreaseStage();
        }
    }

    private void UpdateBushStage()
    {
        bool isBushReady = _currentGrowStage == _bushStages.Count - 1;
        _onPlantedTile.SetTileReadiness(isBushReady);
        _bushSpriteRendered.sprite = _bushStages[_currentGrowStage];
    }

    public void IncreaseStage()
    {
        bool isBeyondStages = _currentGrowStage + 1 > _growMaxStages;
        _currentGrowStage = isBeyondStages ? _growMaxStages : _currentGrowStage + 1;
        UpdateBushStage();
    }

    public void DecreaseStage()
    {
        bool isBelowZero = _currentGrowStage - 1 < 0;
        
        _currentGrowStage = isBelowZero ? 0 : _currentGrowStage - 1;
        UpdateBushStage();
    }
    public float GetTimeNeedToGrow
    {
        get => _timeToGrow;
    }

    public float GetTimePastSincePlanted
    {
        get => _timePastSincePlanted;
    }
}