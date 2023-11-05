
using System;
using UnityEngine;

[ExecuteInEditMode]
public class MapCreator : MonoBehaviour
{
    [SerializeField] private string _mapName;
    
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
    
    private GameObject _mapStorage;
    private GameObject _currentMap;
    private MapManager _currentMapManager; 
    private bool isMapLoaded;

    private void Start()
    {
        _mapStorage = GameObject.FindWithTag("MapStorage");
        
        if (isMapLoaded)
        {
            _currentMap = _mapStorage.transform.GetChild(0).gameObject;
            _currentMapManager = _currentMap.GetComponent<MapManager>();
        }
    }

    void Update()
    {
        isMapLoaded = _mapStorage.transform.childCount != 0;
        
        if (!isMapLoaded)
        {
            CreateMap(_mapName, _mapWidth,_mapHeight);
            isMapLoaded = true;
        }
    }

    private void CreateMap(string mapName, int width, int height)
    {
        GameObject mapObject = new GameObject();
        
        mapObject.name = $"{mapName}.map";
        
        MapManager mapManager = mapObject.AddComponent<MapManager>();
        mapManager.MapWidth = width;
        mapManager.MapHeight = height;
        
        mapObject.transform.parent = _mapStorage.transform;
        
        Grid map = new Grid(width, height, mapObject);
    }
    
    public int MapHeight
    {
        get => _mapHeight;
    }

    public int MapWidth
    {
        get => _mapWidth;
    }
}
