
using System;
using UnityEditor;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField, Tooltip("For Debugging")]
    private bool isInDebugging = false;
    [SerializeField] private string _mapName;
    [SerializeField,Range(1, 100)] private int _mapWidth, _mapHeight;
    
    [SerializeField] private GameObject _borderTile;
    [SerializeField] private GameObject _sandTile;
    [SerializeField] private GameObject _grassTile;
    [SerializeField] private GameObject _dirtTile;
    [SerializeField] private GameObject _cultivatedDirt;
    
    private GameObject _mapStorage, _currentMap;
    private bool _isMapLoaded;
    

    private void OnEnable()
    {
        _mapStorage = this.gameObject;
        GlobalEventBus.Sync.Subscribe<OnMapDataSent>(OnMapCreatedHandler);
    }

    private void Start()
    {
        if(isInDebugging) CreateMap(_mapName, _mapWidth, _mapHeight);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMapDataSent>(OnMapCreatedHandler);
    }
    private void OnMapCreatedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnMapDataSent onMapDataSent)
        {
            CreateMap(_mapName, onMapDataSent.MapWidth,onMapDataSent.MapHeight);
        }
    }

    private void CreateMap(string mapName, int width, int height)
    {
        GameObject mapObject = new GameObject();
        
        mapObject.name = $"{mapName}.map";
        mapObject.transform.parent = _mapStorage.transform;
        mapObject.AddComponent<MapManager>();
        
        Grid map = new Grid(width, height, mapObject, _borderTile, _sandTile, _grassTile, _dirtTile, _cultivatedDirt);
        
        GlobalEventBus.Sync.Publish(this, new OnMapCreated(width,height, _borderTile, _sandTile, _grassTile, _dirtTile, _cultivatedDirt));
    }
}