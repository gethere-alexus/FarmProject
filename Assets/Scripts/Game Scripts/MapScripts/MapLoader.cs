
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    private GameObject _mapStorage;
    private bool isMapLoaded()
    {
        return _mapStorage.transform.childCount != 0;
    }

    private void LoadMap()
    {
        
    }

    private void UnLoadMap()
    {
        
    }

    private void SaveMap()
    {
        
    }
}
