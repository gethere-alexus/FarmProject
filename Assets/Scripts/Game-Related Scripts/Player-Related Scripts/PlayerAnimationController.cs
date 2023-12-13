using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] 
    private string _runningTrigger = "isRunning";

    private SpriteRenderer _spriteRenderer;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = this.gameObject.GetComponent<Animator>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
            
        GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(MoveOnSignal);
        GlobalEventBus.Sync.Subscribe<OnPlayerStopped>(MoveOnSignal);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnPlayerMoved>(MoveOnSignal);
        GlobalEventBus.Sync.Unsubscribe<OnPlayerStopped>(MoveOnSignal);
    }

    private void MoveOnSignal(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnPlayerMoved onPlayerMoved)
        {
            _playerAnimator.SetBool(_runningTrigger , true);
            if (onPlayerMoved.HorizontalInput > 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (onPlayerMoved.HorizontalInput < 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            _playerAnimator.SetBool(_runningTrigger , false);
        }
    }
}
