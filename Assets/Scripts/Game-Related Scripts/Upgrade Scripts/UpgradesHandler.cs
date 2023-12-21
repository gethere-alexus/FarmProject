using System.Collections.Generic;
using UnityEngine;

public class UpgradesHandler : MonoBehaviour
{
   [SerializeField] private Upgrade[] _upgrades;
   [SerializeField] private List<Upgrade> _instantiatedUpgrades;
   
   private void Start()
   {
      int idToSet = 0;
      foreach (var upgrade in _upgrades)
      {
         upgrade.UpgradeID = idToSet;
         idToSet++;
         _instantiatedUpgrades.Add(Instantiate(upgrade, this.gameObject.transform));
      }
   }

   public Upgrade[] GetActiveUpgrades()
   {
      return _instantiatedUpgrades.ToArray();
   }

   public Upgrade GetUpgradeByID(int id)
   {
      foreach (var upgrade in _instantiatedUpgrades)
      {
         if (upgrade.UpgradeID == id)
         {
            return upgrade;
         }
      }
      
      Debug.LogError($"Upgrade with ID {id} wasn't found !");
      return null;
   }
}
