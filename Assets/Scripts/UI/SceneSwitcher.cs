using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private float _coolDownTime = .8f;
    private string _previousSceneName;
    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(ButtonPressedHandler);
    }

    private void Start()
    {
        _previousSceneName = GetPreviousSceneName(_previousSceneName);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(ButtonPressedHandler);
    }
    private void ButtonPressedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnButtonPressed onButtonPressed)
        {
            LoadProperScene(onButtonPressed.PressedButtonType, onButtonPressed.SceneToSwitchName);
        }
    }

    private void LoadProperScene(ButtonClickController.ButtonTypes pressedButtonType, string sceneToSwitchName)
    {
        switch (pressedButtonType)
        {
            case ButtonClickController.ButtonTypes.QuitButton:
                StartCoroutine(QuitWithCd());
                break;
            case ButtonClickController.ButtonTypes.PreviousSceneButton:
                StartCoroutine(SwitchSceneWithCd(GetPreviousSceneName(_previousSceneName)));
                break;
            default:
                _previousSceneName = SceneManager.GetActiveScene().name;
                StartCoroutine(SwitchSceneWithCd(sceneToSwitchName));
                break;
        }
    }

    private string GetPreviousSceneName(string candidate)
    {
        string previousSceneName = String.Empty;
        
        bool isTheCurrentSceneDynamic = SceneManager.GetActiveScene().name == "Settings";
        
        if (!isTheCurrentSceneDynamic)
        {
            int indexOfPreviousScene;
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                indexOfPreviousScene = SceneManager.GetActiveScene().buildIndex - 1;
                previousSceneName = SceneUtility.GetScenePathByBuildIndex(indexOfPreviousScene);
                
                Debug.Log(SceneUtility.GetScenePathByBuildIndex(indexOfPreviousScene));
                Debug.Log(indexOfPreviousScene + " - " + previousSceneName);
            }
            else
            {
                previousSceneName = SceneManager.GetActiveScene().name;
            }
            return previousSceneName;
        }
        else
        {
            return candidate;
        }
    }
    IEnumerator QuitWithCd()
    {
        yield return new WaitForSeconds(_coolDownTime);
        if(!Application.isEditor) Application.Quit();
    }

    IEnumerator SwitchSceneWithCd(string sceneName)
    {
        yield return new WaitForSeconds(_coolDownTime);
        SceneManager.LoadScene(sceneName);
    }
}
