using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Vector2 worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.transform.gameObject.TryGetComponent<DefaultTile>(out DefaultTile tileComponent))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
