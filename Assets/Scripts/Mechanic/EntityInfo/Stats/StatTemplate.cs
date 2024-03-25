using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EntityDataEnums;

public class StatTemplate
{
    private float _baseValue;

    float _currentVal;
    public float Value { get { return _currentVal; } }

    public StatStatusEnum CurrentStatus;

    public readonly ValueTypeEnum StatValueType;

    private float _minVal;
    private float _maxVal = 999999999;

    private float _flat = 0;
    private float _percent = 100;

    public StatTemplate(GameObject parent, float startValue = 10, float minVal = 0, float maxVal = 999999999, ValueTypeEnum statValueType = ValueTypeEnum.Flat)
    {
        _baseValue = startValue;
        _currentVal = startValue;
        _minVal = minVal;
        _maxVal = maxVal;

        StatValueType = statValueType;

        UpdateStat(_currentVal);
        if (CurrentStatus != StatStatusEnum.Normal)
        {
            Debug.LogError(parent.name + " may have issues");
        }
    }

    public void UpdateStat(float value)
    {
        // if calculations exceeded minimum or maximum allowed stat value
        if (value < _minVal)
        {
            _currentVal = _minVal;
            CurrentStatus = StatStatusEnum.Min;
        }
        else if (value > _maxVal)
        {
            _currentVal = _maxVal;
            CurrentStatus = StatStatusEnum.Max;
        }
        else
        {
            _currentVal = value;
            CurrentStatus = StatStatusEnum.Normal;
        }
    }

    public void StatModifier(float flat, float percent) 
    {
        float newTotal = CalculateStat(_flat + flat, _percent + percent);

        _flat = flat;
        _percent = percent;
        UpdateStat(newTotal);
    }

    public float CalculateStat(float flat, float percent)
    {
        return (_baseValue + flat) * (percent / 100);
    }

    public float UpgradeRange(ValueTypeEnum value, float maxIncrease, int rarity)
    {
        switch (rarity)
        {
            case 0:
                break;
            default:
                break;
        }
        return 0;
    }
}