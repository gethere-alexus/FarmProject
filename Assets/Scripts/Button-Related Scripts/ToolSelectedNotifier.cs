using UnityEngine;

public enum ToolTypes {Shovel, Hoe, Sickle, Bag, None}
public class ToolSelectedNotifier : MonoBehaviour
{
    [SerializeField] private ToolTypes _chosenToolType;
    
    public void ToolChosen()
    {
        GlobalEventBus.Sync.Publish(this, new OnToolChosen(_chosenToolType));
    }
}
