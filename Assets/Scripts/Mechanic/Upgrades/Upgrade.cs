using EntityDataEnums;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UpgradeRange;

public class Upgrade
{
    public List<UpgradeStat> UpgradeStats = new List<UpgradeStat>();
    public List<UpgradeEStat> UpgradeEStats = new List<UpgradeEStat>();

    int _price = 0;

    GUID _upgradeID;
    int _rating = 1;
    Rarity _rarity = Rarity.Common;

    public int Price { get { return _price; } set { _price = value; } }
    public GUID UpgradeID { get { return _upgradeID; } }
    public int Rating { get { return _rating; } set { _rating = value; } }
    public Rarity GivenRarity { get { return _rarity; } set { _rarity = value; } }

    public Upgrade(Rarity rarity)
    {
        _upgradeID = GUID.Generate();
        _rarity  = rarity;
    }
/*
    public Upgrade(UpgradeScript upgradeScript)
    {
        _upgradeID = upgradeScript.UpgradeID;
        _price = upgradeScript.Price;
        _rarity = upgradeScript.GivenRarity;

        //temp
        for (int i = 0; i < upgradeScript.UpgradeStats.Length; i++)
            UpgradeStats.Add(upgradeScript.UpgradeStats[i]);

        for (int i = 0; i < upgradeScript.UpgradeEStats.Length; i++)
            UpgradeEStats.Add(upgradeScript.UpgradeEStats[i]);
    }*/
}

public class UpgradeStat
{

    protected UpgradeableStatsEnum _statType;
    protected ValueTypeEnum _valType;
    protected float _value;

    public UpgradeableStatsEnum StatType { get { return _statType; } }
    public ValueTypeEnum ValueType { get { return _valType; } }
    public float Value { get { return _value; } }

    public UpgradeStat(UpgradeableStatsEnum statType, ValueTypeEnum valType, float value)
    {
        _statType = statType;
        _valType = valType;
        _value = value;
    }
}

public class UpgradeEStat : UpgradeStat
{
    ElementTypesEnum _element;

    public ElementTypesEnum ElementType { get { return _element; } }

    public UpgradeEStat(UpgradeableStatsEnum statType, ValueTypeEnum valType, float value, ElementTypesEnum element) : base(statType, valType, value)
    {
        _element = element;
    }
}