using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private float _fadingAnimationTime = .8f;
    private string _previousSceneName;
    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(ProccessPressedButton);
    }

    private void Start()
    {
        _previousSceneName = GetPreviousSceneName(_previousSceneName);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnButtonPressed>(ProccessPressedButton);
    }
    private void ProccessPressedButton(object sender, EventArgs eventArgs)
    {
        OnButtonPressed onButtonPressed = (OnButtonPressed)eventArgs;
        LoadScene(onButtonPressed.PressedButtonType, onButtonPressed.SceneToSwitchName);
        
    }

    private void LoadScene(ButtonTypes pressedButtonType, string sceneToSwitchName)
    {
        switch (pressedButtonType)
        {
            case ButtonTypes.QuitButton:
                StartCoroutine(QuitWithCd());
                break;
            case ButtonTypes.PreviousSceneButton:
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
        yield return new WaitForSeconds(_fadingAnimationTime);
        if(!Application.isEditor) Application.Quit();
    }

    IEnumerator SwitchSceneWithCd(string sceneName)
    {
        yield return new WaitForSeconds(_fadingAnimationTime);
        SceneManager.LoadScene(sceneName);
    }
}
