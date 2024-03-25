using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : ResourceTemplate
{
    public Currency(float value) : base(value)
    {
        Value = value;
        _totalValue = float.MaxValue;
    }

    public override bool Remove(float value)
    {
        if (_currentValue - value >= 0)
        {
            _currentValue -= value;
            return true;
        }
        return false;
    }
}
