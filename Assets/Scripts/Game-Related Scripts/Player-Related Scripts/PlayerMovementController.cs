using System;
using UnityEngine;

public interface IPauseable
{ 
   public void SwitchPauseState(bool isPaused);
}
public class PlayerMovementController : MonoBehaviour , IPauseable
{
   [SerializeField] private float _playerSpeed = 4f;
   [SerializeField] private float _smoothTime = 0.1f;

   private float _horizontalInput;
   private float _verticalInput;
   private float _yAcceleration = 0;
   private float _xAcceleration = 0;

   private bool _isPlayerMoving = false;
   private bool _isScriptPaused = false;
   
   private Rigidbody2D _playerRb2D;

   private void Awake()
   {
      _playerRb2D = this.gameObject.GetComponent<Rigidbody2D>();
   }

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProccessGamePause);
      
      GlobalEventBus.Sync.Subscribe<OnMovementActionPerformed>(HandlePlayerMovedSignal);
      GlobalEventBus.Sync.Subscribe<OnMovementActionCanceled>(HandlePlayerStoppedSignal);
   }

   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnMovementActionPerformed>(HandlePlayerMovedSignal);
      GlobalEventBus.Sync.Unsubscribe<OnMovementActionCanceled>(HandlePlayerStoppedSignal);
   }

   private void ProccessGamePause(object sender, EventArgs eventArgs)
   {
      OnGamePausePerformed onGamePausePerformed = (OnGamePausePerformed)eventArgs;
      SwitchPauseState(onGamePausePerformed.IsGamePaused);
   }

   public void SwitchPauseState(bool isPaused)
   {
      _isScriptPaused = isPaused;
   }
   private void FixedUpdate()
   {
      if (_isPlayerMoving && !_isScriptPaused)
      {
         float smoothedHorizontal = Mathf.SmoothDamp(0, _horizontalInput, ref _yAcceleration, _smoothTime);
         float smoothedVertical = Mathf.SmoothDamp(0, _verticalInput, ref _xAcceleration, _smoothTime);
         
         Move(smoothedHorizontal, smoothedVertical);
      }
   }

   private void HandlePlayerMovedSignal(object sender, EventArgs eventArgs)
   {
      OnMovementActionPerformed onMovementActionPerformed = (OnMovementActionPerformed) eventArgs;
      
      _verticalInput = onMovementActionPerformed.VerticalInput;
      _horizontalInput = onMovementActionPerformed.HorizontalInput;

      _isPlayerMoving = true;
   }

   private void HandlePlayerStoppedSignal(object sender, EventArgs eventArgs)
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
