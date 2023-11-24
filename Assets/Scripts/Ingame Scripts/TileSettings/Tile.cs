using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public interface IPlantable
{
    void Plant();
}

public interface IPlowable
{
    void Plow();
}
public interface ICultivatable
{
    void Cultivate();
}

public class Tile : MonoBehaviour
{
    protected Sprite _tileSprite;
    protected bool _isWalkable;
    protected string _pathToSprite;
    protected virtual void CreateObjectSprite(Sprite tileSprite)
    {
        SpriteRenderer objectSpriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        objectSpriteRenderer.sprite = tileSprite;
    }
}
public class Grass : Tile, IPlowable
{
    private void OnEnable()
    {
        _pathToSprite = "Sprites/grass";
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
    }

    private void Start()
    {
        CreateObjectSprite(_tileSprite);
    }

    public void Plow()
    {
        this.gameObject.AddComponent<Dirt>();
        GlobalEventBus.Sync.Publish(this, new OnGrassPlowed());
        Destroy(this);
    }
}
public class Dirt : Tile, ICultivatable
{
    private GameObject _processPrefabCanvas;
    
    private Slider _processSlider;
    private int _amountOfCultivatingStages = 10;
    private int _currentCultivatingStage = 0;

    private void OnEnable()
    {
        _tileSprite = Resources.Load<Sprite>("Sprites/dirt");
        _processPrefabCanvas = Resources.Load<GameObject>("Prefabs/CultivatingProcessBar");
    }

    private void Start()
    {
        this.gameObject.transform.rotation = quaternion.identity;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _tileSprite;
    }

    public void Cultivate()
    {
        _currentCultivatingStage++;
        if (_currentCultivatingStage == 1)
        {
            _processSlider = Instantiate(_processPrefabCanvas).GetComponentInChildren<Slider>();
            _processSlider.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, 0.5f);
            _processSlider.maxValue = _amountOfCultivatingStages; 
            _processSlider.minValue = 0;
        }
        if (_currentCultivatingStage >= _amountOfCultivatingStages)
        {
            this.gameObject.AddComponent<CultivatedDirt>();
            Destroy(_processSlider.gameObject);
            Destroy(this);
        }
        _processSlider.value = _currentCultivatingStage;
    }
    
}

public class CultivatedDirt : Tile, IPlantable
{ 
    private void Start()
    {
        _tileSprite = Resources.Load<Sprite>("Sprites/dirtCultivated");
        this.gameObject.transform.rotation = quaternion.identity;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _tileSprite;
    }
    public void Plant() { }
}
public class Sand : Tile
{
    private void Start()
    {
        _pathToSprite = "Sprites/sand";
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
        
        CreateObjectSprite(_tileSprite);
    }
}

