using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class DirtTile : Tile, ICultivatable
{
    private GameObject _processPrefabCanvas;
        
    private Slider _processSlider;
    
    private int _amountOfCultivatingStages = 2;
    private int _currentCultivatingStage = 0;

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
