using System;
using UnityEngine;
public class PlayerInputController : MonoBehaviour,IPauseable
{
    private GameControls _gameControls;
    
    private bool _isGamePaused = false;
    private void Awake()
    {
        _gameControls = new GameControls();
        
        _gameControls.Player.PauseGame.canceled += context => PauseGame();
        
        _gameControls.Player.Move.performed += context => Move(context.ReadValue<Vector2>());
        _gameControls.Player.Move.canceled += context => Stop();
        
        _gameControls.Player.Click.started += context => Click(); 
    }
    private void OnEnable()
    {
        _gameControls.Enable();
        GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProcessGamePausedSignal);
    }
    private void OnDisable()
    {
        _gameControls.Disable();
        GlobalEventBus.Sync.Unsubscribe<OnGamePausePerformed>(ProcessGamePausedSignal);
    }

    private void ProcessGamePausedSignal(object sender, EventArgs eventArgs)
    {
        OnGamePausePerformed onGamePausePerformed = (OnGamePausePerformed)eventArgs;
        SwitchPauseState(onGamePausePerformed.IsGamePaused);
    }
    public void SwitchPauseState(bool isPaused)
    {
        _isGamePaused = isPaused;
    }
    private void Click()
    {
        GlobalEventBus.Sync.Publish(this, new OnMouseButtonPressed());
    }
    private void Move(Vector2 input)
    {
        GlobalEventBus.Sync.Publish(this, new OnMovementActionPerformed(input.x, input.y));
    }

    private void PauseGame()
    {
        _isGamePaused = !_isGamePaused;
        
        GlobalEventBus.Sync.Publish(this, new OnGamePausePerformed(_isGamePaused));
    }
    private void Stop()
    {
        GlobalEventBus.Sync.Publish(this, new OnMovementActionCanceled());
    }
    
}
