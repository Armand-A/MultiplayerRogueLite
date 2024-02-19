using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatTemplate
{
    private float _startValue;

    float _currentVal;
    public float Value { get { return _currentVal; } }

    public EntityDataTypes.StatStatus CurrentStatus;

    public readonly EntityDataTypes.ValueType StatValueType;

    private float _minVal;
    private float _maxVal = 999999999;

    private float _flat = 0;
    private float _percent = 100;
    private float _multi = 1;

    public StatTemplate(GameObject parent, float startValue = 10, float minVal = 0, float maxVal = 999999999, EntityDataTypes.ValueType statValueType = EntityDataTypes.ValueType.Flat)
    {
        _startValue = startValue;
        _currentVal = startValue;
        _minVal = minVal;
        _maxVal = maxVal;

        StatValueType = statValueType;

        CheckNewStat(_startValue);
        if (CurrentStatus != EntityDataTypes.StatStatus.Normal)
        {
            Debug.LogError(parent.name + " may have issues");
        }
    }

    public virtual void CheckNewStat(float value)
    {
        // if calculations exceeded minimum or maximum allowed stat value
        if (value < _minVal)
        {
            _currentVal = _minVal;
            CurrentStatus = EntityDataTypes.StatStatus.Min;
        }
        else if (value > _maxVal)
        {
            _currentVal = _maxVal;
            CurrentStatus = EntityDataTypes.StatStatus.Max;
        }
        else
        {
            _currentVal = value;
            CurrentStatus = EntityDataTypes.StatStatus.Normal;
        }
    }

    public virtual void StatUpdate(float flat, float percent, float multi) 
    {
        _flat = flat;
        _percent = percent;
        _multi = multi;

        float newTotal = CalculateStat();
        CheckNewStat(newTotal);
    }

    public virtual float CalculateStat()
    {
        return (_startValue + _flat) * (_percent/100) * _multi;
    }
}