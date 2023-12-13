using UnityEngine;

public class MoneyChangesCleaner : MonoBehaviour
{
    private enum  MessageTypes
    {
        NotEnoughMoney,
        MoneyChanges,
    }

    [SerializeField] 
    private MessageTypes _currentMessageType;
    private Animation _animation;
    
    private string _currentAnimation;
  
    void Start()
    {
        _currentAnimation = _currentMessageType.ToString();
        _animation = GetComponentInChildren<Animation>();
    }
    
    void Update()
    {
        if(!_animation.IsPlaying(_currentAnimation))
        {
            Destroy(this.gameObject);
        }
    }
}
