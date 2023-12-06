using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private string _runningTrigger = "isRunning";

    private SpriteRenderer _spriteRenderer;
    private Animator _playerAnimator;

    private void OnEnable()
    {
        _playerAnimator = this.gameObject.GetComponent<Animator>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            
        GlobalEventBus.Sync.Subscribe<OnPlayerMoved>(MoveHandler);
        GlobalEventBus.Sync.Subscribe<OnPlayerStopped>(MoveHandler);
    }

    private void MoveHandler(object sender, EventArgs eventArgs)
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
