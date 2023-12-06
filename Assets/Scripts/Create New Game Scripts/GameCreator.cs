using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCreator : MonoBehaviour
{
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnSliderChanged>(OnSliderValueChangedHandler);
        SceneManager.sceneLoaded += SendMapInfo;
        DontDestroyOnLoad(this.gameObject);
    }

    private void SendMapInfo(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Create New Game")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnSliderValueChangedHandler(object sender, EventArgs eventArgs)
    {
        OnSliderChanged onSliderChanged = (OnSliderChanged)eventArgs;
        PlayerPrefs.SetFloat(onSliderChanged.PropertyType.ToString(), onSliderChanged.Value);
        Debug.Log(PlayerPrefs.GetFloat(onSliderChanged.PropertyType.ToString()));
    }
}
