using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreator : MonoBehaviour
{
    [SerializeField, Tooltip("For Debugging")]
    private bool _isInDebugging = false;
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
        SceneManager.sceneLoaded += MapInit;
    }
    
    private void MapInit(Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        CreateMap();
    }
    private void CreateMap()
    {
        GameObject mapObject = new GameObject();
        int width, height;
        
        if (!_isInDebugging)
        {
            width = height = (int)PlayerPrefs.GetFloat(PropertyTypes.MapSize.ToString());
        }
        else
        {
            width = _mapWidth;
            height = _mapHeight;
        }
        
        
        mapObject.name = $"{_mapName}.map";
        mapObject.transform.parent = _mapStorage.transform;
        mapObject.AddComponent<MapManager>();
        
        Grid map = new Grid(width, height, mapObject, _borderTile, _sandTile, _grassTile, _dirtTile, _cultivatedDirt);
        
        GlobalEventBus.Sync.Publish(this, new OnMapCreated(width,height, _borderTile, _sandTile, _grassTile, _dirtTile, _cultivatedDirt));
    }
}
