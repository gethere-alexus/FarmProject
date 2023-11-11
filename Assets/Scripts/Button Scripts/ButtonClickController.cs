using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickController : MonoBehaviour
{
    [SerializeField] private ButtonClickManager.ButtonTypes buttonType;

    public void SendButtonClickMessage()
    {
        GlobalEventBus.Sync.Publish(this, new OnButtonPressed(buttonType));
    }
}
