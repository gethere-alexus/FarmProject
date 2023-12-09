using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    [SerializeField] private float _cameraAltitude = 8f;
    [SerializeField] private Camera _mainCamera;
    
    private bool isPlayerMoving = false;
    
    private void LateUpdate()
    {
        if (isPlayerMoving)
        {
            UpdateCameraPosition();
        }
    }

    private void OnEnable()
    {
       GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(OnPlayerMovedHandler);
       GlobalEventBus.Sync.Subscribe<OnPlayerStopped>(OnPlayerMovedHandler);
       GlobalEventBus.Sync.Subscribe<OnMapCreated>(OnMapCreatedHandler);  
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnPlayerMoved>(OnPlayerMovedHandler);  
        GlobalEventBus.Sync.Unsubscribe<OnPlayerStopped>(OnPlayerMovedHandler);  
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(OnMapCreatedHandler);  
    }

    private void OnPlayerMovedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnPlayerMoved onPlayerMoved)
        {
            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }
    }
    private void OnMapCreatedHandler(object send, EventArgs eventArgs)
    {
        OnMapCreated onMapCreated = (OnMapCreated)eventArgs;
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        _mainCamera.gameObject.transform.position =
            new Vector3(transform.position.x, transform.position.y, -_cameraAltitude);
    }
}
