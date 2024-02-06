using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatTemplate : MonoBehaviour
{
    private float _baseValue;
    float _currentVal;
    public readonly EntityDataTypes.ValueType StatValueType;

    private float _minVal;
    private float _maxVal = 999999999;

    public StatTemplate(float baseValue = 10, float minVal = 0, float maxVal = 999999999, EntityDataTypes.ValueType statValueType = EntityDataTypes.ValueType.Flat)
    {
        _baseValue = baseValue;
        _currentVal = baseValue;
        _minVal = minVal;
        _maxVal = maxVal;

        StatValueType = statValueType;

        if (CheckNewStat(_baseValue) < _minVal || _maxVal <CheckNewStat(_baseValue))
        {
            Debug.LogError(gameObject.name + " may have issues with the base stat template");
        }
    }

    public virtual float CheckNewStat(float value)
    {
        // if calculations exceeded minimum or maximum allowed stat value
        if (value < _minVal)
            _currentVal = _minVal;
        else if (value > _maxVal) 
            _currentVal = _maxVal;
        else
            _currentVal = value;

        return _currentVal;
    }
}