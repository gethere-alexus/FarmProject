using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Rigidbody2D _playerRB2D;
    
    private float _horizontalInput, _verticalInput;
    private float _xMinBorderCoordinate = -0.5f, _xMaxBorderCoordinate; // to be deleted
    private float _yMinBorderCoordinate = -0.5f, _yMaxBorderCoordinate; // as well this
    private Vector2 _movementDirection;

    private void OnEnable()
    {
        _playerRB2D = this.gameObject.GetComponent<Rigidbody2D>();
        GlobalEventBus.Sync.Subscribe<OnMapCreated>(OnMapCreatedHandler);
    }

    private void OnMapCreatedHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnMapCreated onMapCreated)
        {
            _xMaxBorderCoordinate = onMapCreated.XMaxBorderCoordinate;
            _yMaxBorderCoordinate = onMapCreated.YMaxBorderCoordinate;
        }
    }

    void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Move();
    }

    private void Move()
    {
        _playerRB2D.velocity = new Vector2(_horizontalInput,_verticalInput) * _speed;
        
        GlobalEventBus.Sync.Publish(this, new OnPlayerMoved(transform.position.x, transform.position.y));
    }
    
}
