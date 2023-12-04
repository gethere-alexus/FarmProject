using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Rigidbody2D _playerRb2D;
    
    private float _horizontalInput, _verticalInput;
    private Vector2 _movementDirection;

    private void OnEnable()
    {
        _playerRb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        _playerRb2D.velocity = new Vector2(_horizontalInput,_verticalInput) * _speed;

        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            GlobalEventBus.Sync.Publish(this, new OnPlayerMoved(transform.position.x, transform.position.y, _horizontalInput));
        }
        else
        {
            GlobalEventBus.Sync.Publish(this, new OnPlayerStopped());
        }
    }
    
}
