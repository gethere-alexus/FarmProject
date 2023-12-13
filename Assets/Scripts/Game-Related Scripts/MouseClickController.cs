using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickController : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnMouseButtonPressed>(ProcessMousePressed);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnMouseButtonPressed>(ProcessMousePressed);
    }

    private void ProcessMousePressed(object sender, EventArgs eventArgs)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
            
        Vector2 worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            
        if (hit.collider != null)
        {
            GlobalEventBus.Sync.Publish(this, new OnTileTriggered(hit.collider.gameObject));
        }
    }
    
}
