using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntityDataEnums;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade Data")]
public class UpgradeScript : ScriptableObject
{
    [SerializeField] UpgradeStat[] UpgradeStats;
    [SerializeField] UpgradeEStat[] UpgradeEStats;

    [SerializeField] float _price = 0;

    string _name;
    int _upgradeID;
    bool _duplicates = true;
    int _quantity = 0;
    int _rarity = 0;

    public string Name { get { return _name; } }
    public float Price { get { return _price; } }
    public int UpgradeID { get { return _upgradeID; } }
    public bool Duplicates { get { return _duplicates; } }
    public int Quantity { get { return _quantity; } }
    public int Rarity { get { return _rarity; } }

    [System.Serializable]
    public class UpgradeStat
    {   
        
        [SerializeField] StatsEnum _statType;
        [SerializeField] ValueTypeEnum _valueType;
        [SerializeField] float value;

        public StatsEnum StatType { get { return _statType; } }
        public ValueTypeEnum ValueType { get { return _valueType; } }
        public float Value { get { return value; } }
    }

    [System.Serializable]
    public class UpgradeEStat
    {
        [SerializeField] StatsEnum _statType;
        [SerializeField] ValueTypeEnum _valueType;
        [SerializeField] ElementTypesEnum _element;
        [SerializeField] float value;

        public StatsEnum StatType { get { return _statType; } }
        public ValueTypeEnum ValueType { get { return _valueType; } }
        public ElementTypesEnum Element { get { return _element; } }
        public float Value { get { return value; } }
    }
}