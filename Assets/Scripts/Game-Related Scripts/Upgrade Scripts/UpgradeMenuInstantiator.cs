using UnityEngine;

public class UpgradeMenuInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeMenu;

    public void InstantiateMenu()
    {
        Instantiate(_upgradeMenu);
    }
}
