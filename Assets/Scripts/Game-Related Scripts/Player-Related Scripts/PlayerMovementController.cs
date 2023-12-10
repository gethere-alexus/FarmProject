using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
   [SerializeField] 
   private float _playerSpeed = 4f;

   [SerializeField] private float _smoothTime = 0.1f;

   private float _horizontalInput;
   private float _verticalInput;
   private float _yAcceleration = 0;
   private float _xAcceleration = 0;

   private bool _isPlayerMoving = false;
   
   private Rigidbody2D _playerRb2D;

   private void OnEnable()
   {
      _playerRb2D = this.gameObject.GetComponent<Rigidbody2D>();
      
      GlobalEventBus.Sync.Subscribe<OnMovementActionPerformed>(OnPlayerMovedHandler);
      GlobalEventBus.Sync.Subscribe<OnMovementActionCanceled>(OnPlayerStoppedHandler);
   }

   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnMovementActionPerformed>(OnPlayerMovedHandler);
      GlobalEventBus.Sync.Unsubscribe<OnMovementActionCanceled>(OnPlayerStoppedHandler);
   }

   private void FixedUpdate()
   {
      if (_isPlayerMoving)
      {
         float smoothedHorizontal = Mathf.SmoothDamp(0, _horizontalInput, ref _yAcceleration, _smoothTime);
         float smoothedVertical = Mathf.SmoothDamp(0, _verticalInput, ref _xAcceleration, _smoothTime);
         
         Move(smoothedHorizontal, smoothedVertical);
      }
   }

   private void OnPlayerMovedHandler(object sender, EventArgs eventArgs)
   {
      OnMovementActionPerformed onMovementActionPerformed = (OnMovementActionPerformed) eventArgs;
      
      _verticalInput = onMovementActionPerformed.VerticalInput;
      _horizontalInput = onMovementActionPerformed.HorizontalInput;

      _isPlayerMoving = true;
   }

   private void OnPlayerStoppedHandler(object sender, EventArgs eventArgs)
   {
      Stop();
   }
   
   private void Move(float horizontalInput, float verticalInput)
   {
      _playerRb2D.MovePosition(transform.position + new Vector3(horizontalInput, verticalInput, 0) * _playerSpeed);
      
      GlobalEventBus.Sync.Publish(this, new OnPlayerMoved(transform.position.x, transform.position.y, horizontalInput));
   }

   private void Stop()
   {
      _verticalInput = 0;
      _horizontalInput = 0;

      _yAcceleration = 0;
      _xAcceleration = 0;

      _isPlayerMoving = false;
      
      GlobalEventBus.Sync.Publish(this, new OnPlayerStopped());
   }
}
