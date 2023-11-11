using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    
    private Rigidbody2D _playerRB;
    private float _horizontalInput, _verticalInput;
    private Vector2 _movementDirection;
    void Start()
    {
        _playerRB = this.gameObject.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            Move();
        }
    }

    private void Move()
    {
        _movementDirection = new Vector2(_horizontalInput, _verticalInput) * _speed;
        this.gameObject.transform.Translate(_movementDirection);
        
        GlobalEventBus.Sync.Publish(this, new OnPlayerMoved(transform.position.x, transform.position.y));
    }
    
}
