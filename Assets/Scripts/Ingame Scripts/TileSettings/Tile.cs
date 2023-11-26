using System.Linq.Expressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

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
        _pathToSprite = "Sprites/Tiles/Grass";
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
        _pathToSprite = "Sprites/Tiles/Dirt";
        string pathToProcessBar = "Prefabs/CultivatingProcessBar";
        
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
        _processPrefabCanvas = Resources.Load<GameObject>(pathToProcessBar);
    }

    private void Start()
    {
        this.gameObject.transform.rotation = quaternion.identity;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _tileSprite;
    }

    public void Cultivate()
    {
        _currentCultivatingStage++;
        GlobalEventBus.Sync.Publish(this, new OnDirtCultivatingStageCompleted(this.gameObject, _currentCultivatingStage, _amountOfCultivatingStages));
    }
    public void ChangeToCultivatedDirt()
    {
        this.gameObject.AddComponent<CultivatedDirt>();
        Destroy(this);
    }
}

public class CultivatedDirt : Tile, IPlantable
{
    private string _pathToBushPrefab;
    [SerializeField] private bool _isTilePlanted, _isTileReadyToCollect;
    
    private List<Sprite> _bushStages = new List<Sprite>();
    private void Start()
    {
        _pathToSprite = "Sprites/Tiles/CultivatedDirt";
        _pathToBushPrefab = "Prefabs/Bushes/Bush";
        
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
        
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
            
        this.gameObject.transform.rotation = quaternion.identity;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _tileSprite;
    }

    public void Plant()
    {
        if (!_isTilePlanted)
        {
            _isTilePlanted = true;
            GlobalEventBus.Sync.Publish(this, new OnTilePlanted(this.gameObject));
            InstantiateBush();
        }
    }

    public void SetTileReadiness(bool isReady)
    {
        _isTileReadyToCollect = isReady;
    }
    private void InstantiateBush()
    {
        Vector3 positionFromTile = new Vector3(0, .5f, 0);
        GameObject bush = Instantiate(Resources.Load<GameObject>(_pathToBushPrefab), this.gameObject.transform);
        bush.transform.position = this.gameObject.transform.position + positionFromTile;
    }
}
public class Sand : Tile
{
    private void Start()
    {
        _pathToSprite = "Sprites/Tiles/Sand";
        _tileSprite = Resources.Load<Sprite>(_pathToSprite);
        
        CreateObjectSprite(_tileSprite);
    }
}

