using System;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    [SerializeField] private float _cameraAltitude = 8f;
    
    private Camera _mainCamera;
    private bool _isPlayerMoving = false;
    private void OnEnable()
    {
        _mainCamera = Camera.main;
       GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(OnPlayerMovedHandler);
       GlobalEventBus.Sync.Subscribe<OnPlayerStopped>(OnPlayerMovedHandler);
       
       GlobalEventBus.Sync.Subscribe<OnMapCreated>(OnMapCreatedHandler);  
    }
    private void LateUpdate()
    {
        if (_isPlayerMoving)
        {
            UpdateCameraPosition();
        }
    }
    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnPlayerMoved>(OnPlayerMovedHandler);  
        GlobalEventBus.Sync.Unsubscribe<OnPlayerStopped>(OnPlayerMovedHandler);  
        GlobalEventBus.Sync.Unsubscribe<OnMapCreated>(OnMapCreatedHandler);  
    }

    private void OnPlayerMovedHandler(object sender, EventArgs eventArgs)
    {
        _isPlayerMoving = eventArgs is OnPlayerMoved onPlayerMoved;
    }
    
    private void OnMapCreatedHandler(object send, EventArgs eventArgs)
    {
        OnMapCreated onMapCreated = (OnMapCreated)eventArgs;
        
        Vector3 position = new Vector3(onMapCreated.PlayerSpawnPointX, onMapCreated.PlayerSpawnPointY, -_cameraAltitude);
        
        UpdateCameraPosition(position);
    }

    private void UpdateCameraPosition(Vector3 newPosition = new Vector3())
    {
        Vector3 newCameraPosition;
        
        if (newPosition == Vector3.zero)
        {
            newCameraPosition = new Vector3(transform.position.x, transform.position.y, -_cameraAltitude);
        }
        else
        {
            newCameraPosition = newPosition;
        }
        _mainCamera.gameObject.transform.position = newCameraPosition;
    }
}
