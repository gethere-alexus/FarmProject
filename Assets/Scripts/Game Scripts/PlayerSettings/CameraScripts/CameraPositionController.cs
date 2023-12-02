using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    [SerializeField] private float _cameraAltitude = 8f;
    private void OnEnable()
    {
       GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(OnPlayerMovedHandler);
       GlobalEventBus.Sync.Subscribe<OnMapCreated>(OnPlayerMovedHandler);  
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnPlayerMoved>(OnPlayerMovedHandler);
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(OnPlayerMovedHandler);  
    }

    private void OnPlayerMovedHandler(object send, EventArgs eventArgs)
    {
        if (eventArgs is OnPlayerMoved onPlayerMoved)
        {
            UpdateCameraPosition(onPlayerMoved.PlayerPositionX, onPlayerMoved.PlayerPositionY);
        }
        else if (eventArgs is OnMapCreated onMapCreated)
        {
            UpdateCameraPosition(onMapCreated.PlayerSpawnPointX,onMapCreated.PlayerSpawnPointY);
        }
    }

    private void UpdateCameraPosition(float xPlayerPos, float yPlayerPos)
    {
        Vector3 cameraPosition = new Vector3(xPlayerPos, yPlayerPos, (_cameraAltitude * -1));

        this.gameObject.transform.position = cameraPosition;
    }
}
