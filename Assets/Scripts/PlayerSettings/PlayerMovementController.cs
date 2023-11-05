using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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

        _movementDirection = new Vector2(_horizontalInput, _verticalInput) * _speed;
        
        this.gameObject.transform.Translate(_movementDirection);
    }
}
