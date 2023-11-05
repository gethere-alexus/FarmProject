using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraConfigurator : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _zoomingSensitivity = .5f;
    [SerializeField] private float _minZoomScale = -10;
    [SerializeField] private float _maxZoomScale = -17;
    
    private float _currentCameraZoom;
    private Vector3  _currentCameraPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _currentCameraZoom = _minZoomScale;
        _currentCameraPosition = new Vector3(0, 0, _currentCameraZoom);
        
    }
    
    void FixedUpdate()
    {
        _currentCameraZoom = this.gameObject.transform.position.z;
        
        bool isPlayerScrolling = Input.mouseScrollDelta.y != 0;
        if (isPlayerScrolling)
        {
           UpdateCameraAltitude();
        }
        
        this.gameObject.transform.position = _player.transform.position + _currentCameraPosition;   
    }
    
    private void UpdateCameraAltitude()
    {
        bool isCameraZoomingBeyondMaxLimits = _currentCameraZoom <= _maxZoomScale && Input.mouseScrollDelta.y < 0;
        bool isCameraZoomingBeyondMinLimits = _currentCameraZoom >= _minZoomScale && Input.mouseScrollDelta.y > 0;
        
        float mouseScroll = (isCameraZoomingBeyondMaxLimits || isCameraZoomingBeyondMinLimits) ? 0 : Input.mouseScrollDelta.y * _zoomingSensitivity;
        
        _currentCameraZoom += mouseScroll;
        _currentCameraPosition = new Vector3(0,0, _currentCameraZoom);
    }
}
