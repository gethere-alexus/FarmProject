using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    private float _coolDownTime = 1.5f;
    private string _previousSceneName;
    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        GlobalEventBus.Sync.Subscribe<OnButtonPressed>(ButtonPressedHandler);
    }

    private void Start()
    {
        _previousSceneName = SceneManager.GetActiveScene().name;
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

    private void LoadProperScene(ButtonClickManager.ButtonTypes pressedButtonType, string sceneToSwitchName)
    {
        switch (pressedButtonType)
        {
            case ButtonClickManager.ButtonTypes.QuitButton:
                StartCoroutine(QuitWithCD());
                break;
            case ButtonClickManager.ButtonTypes.PreviousSceneButton:
                StartCoroutine(SwitchSceneWithCD(_previousSceneName));
                break;
            default:
                _previousSceneName = SceneManager.GetActiveScene().name;
                StartCoroutine(SwitchSceneWithCD(sceneToSwitchName));
                break;
        }
    }
    IEnumerator QuitWithCD()
    {
        yield return new WaitForSeconds(_coolDownTime);
        if(!Application.isEditor) Application.Quit();
        //else EditorApplication.ExitPlaymode();
    }

    IEnumerator SwitchSceneWithCD(string sceneName)
    {
        yield return new WaitForSeconds(_coolDownTime);
        SceneManager.LoadScene(sceneName);
    }
}
