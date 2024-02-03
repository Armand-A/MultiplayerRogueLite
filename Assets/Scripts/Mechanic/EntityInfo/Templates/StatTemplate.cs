using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatTemplate : MonoBehaviour
{
    private float _baseValue;
    float _totalVal;
    public readonly StatTypes.ValueType StatValueType;
    
    //For consumable stats like health, action, currency
    float _currentVal;
    bool _consumable = false;

    private float _minVal;
    private float _maxVal = 999999999;

    //can add or remove value beyond its _tempCurVal
    [SerializeField] private bool _hasLimiter = false;

    //private float[] _elements = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];

    public float AddModifier = 0;
    public float MultiModifier = 1;
    public float PercentModifier = 1;

    public StatTemplate(float baseValue = 10, float minVal = 0, float maxVal = 999999999, bool consumable = false, bool hasLimiter = false, StatTypes.ValueType statValueType = StatTypes.ValueType.Flat)
    {
        _baseValue = baseValue;
        _totalVal = baseValue;
        _currentVal = baseValue;
        _minVal = minVal;
        _maxVal = maxVal;

        StatValueType = statValueType;

        if (CheckNewStat(_baseValue))
        {
            Debug.LogError(gameObject.name + " may have issues with the base stat template");
        }

        _consumable = consumable;
        _hasLimiter = hasLimiter;
    }

    public virtual bool Refill(float value)
    {
        if (_currentVal + value < _totalVal)
        {
            _totalVal = value;
            return true;
        }
        return false;
    }

    public virtual bool Deplete(float value)
    {
        if (_totalVal - value > _minVal)
        {
            _totalVal = value;
            return true;
        }
        return false;
    }

    // use the modifiers 
    public virtual float CalculateStat(StatTypes.CalculationOrder order)
    {
        float newValue = 0;

        newValue = _baseValue + AddModifier;
        newValue = newValue * MultiModifier;
        newValue = newValue * PercentModifier;

        CheckNewStat(newValue);
        return _totalVal;
    }

    public virtual bool CheckNewStat(float value)
    {
        // if calculations exceeded minimum or maximum allowed stat value
        if (value < _minVal)
        {
            _totalVal = _minVal;
            return false;
        } else if (value > _maxVal) 
        {
            _totalVal = _maxVal;
            return false;
        }

        // if there is no problem
        _totalVal = value;
        return true;
    }
}