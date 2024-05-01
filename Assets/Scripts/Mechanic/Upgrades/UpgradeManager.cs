using EntityDataEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using static UpgradeRange;
using Debug = UnityEngine.Debug;

public static class UpgradeManager // : MonoBehaviour
{
    public static Dictionary<GUID, Upgrade> upgrades = new Dictionary<GUID, Upgrade>();

    /*    private void Start()
        {
            DisplayUpgradeOptions(); 
        }*/

/*    public static void Add(UpgradeScript upgrade)
    {
        if (upgrade.UpgradeID == null)
            upgrade.UpgradeID = GUID.Generate();
        upgrades[upgrade.UpgradeID] = new Upgrade(upgrade);
    }*/


    public static void DisplayUpgradeOptions()
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

    public static List<Upgrade> GenerateUpgrades(int upgradeQuantity)
    {
        List<Upgrade> upgradeOptions = new List<Upgrade>();
        for (int i = 0; i < upgradeQuantity; i++)
        {
            upgradeOptions.Add(GenerateUpgrade((Rarity) UnityEngine.Random.Range(0, 3)));
        }
        return upgradeOptions;
    }

    // Selects random upgrade rarity and use the RandomUpgrade function
    private static Upgrade GenerateUpgrade(Rarity rarity)
    {
        Upgrade newUpgrade = new Upgrade(rarity);
        int nStats;

        switch (rarity)
        {
            case Rarity.Common:
                nStats = UnityEngine.Random.Range(1, 2);
                RandomUpgrade(newUpgrade, Common, nStats, 3f);
                break;
            case Rarity.Rare:
                nStats = UnityEngine.Random.Range(2, 3);
                RandomUpgrade(newUpgrade, Rare, nStats, 5f);
                break;
            case Rarity.Epic:
                nStats = UnityEngine.Random.Range(3, 4);
                RandomUpgrade(newUpgrade, Epic, nStats, 10f);
                break;
            default:
                nStats = UnityEngine.Random.Range(4, 5);
                RandomUpgrade(newUpgrade, Legendary, nStats, 20f);
                break;
        }
        return newUpgrade;
    }

    // Creates the data for the upgrade
    private static void RandomUpgrade(Upgrade newUpgrade, Dictionary<UpgradeableStatsEnum, UpgradeValues> selectedRange, int nStats, float priceMultiplier)
    {
        int price = 0;
        List<int> usedStats = new List<int>();

        for (int i = 0; i < nStats; i++)
        {
            if (usedStats.Count > Enum.GetNames(typeof(UpgradeableStatsEnum)).Length)
                break;

            int valueType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ValueTypeEnum)).Length - 1);
            int statType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(UpgradeableStatsEnum)).Length - 1);

            //only allow one stat value
            while (usedStats.Contains(statType))
            {
                statType++;
                if (statType > Enum.GetNames(typeof(UpgradeableStatsEnum)).Length - 1)
                    statType = 0;
            }
            
            UpgradeValue valueRange = selectedRange[(UpgradeableStatsEnum)statType].Values[valueType];
            float newValue = UnityEngine.Random.Range(valueRange.Min, valueRange.Max);

            price += (int)(newValue / (valueRange.Max - valueRange.Min) * priceMultiplier);

            if (UpgradeableStatsEnum.Attack == (UpgradeableStatsEnum)statType || UpgradeableStatsEnum.Defence == (UpgradeableStatsEnum)statType)
            {
                int eType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ElementTypesEnum)).Length - 1);
                newUpgrade.UpgradeEStats.Add(new UpgradeEStat((UpgradeableStatsEnum)statType, (ValueTypeEnum)valueType, newValue, (ElementTypesEnum)eType));
            }
            else
            {
                newUpgrade.UpgradeStats.Add(new UpgradeStat((UpgradeableStatsEnum)statType, (ValueTypeEnum)valueType, newValue));
            }
        }
        newUpgrade.Price = price;
    }
}