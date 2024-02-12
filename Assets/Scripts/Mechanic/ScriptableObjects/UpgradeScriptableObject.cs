using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Stat", menuName = "Upgrade Stat Data")]
public class UpgradeScriptableObject : ScriptableObject
{
    [SerializeField] EntityDataTypes.Stats _statType;
    [SerializeField] EntityDataTypes.ValueType _valueType;
    [SerializeField] EntityDataTypes.ElementTypes _element;
    [SerializeField] float price;
    [SerializeField] float value;

    public EntityDataTypes.Stats StatType { get { return _statType; } }
    public EntityDataTypes.ValueType ValueType { get { return _valueType; } }
    public EntityDataTypes.ElementTypes Element { get { return _element; } }
    public float Value { get { return value; } }
}