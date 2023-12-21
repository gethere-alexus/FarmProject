using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class DirtTile : Tile, ICultivatable, IDifficultyDepended
{
    private GameObject _processPrefabCanvas;
        
    private Slider _processSlider;
    
    [SerializeField] private int _amountOfCultivatingStages = 10;
    [SerializeField] private float _stagesReduction = 1;
    private int _currentCultivatingStage = 0;

    public void AdjustDifficultyDependedProperties()
    {
        int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());

        _amountOfCultivatingStages *= difficulty;
    }
    private void Awake()
    {
        _amountOfCultivatingStages = (int)Math.Floor((decimal)(_amountOfCultivatingStages / _stagesReduction));
        AdjustDifficultyDependedProperties();
    }
    private void OnEnable()
    {
        string pathToProcessBar = "Prefabs/CultivatingProcessBar";
        _processPrefabCanvas = Resources.Load<GameObject>(pathToProcessBar);
        this.gameObject.transform.rotation = quaternion.identity;
    }
    public void Cultivate()
    {
        _currentCultivatingStage++;
        GlobalEventBus.Sync.Publish(this, new OnDirtCultivatingStageCompleted(this.gameObject, _currentCultivatingStage, (int)_amountOfCultivatingStages));
    }
    public void ChangeToCultivatedDirt()
    {
        GlobalEventBus.Sync.Publish(this, new OnDirtCultivated(this.gameObject));
    }

    public int AmountOfCultivatingStages
    {
        get => _amountOfCultivatingStages;
        set => _amountOfCultivatingStages = value;
    }

    public float StageReduction
    {
        get => _stagesReduction;
        set => _stagesReduction = value;
    }
}
