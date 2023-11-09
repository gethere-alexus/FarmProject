using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    private GameObject _mapStorage;


    private void Start()
    {
        _mapStorage = GameObject.FindWithTag("MapStorage");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMapLoaded())
        {
            GlobalEventBus.Sync.Publish(this,new OnMapAbsent());
        }
    }

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
