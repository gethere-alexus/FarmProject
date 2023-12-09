using UnityEngine;
public class PlayerInputController : MonoBehaviour
{
    private GameControls _gameControls;

    private void OnEnable()
    {
        _gameControls.Enable();
    }
    private void OnDisable()
    {
        _gameControls.Disable();
    }
    private void Awake()
    {
        _gameControls = new GameControls();
        
        _gameControls.Player.Move.performed += context => Move(context.ReadValue<Vector2>());
        _gameControls.Player.Move.canceled += context => Stop();
        
        _gameControls.Player.Click.started += context => Click(); 
    }

    private void Click()
    {
        GlobalEventBus.Sync.Publish(this, new OnMouseButtonPressed());
    }
    private void Move(Vector2 input)
    {
        GlobalEventBus.Sync.Publish(this, new OnMovementActionPerformed(input.x, input.y));
    }
    private void Stop()
    {
        GlobalEventBus.Sync.Publish(this, new OnMovementActionCanceled());
    }
    
}
