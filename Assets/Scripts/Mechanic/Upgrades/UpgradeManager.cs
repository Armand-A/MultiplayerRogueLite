using EntityDataEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UpgradeRange;
using Debug = UnityEngine.Debug;

public class UpgradeManager : MonoBehaviour
{
    Dictionary<Guid, Upgrade> upgrades = new Dictionary<Guid, Upgrade>();

    private void Start()
    {
        DisplayUpgradeOptions(); 
    }

    public void Add(UpgradeScript upgrade)
    {
        if (upgrade.UpgradeID == null)
            upgrade.UpgradeID = Guid.NewGuid();
        upgrades[upgrade.UpgradeID] = new Upgrade(upgrade);
    }

    public void DisplayUpgradeOptions()
    {
        List<Upgrade> upgradeOptions = GenerateUpgrades(1);
        for (int i = 0; i < upgradeOptions.Count; i++)
        {
            Debug.Log(upgradeOptions[i].UpgradeID);
            Debug.Log(upgradeOptions[i].Price);
            for (int j = 0; j < upgradeOptions[i].UpgradeStats.Count; j++)
                Debug.Log(upgradeOptions[i].UpgradeStats[j].Value);
            for (int j = 0; j < upgradeOptions[i].UpgradeEStats.Count; j++)
                Debug.Log(upgradeOptions[i].UpgradeEStats[j].Value);
        }
    }

    public List<Upgrade> GenerateUpgrades(int upgradeQuantity)
    {
        List<Upgrade> upgradeOptions = new List<Upgrade>();
        for (int i = 0; i < upgradeQuantity; i++)
        {
            upgradeOptions.Add(GenerateUpgrade(UnityEngine.Random.Range(0, 3)));
        }
        return upgradeOptions;
    }

    private Upgrade GenerateUpgrade(int rarity)
    {
        Upgrade newUpgrade = new Upgrade(rarity);
        int nStats;

        switch (rarity)
        {
            case 0:
                nStats = UnityEngine.Random.Range(1, 2);
                RandomUpgrade(newUpgrade, Common, nStats, 3f);
                break;
            case 1:
                nStats = UnityEngine.Random.Range(2, 3);
                RandomUpgrade(newUpgrade, Rare, nStats, 5f);
                break;
            case 2:
                nStats = UnityEngine.Random.Range(3, 4);
                RandomUpgrade(newUpgrade, Epic, nStats, 10f);
                break;
            case 3:
                nStats = UnityEngine.Random.Range(4, 5);
                RandomUpgrade(newUpgrade, Legendary, nStats, 20f);
                break;
            default:
                break;
        }
        return newUpgrade;
    }

    private void RandomUpgrade(Upgrade newUpgrade, Dictionary<UpgradeableStatsEnum, UpgradeValues> selectedRange, int nStats, float priceMultiplier)
    {
        int price = 0;
        for (int i = 0; i < nStats; i++)
        {
            int valueType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ValueTypeEnum)).Length - 1);
            int statType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(UpgradeableStatsEnum)).Length - 1);

            UpgradeValue valueRange = selectedRange[(UpgradeableStatsEnum)statType].Values[valueType];
            float newValue = UnityEngine.Random.Range(valueRange.Min, valueRange.Max);

            price += (int)(newValue / (valueRange.Max - valueRange.Min) * priceMultiplier);

            if (UpgradeableStatsEnum.Attack == (UpgradeableStatsEnum)statType || UpgradeableStatsEnum.Defence == (UpgradeableStatsEnum)statType)
            {
                int eType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ElementTypesEnum)).Length - 1);
                newUpgrade.UpgradeEStats.Add(new UpgradeEStat((UpgradeableStatsEnum)statType, newValue, (ElementTypesEnum)eType));
            }
            else
            {
                newUpgrade.UpgradeStats.Add(new UpgradeStat((UpgradeableStatsEnum)statType, newValue));
            }
        }
        newUpgrade.Price = price;
    }
}