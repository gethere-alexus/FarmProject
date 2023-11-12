
using System;
using UnityEditor;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private string _mapName;
    
    [SerializeField,Range(1, 100)] private int _mapWidth, _mapHeight;
    
    private GameObject _mapStorage, _currentMap;
    private bool _isMapLoaded;
    

    private void OnEnable()
    {
        _mapStorage = this.gameObject;
        GlobalEventBus.Sync.Subscribe<OnMapAbsent>(OnMapCreatedHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMapAbsent>(OnMapCreatedHandler);
    }
    private void OnMapCreatedHandler(object sender, EventArgs eventArgs)
    {
        CreateMap(_mapName,_mapWidth,_mapHeight);
    }

    private void CreateMap(string mapName, int width, int height)
    {
        GameObject mapObject = new GameObject();
        
        mapObject.name = $"{mapName}.map";
        mapObject.transform.parent = _mapStorage.transform;
        mapObject.AddComponent<MapManager>();
        
        Grid map = new Grid(width, height, mapObject);
        
        GlobalEventBus.Sync.Publish(this, new OnMapCreated(width,height));
    }
}
