using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyChangesCleaner : MonoBehaviour
{
    private enum  MessageTypes
    {
        NotEnoughMoney,
        MoneyChanges,
    }

    [SerializeField] private MessageTypes _currentMessageType;

    private string _currentAnimation;
    private Animation _animation;
    // Start is called before the first frame update
    void Start()
    {
        _currentAnimation = _currentMessageType.ToString();
        _animation = GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_animation.IsPlaying(_currentAnimation))
        {
            Destroy(this.gameObject);
        }
    }
}
