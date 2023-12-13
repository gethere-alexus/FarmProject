using UnityEngine;

public class FinancialPresentController : MonoBehaviour
{
    [SerializeField] private int _moneyToCollect;
    
    public void ProccessPresentCollection()
    {
        GlobalEventBus.Sync.Publish(this, new OnMoneyProvided(_moneyToCollect));
        GlobalEventBus.Sync.Publish(this, new OnFinancialPresentClaimed());
        
        Destroy(this.gameObject);
    }
    
    public int SetMoneyToCollect
    {
        set => _moneyToCollect = value;
    }
}
