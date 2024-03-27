using EntityDataEnums;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UpgradeRange;

public class Upgrade
{
    public List<UpgradeStat> UpgradeStats = new List<UpgradeStat>();
    public List<UpgradeEStat> UpgradeEStats = new List<UpgradeEStat>();

    int _price = 0;

    Guid _upgradeID;
    int _rating = 1;
    int _rarity = 0;

    public int Price { get { return _price; } set { _price = value; } }
    public Guid UpgradeID { get { return _upgradeID; } }
    public int Rating { get { return _rating; } set { _rating = value; } }
    public int Rarity { get { return _rarity; } set { _rarity = value; } }

    public Upgrade(int rarity)
    {
        _upgradeID = Guid.NewGuid();
        _rarity  = rarity;
    }

    public Upgrade(UpgradeScript upgradeScript)
    {
        _upgradeID = upgradeScript.UpgradeID;
        _price = upgradeScript.Price;
        _rarity = upgradeScript.Rarity;

        for (int i = 0; i < upgradeScript.UpgradeStats.Length; i++)
            UpgradeStats.Add(upgradeScript.UpgradeStats[i]);

        for (int i = 0; i < upgradeScript.UpgradeEStats.Length; i++)
            UpgradeEStats.Add(upgradeScript.UpgradeEStats[i]);
    }
}

public class UpgradeStat
{

    protected UpgradeableStatsEnum _statType;
    protected float _value;

    public UpgradeableStatsEnum StatType { get { return _statType; } }
    public float Value { get { return _value; } }

    public UpgradeStat(UpgradeableStatsEnum statType, float value)
    {
        _statType = statType;
        _value = value;
    }
}

public class UpgradeEStat : UpgradeStat
{
    ElementTypesEnum _element;

    public ElementTypesEnum Element { get { return _element; } }

    public UpgradeEStat(UpgradeableStatsEnum statType, float value, ElementTypesEnum element) : base(statType, value)
    {
        _element = element;
    }
}