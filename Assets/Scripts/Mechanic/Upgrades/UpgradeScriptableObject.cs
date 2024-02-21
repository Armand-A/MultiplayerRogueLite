using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Stat", menuName = "Upgrade Stat Data")]
public class UpgradeScriptableObject : ScriptableObject
{
    [SerializeField] UpgradeStat[] UpgradeStats;

    [SerializeField] float _price = 0;

    int _upgradeID;
    bool _duplicates = true;
    int _quantity = 0;

    public float Price { get { return _price; } }
    public int UpgradeID { get { return _upgradeID; } }
    public bool Duplicates { get { return _duplicates; } }
    public int Quantity { get { return _quantity; } }

    [System.Serializable]
    public class UpgradeStat
    {
        [SerializeField] EntityDataTypes.Stats _statType;
        [SerializeField] EntityDataTypes.ValueType _valueType;
        [SerializeField] EntityDataTypes.ElementTypes _element;
        [SerializeField] float value;

        public EntityDataTypes.Stats StatType { get { return _statType; } }
        public EntityDataTypes.ValueType ValueType { get { return _valueType; } }
        public EntityDataTypes.ElementTypes Element { get { return _element; } }
        public float Value { get { return value; } }
    }
}