using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    Dictionary<int, Upgrade> upgrades = new Dictionary<int, Upgrade>();

    public void Add(UpgradeScript upgrade)
    {
        upgrades[upgrade.UpgradeID] = new Upgrade(upgrade);
    }

    public void GenerateUpgrades(PlayerData data)
    {
        
    }
}