using EntityDataEnums;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UpgradeRange;

public class Upgrade
{
    List<UpgradeStat> UpgradeStats;
    UpgradeEStat[] UpgradeEStats;

    float _price = 0;

    int _upgradeID;
    bool _duplicates = true;
    int _quantity = 0;
    int _rating = 1;
    int _rarity = 0;

    public float Price { get { return _price; } }
    public int UpgradeID { get { return _upgradeID; } }
    public bool Duplicates { get { return _duplicates; } }
    public int Quantity { get { return _quantity; } }
    public int Rating { get { return _rating; } }
    public int Rarity { get { return _rarity; } }    

    public Upgrade(int rarity)
    {
        _rarity  = rarity;
        GenerateUpgrade();
    }

    public Upgrade(UpgradeScript upgradeScript)
    {

    }

    private void GenerateUpgrade()
    {
        switch (_rarity) {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    int valueType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ValueTypeEnum)).Length-1);
                    UpgradeStat newUpgrade = RandomUpgrade(Common[StatsEnum.HP]);
                    UpgradeStats.Add(newUpgrade);

                }
                break;
            default:
                break;
        }
    }

    private UpgradeStat RandomUpgrade(UpgradeValues range)
    {
        
        return null;
    }
}

public class UpgradeStat
{

    protected StatsEnum _statType;
    protected CalculationOrderEnum _calcType;
    protected float _value;

    public StatsEnum StatType { get { return _statType; } }
    public CalculationOrderEnum ValueType { get { return _calcType; } }
    public float Value { get { return _value; } }

    public UpgradeStat(StatsEnum statType, CalculationOrderEnum calcType, float value)
    {
        _statType = statType;
        _calcType = calcType;
        _value = value;
    }
}

public class UpgradeEStat : UpgradeStat
{
    ElementTypesEnum _element;

    public ElementTypesEnum Element { get { return _element; } }

    public UpgradeEStat(StatsEnum statType, CalculationOrderEnum calcType, float value, ElementTypesEnum element) : base(statType, calcType, value)
    {
        _element = element;
    }
}