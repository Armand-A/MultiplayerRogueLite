using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTemplate : MonoBehaviour
{
    [SerializeField] private float _baseValue;
    float _curVal;
    
    //For consumable stats like health and action
    float _tempVal;

    [SerializeField] private float _minVal;
    [SerializeField] private float _maxVal;

    //can add or remove value beyond its _tempCurVal
    [SerializeField] private bool _hasLimiter;

    public readonly StatTypes.ValueType StatValueType;

    public StatTemplate(float curVal = 10, float tempVal = 10)
    {
        _curVal = curVal;
        _tempVal = tempVal;
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
}