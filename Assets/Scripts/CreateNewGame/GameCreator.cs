using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCreator : MonoBehaviour
{
    [SerializeField] private float _mapSize = 5f;
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnSliderChanged>(OnSliderValueChangedHandler);
        SceneManager.sceneLoaded += SendMapInfo;
        DontDestroyOnLoad(this.gameObject);
    }

    private void SendMapInfo(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            GlobalEventBus.Sync.Publish(this, new OnMapDataSent((int)_mapSize));
        }
        if (scene.name != "Create New Game")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnSliderValueChangedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnSliderChanged onSliderChanged)
        {
            _mapSize = onSliderChanged.Value;
        }
    }
}
