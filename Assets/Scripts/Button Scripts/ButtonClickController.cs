using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickController : MonoBehaviour
{
    [SerializeField] private ButtonClickManager.ButtonTypes buttonType;

    public void SendButtonClickMessage()
    {
        GlobalEventBus.Sync.Publish(this, new OnButtonPressed(buttonType, SceneManager.GetActiveScene().name));
    }
}
