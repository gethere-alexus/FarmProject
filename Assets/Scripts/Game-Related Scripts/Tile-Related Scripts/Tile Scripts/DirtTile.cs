using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class DirtTile : Tile, ICultivatable, IDifficultyDepended
{
    private GameObject _processPrefabCanvas;
        
    private Slider _processSlider;
    
    private int _amountOfCultivatingStages = 10;
    private int _currentCultivatingStage = 0;

    public void AdjustDifficultyDependedProperties()
    {
        int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());

        _amountOfCultivatingStages *= difficulty;
    }

    private void Awake()
    {
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
        GlobalEventBus.Sync.Publish(this, new OnDirtCultivatingStageCompleted(this.gameObject, _currentCultivatingStage, _amountOfCultivatingStages));
    }
    public void ChangeToCultivatedDirt()
    {
        GlobalEventBus.Sync.Publish(this, new OnDirtCultivated(this.gameObject));
    }
}
