using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
   [SerializeField] 
   private float _speed = 4f;
   
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

   private void OnPlayerMovedHandler(object sender, EventArgs eventArgs)
   {
      OnMovementActionPerformed onMovementActionPerformed = (OnMovementActionPerformed) eventArgs;
      Move(onMovementActionPerformed.HorizontalInput, onMovementActionPerformed.VerticalInput);
   }

   private void OnPlayerStoppedHandler(object sender, EventArgs eventArgs)
   {
      Stop();
   }
   
   private void Move(float horizontalInput, float verticalInput)
   {
      _playerRb2D.velocity = new Vector2(horizontalInput, verticalInput) * _speed;
      GlobalEventBus.Sync.Publish(this, new OnPlayerMoved(transform.position.x, transform.position.y, horizontalInput));
   }

   private void Stop()
   {
      _playerRb2D.velocity = Vector2.zero;
      GlobalEventBus.Sync.Publish(this, new OnPlayerStopped());
   }
}
