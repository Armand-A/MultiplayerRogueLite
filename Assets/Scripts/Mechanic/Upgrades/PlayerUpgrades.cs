using System.Collections.Generic;
using UnityEngine;
using EntityDataEnums;
using UnityEditor;

public class PlayerUpgrades : MonoBehaviour
{
    List<Upgrade> upgrades = new List<Upgrade>();
    private Dictionary<int, StatUpgradeValue> _upgrades = new Dictionary<int, StatUpgradeValue>();

    public List<Upgrade> Upgrades {get { return upgrades; }}

    public void AddUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        AddUpgradeStats(upgrade);
    }

    public void RemoveUpgrade(GUID id)
    {
        upgrades.Remove(upgrades.Find(x => x.UpgradeID == id));
    }

    private void AddUpgradeStats(Upgrade upgrade)
    {
        for (int i = 0; i < upgrade.UpgradeStats.Count; i++)
        {
            if (upgrade.UpgradeStats[i].ValueType == ValueTypeEnum.Flat)
                _upgrades[(int) upgrade.UpgradeStats[i].StatType].Flat += upgrade.UpgradeStats[i].Value;
            else
                _upgrades[(int)upgrade.UpgradeStats[i].StatType].Percent += upgrade.UpgradeStats[i].Value;
        }

        for (int i = 0; i < upgrade.UpgradeEStats.Count; i++)
        {
            if (upgrade.UpgradeEStats[i].ValueType == ValueTypeEnum.Flat)
                _upgrades[(int)upgrade.UpgradeEStats[i].StatType + (int)upgrade.UpgradeEStats[i].ElementType].Flat += upgrade.UpgradeEStats[i].Value;
            else
                _upgrades[(int)upgrade.UpgradeEStats[i].StatType + (int)upgrade.UpgradeEStats[i].ElementType].Percent += upgrade.UpgradeEStats[i].Value;
        }
    }


    public class StatUpgradeValue
    {
        public float Flat = 0;
        public float Percent = 0;
    }
}