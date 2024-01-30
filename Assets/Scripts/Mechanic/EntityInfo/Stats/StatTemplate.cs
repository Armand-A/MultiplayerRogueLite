using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatTemplate : MonoBehaviour
{
    [SerializeField] private float _baseValue;
    float _curVal;
    
    //For consumable stats like health and action
    float _tempVal;
    bool _useTempVal = false;

    [SerializeField] private float _minVal;
    [SerializeField] private float _maxVal;

    //can add or remove value beyond its _tempCurVal
    [SerializeField] private bool _hasLimiter;

    public readonly StatTypes.ValueType StatValueType;

    public StatTemplate(float baseValue = 10, float curVal = 10, bool useTempVal = false)
    {
        
        _baseValue = baseValue;
        _curVal = curVal;
        _useTempVal = useTempVal;

        if (_useTempVal)
            _tempVal = curVal;
    }

    public virtual bool AddStat(float value)
    {
        if (_curVal + value < _maxVal)
        {
            _curVal = value;
            return true;
        }
        return false;
    }

    public virtual bool RemoveStat(float value)
    {
        if (_curVal - value > _minVal)
        {
            _curVal = value;
            return true;
        }
        return false;
    }

    public virtual bool CalculateStat()
    {
        return false;
    }
}