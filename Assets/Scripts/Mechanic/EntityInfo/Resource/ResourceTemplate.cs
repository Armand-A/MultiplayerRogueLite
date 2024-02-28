using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTemplate
{
    [Header("Resource values")]
    [Tooltip("Current value")]
    [SerializeField] protected float _currentValue = 20;
    [Tooltip("Total value")]
    [SerializeField] protected float _totalValue = 20;

    public float Value { get { return _currentValue; } set { _currentValue = value; } }
    public float TotalValue { get { return _totalValue; } set { _totalValue = value; } }

    public virtual bool Add(float value)
    {
        if (_currentValue + value <= _totalValue)
        {
            _currentValue += value;
            return true;
        }
        else
        {
            _currentValue = _totalValue;
            return false;
        }
    }

    public virtual bool Remove(float value)
    {
        if (_currentValue - value >= 0)
        {
            _currentValue -= value;
            return true;
        }
        return false;
    }
}